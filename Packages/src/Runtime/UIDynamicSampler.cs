using Coffee.UIDynamicSamplerInternal;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if URP_ENABLE
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#endif

namespace Coffee.UIExtensions
{
    [ExecuteAlways]
    [RequireComponent(typeof(Graphic))]
    [Icon("Packages/com.coffee.ui-dynamic-sampler/UIDynamicSamplerIcon.png")]
    public class UIDynamicSampler : UIBehaviour, IMaterialModifier
    {
        [Range(0.5f, 10f)]
        [SerializeField] private float m_SamplingThreshold = 2f;

        [Range(0.5f, 10f)]
        [SerializeField] private float m_ScaleFactor = 1.5f;

        private RenderTexture _dynamicTexture;
        private Canvas.WillRenderCanvases _blit;
        private Graphic _graphic;

        private Graphic graphic => _graphic ? _graphic : _graphic = GetComponent<Graphic>();
        public RenderTexture dynamicTexture => _dynamicTexture;


        protected override void OnEnable()
        {
            SetMaterialDirty();
        }

        protected override void OnDisable()
        {
            SetMaterialDirty();
            RenderTextureRepository.Release(ref _dynamicTexture);
            if (_blit != null)
            {
                Canvas.willRenderCanvases -= _blit;
            }
        }

        protected override void OnDestroy()
        {
            _blit = null;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            SetMaterialDirty();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetMaterialDirty();
        }
#endif

        public void SetMaterialDirty()
        {
            if (graphic)
            {
                graphic.SetMaterialDirty();
#if UNITY_EDITOR
                EditorApplication.QueuePlayerLoopUpdate();
#endif
            }
        }

        Material IMaterialModifier.GetModifiedMaterial(Material baseMaterial)
        {
            if (baseMaterial && isActiveAndEnabled)
            {
                Canvas.willRenderCanvases -= _blit ??= Blit;
                Canvas.willRenderCanvases += _blit;
            }

            return baseMaterial;
        }

        private void Blit()
        {
            Canvas.willRenderCanvases -= _blit;
            if (!graphic || !graphic.canvas) return;

            // mainTexture is null.
            var tex = _graphic.mainTexture;
            if (!tex)
            {
                graphic.canvasRenderer.SetTexture(null);
                RenderTextureRepository.Release(ref _dynamicTexture);
                return;
            }

            // Anti-aliasing is disabled.
            var texSize = new Vector2Int(tex.width, tex.height);
            var canvas = graphic.canvas;
            var canvasScale = canvas.scaleFactor;
            var rtSize = graphic.rectTransform.rect.size;
            if (Mathf.Max(texSize.x, texSize.y) / m_SamplingThreshold < Mathf.Max(rtSize.x, rtSize.y) * canvasScale)
            {
                graphic.canvasRenderer.SetTexture(tex);
                RenderTextureRepository.Release(ref _dynamicTexture);
                return;
            }

            var cam = canvas.renderMode != RenderMode.ScreenSpaceOverlay
                ? canvas.worldCamera
                : null;
            var renderScale = cam && cam.allowDynamicResolution
                ? Mathf.Max(ScalableBufferManager.widthScaleFactor, ScalableBufferManager.heightScaleFactor)
                : 1;
#if URP_ENABLE
            if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset urpAsset)
            {
                renderScale *= urpAsset.renderScale;
            }
#endif

            // Get or create render texture.
            var size = Vector2Int.RoundToInt(rtSize * canvasScale * m_ScaleFactor * renderScale);
            var hash = new Hash128((uint)tex.GetInstanceID(), (uint)size.GetHashCode(), 0, 0);
            if (!RenderTextureRepository.Valid(hash, _dynamicTexture))
            {
                RenderTextureRepository.Get(hash, ref _dynamicTexture,
                    x => new RenderTexture(RenderTextureRepository.GetDescriptor(x, false))
                    {
                        hideFlags = HideFlags.DontSave
                    }, size);

                // Blit to render texture.
                Graphics.Blit(tex, _dynamicTexture);
            }

            graphic.canvasRenderer.SetTexture(_dynamicTexture);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(UIDynamicSampler))]
    [CanEditMultipleObjects]
    public class UIDynamicSamplerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            var tex = (target as UIDynamicSampler).dynamicTexture;
            if (tex)
            {
                EditorGUILayout.LabelField("Sampling Size", $"{tex.width} x {tex.height}");
            }
            else
            {
                EditorGUILayout.LabelField("Sampling Size", "disabled");
            }

            EditorGUILayout.EndVertical();
        }
    }
#endif
}
