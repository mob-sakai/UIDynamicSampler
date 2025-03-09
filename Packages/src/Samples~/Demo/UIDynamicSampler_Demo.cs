using UnityEngine;
using UnityEngine.UI;

public class UIDynamicSampler_Demo : MonoBehaviour
{
    private Mask[] _masks;
    private Image[] _images;

    [SerializeField]
    private Transform m_Root;

    private void Awake()
    {
        _masks = m_Root.GetComponentsInChildren<Mask>();
        _images = m_Root.GetComponentsInChildren<Image>();
    }

    public void SetImageSize(float size)
    {
        foreach (var mask in _masks)
        {
            mask.rectTransform.sizeDelta = new Vector2(size, size);
        }

        foreach (var image in _images)
        {
            image.SetMaterialDirty();
        }
    }
}
