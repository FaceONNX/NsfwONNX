<p align="center"><img width="30%" src="docs/NsfwONNX_scaled.png" /></p>
<p align="center"> NSFW recognition library based on deep neural networks and <b>ONNX</b> runtime </p>  

# NsfwONNX
**NsfwONNX** is a Not Suitable for Work (NSFW) recognition library based on [ONNX](https://onnx.ai/) runtime.

# Version
You can build **NsfwONNX** from sources or install to your own project using nuget package manager.
| Assembly | Specification | OS | Platform | Package | Algebra |
|-------------|:-------------:|:-------------:|:--------------:|:--------------:|:--------------:|
| [NsfwONNX](netstandard/NsfwONNX) | .NET Standard 2.0 | Cross-platform | CPU | [NuGet](https://www.nuget.org/packages/NsfwONNX/) | [UMapx](https://github.com/asiryan/UMapx) |
| [NsfwONNX.Gpu](netstandard/NsfwONNX.Gpu) | .NET Standard 2.0 | Cross-platform | GPU | [NuGet](https://www.nuget.org/packages/NsfwONNX.Gpu/) | [UMapx](https://github.com/asiryan/UMapx) |

# Installation
C# interface  
```c#
using NsfwONNX;
```
To get started with **NsfwONNX**, it is recommended to look at the folder with [examples](netstandard/Examples).  

# References 
Yahoo - [Open NSFW](https://github.com/yahoo/open_nsfw)  

# License
**NsfwONNX** is released under the [MIT](LICENSE) license.
