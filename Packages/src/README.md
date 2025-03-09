# <img height="26" src="https://github.com/user-attachments/assets/89d75386-0fbb-43d7-81e8-df6e5339213a"/> UI Dynamic Sampler <!-- omit in toc -->

[![](https://img.shields.io/npm/v/com.coffee.ui-dynamic-sampler?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.coffee.ui-dynamic-sampler/)
[![](https://img.shields.io/github/v/release/mob-sakai/UIDynamicSampler?include_prereleases)](https://github.com/mob-sakai/UIDynamicSampler/releases)
[![](https://img.shields.io/github/release-date/mob-sakai/UIDynamicSampler.svg)](https://github.com/mob-sakai/UIDynamicSampler/releases)  
![](https://img.shields.io/badge/Unity-2020.3+-57b9d3.svg?style=flat&logo=unity)
![](https://img.shields.io/badge/Unity-6000.0+-57b9d3.svg?style=flat&logo=unity)  
[![](https://img.shields.io/github/license/mob-sakai/UIDynamicSampler.svg)](https://github.com/mob-sakai/UIDynamicSampler/blob/main/LICENSE.md)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-orange.svg)](http://makeapullrequest.com)
[![](https://img.shields.io/github/watchers/mob-sakai/UIDynamicSampler.svg?style=social&label=Watch)](https://github.com/mob-sakai/UIDynamicSampler/subscription)
[![](https://img.shields.io/twitter/follow/mob_sakai.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=mob_sakai)

<< [📝 Description](#-description-) | [📌 Key Features](#-key-features) | [🎮 Demo](#-demo) | [⚙ Installation](#-installation) | [🚀 Usage](#-usage) | [🤝 Contributing](#-contributing) >>

## 📝 Description <!-- omit in toc -->

This package provides a component to reduce jaggies in UI elements.

For example, when displaying a 2048x2048 texture at just 100x100 pixels, diagonal lines may appear jagged.  
This effect is particularly noticeable on low-DPI displays (such as standard non-Retina screens) where individual pixels are more visible.

![](https://github.com/user-attachments/assets/7eb7527e-5a08-417b-907d-2f94dda7d592)

In such cases, jaggies can be reduced by generating a thumbnail texture that matches the display size or by using mipmaps.  
However, these approaches increase asset size and complicate asset management.  
Moreover, depending on the UI element's size, these approaches may cause blurring or fail to sufficiently reduce jaggies.

![](https://github.com/user-attachments/assets/042dcf2b-7220-47d6-9864-7c1bb59a9a2b)

The `UIDynamicSampler` component dynamically pre-samples textures based on the current UI element size, effectively reducing jaggies without increasing asset size.  
Additionally, it caches sampling results to maintain performance.

![](https://github.com/user-attachments/assets/804b5995-1dd3-4569-a1b5-3e9030818c3b)

- [📌 Key Features](#-key-features)
- [🎮 Demo](#-demo)
- [⚙ Installation](#-installation)
    - [Install via OpenUPM](#install-via-openupm)
    - [Install via UPM (with Package Manager UI)](#install-via-upm-with-package-manager-ui)
    - [Install via UPM (Manually)](#install-via-upm-manually)
    - [Install as Embedded Package](#install-as-embedded-package)
- [🚀 Usage](#-usage)
    - [Getting Started](#getting-started)
- [🤝 Contributing](#-contributing)
    - [Issues](#issues)
    - [Pull Requests](#pull-requests)
    - [Support](#support)
- [License](#license)
- [Author](#author)
- [See Also](#see-also)

<br><br>

## 📌 Key Features

- **Real-time anti-jaggies for uGUI**: Dynamically samples textures based on UI element size to reduce jaggies.
- **No Increase in Asset Size**: Performs sampling dynamically, eliminating the need for additional thumbnails or mipmaps, keeping asset management simple.
- **High Performance with Caching**: Caches sampling results to reduce unnecessary computations.
- **Improved Visibility on Low-DPI Displays**: Provides clearer rendering even on lower-resolution screens where jaggies are more noticeable.
- **Easy to Use**: Simply add the `UIDynamicSampler` component to apply the effect.

<br><br>

## 🎮 Demo

![](https://github.com/user-attachments/assets/ed4393d2-276e-4672-b9c7-bd450d1219d5)

[WebGL Demo](https://mob-sakai.github.io/UIDynamicSampler/)

<br><br>

## ⚙ Installation

_This package requires **Unity 2020.3 or later**._

### Install via OpenUPM

- This package is available on [OpenUPM](https://openupm.com/packages/com.coffee.ui-dynamic-sampler/) package
  registry.
- This is the preferred method of installation, as you can easily receive updates as they're released.
- If you have [openupm-cli](https://github.com/openupm/openupm-cli) installed, then run the following command in your
  project's directory:
  ```
  openupm add com.coffee.ui-dynamic-sampler
  ```
- To update the package, use Package Manager UI (`Window > Package Manager`) or run the following command with
  `@{version}`:
  ```
  openupm add com.coffee.ui-dynamic-sampler@1.0.0
  ```

### Install via UPM (with Package Manager UI)

- Click `Window > Package Manager` to open Package Manager UI.
- Click `+ > Add package from git URL...` and input the repository URL:
  `https://github.com/mob-sakai/UIDynamicSampler.git?path=Packages/src`  
  ![](https://github.com/user-attachments/assets/f88f47ad-c606-44bd-9e86-ee3f72eac548)
- To update the package, change suffix `#{version}` to the target version.
    - e.g. `https://github.com/mob-sakai/UIDynamicSampler.git?path=Packages/src#1.0.0`

### Install via UPM (Manually)

- Open the `Packages/manifest.json` file in your project. Then add this package somewhere in the `dependencies` block:
  ```json
  {
    "dependencies": {
      "com.coffee.ui-dynamic-sampler": "https://github.com/mob-sakai/UIDynamicSampler.git?path=Packages/src",
      ...
    }
  }
  ```

- To update the package, change suffix `#{version}` to the target version.
    - e.g.
      `"com.coffee.ui-dynamic-sampler": "https://github.com/mob-sakai/UIDynamicSampler.git?path=Packages/src#1.0.0",`

### Install as Embedded Package

1. Download the `Source code (zip)` file from [Releases](https://github.com/mob-sakai/UIDynamicSampler/releases) and
   extract it.
2. Move the `<extracted_dir>/Packages/src` directory into your project's `Packages` directory.  
   ![](https://github.com/user-attachments/assets/187cbcbe-5922-4ed5-acec-cf19aa17d208)
    - You can rename the `src` directory if needed.
    - If you intend to fix bugs or add features, installing it as an embedded package is recommended.
    - To update the package, re-download it and replace the existing contents.

<br><br>

## 🚀 Usage

### Getting Started

1. [Install the package](#-installation).

2. Add a `UIDynamicSampler` component to a UI element (Image, RawImage) from the
   `Add Component` in the inspector or `Component > UI > UIDynamicSampler` menu.  
   ![](https://github.com/user-attachments/assets/d4c886c5-d4bb-47fa-b5ad-f65cc765a0e5)

3. Compare how jaggies appear in low DPI display environments.  
   ![](https://github.com/user-attachments/assets/c61a5cde-f67c-43d1-a31c-cfe86cb3a556)

4. Enjoy!

<br><br>

## 🤝 Contributing

### Issues

Issues are incredibly valuable to this project:

- Ideas provide a valuable source of contributions that others can make.
- Problems help identify areas where this project needs improvement.
- Questions indicate where contributors can enhance the user experience.

### Pull Requests

Pull requests offer a fantastic way to contribute your ideas to this repository.  
Please refer to [CONTRIBUTING.md](https://github.com/mob-sakai/UIDynamicSampler/tree/develop/CONTRIBUTING.md)
and [develop branch](https://github.com/mob-sakai/UIDynamicSampler/tree/develop).

### Support

This is an open-source project developed during my spare time.  
If you appreciate it, consider supporting me.  
Your support allows me to dedicate more time to development. 😊

[![](https://user-images.githubusercontent.com/12690315/66942881-03686280-f085-11e9-9586-fc0b6011029f.png)](https://github.com/users/mob-sakai/sponsorship)  
[![](https://user-images.githubusercontent.com/12690315/50731629-3b18b480-11ad-11e9-8fad-4b13f27969c1.png)](https://www.patreon.com/join/2343451?)

<br><br>

## License

* MIT

## Author

* ![](https://user-images.githubusercontent.com/12690315/96986908-434a0b80-155d-11eb-8275-85138ab90afa.png) [mob-sakai](https://github.com/mob-sakai) [![](https://img.shields.io/twitter/follow/mob_sakai.svg?label=Follow&style=social)](https://twitter.com/intent/follow?screen_name=mob_sakai) ![GitHub followers](https://img.shields.io/github/followers/mob-sakai?style=social)

## See Also

* GitHub page : https://github.com/mob-sakai/UIDynamicSampler
* Releases : https://github.com/mob-sakai/UIDynamicSampler/releases
* Issue tracker : https://github.com/mob-sakai/UIDynamicSampler/issues
* Change log : https://github.com/mob-sakai/UIDynamicSampler/blob/main/Packages/src/CHANGELOG.md
