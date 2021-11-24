## Anouncemet 🎤 This project is no longer maintained.

[![DOI](https://zenodo.org/badge/95458635.svg)](https://zenodo.org/badge/latestdoi/95458635)
[![alpha](https://img.shields.io/badge/GH__CPython-Download%20Alpha-yellow.svg)](https://github.com/MahmoudAbdelRahman/GH_CPython/blob/master/GH_CPython.zip)

![python_logo-100](https://user-images.githubusercontent.com/6969514/28852793-2ad6ac70-772c-11e7-92d1-cd9087e3b661.png)

# GH_CPython
CPython plugin for Grasshopper. 

Please cite as : 
```bib
@misc{AbdelRahman2017,
abstract = {CPython plugin for grasshopper},
author = {AbdelRahman, Mahmoud},
doi = {10.5281/ZENODO.888148},
title = {{GH{\_}CPython: CPython plugin for grasshopper}},
url = {https://doi.org/10.5281/zenodo.888148},
year = {2017}
}
```
`Mahmoud Mohamed Abdelrahman. (2017). Gh_CPython: CPython plugin for grasshopper. https://doi.org/10.5281/zenodo.888148`

![ghcpythonposter2](https://user-images.githubusercontent.com/6969514/30294594-ecc40112-973d-11e7-84df-0520bc8cb7cc.jpg)

![image](https://user-images.githubusercontent.com/6969514/29739952-a2c2a0fe-8a4b-11e7-8c0e-63b86e836e23.png)

-----------

[DOI: 10.13140/RG.2.2.27230.33600/1](https://www.researchgate.net/publication/319312347_Enhancing_computational_design_with_Python_high_performance_scientific_libraries_Integration_of_Grasshopper_and_CPython_language)


#### The Idea of this plugin is to provide a component that implements CPython codes inside grasshopper.
- It is pereferred that you download one of the bundles that include all the required python scientific libraries such as [Python(x,y)](https://python-xy.github.io/), [Anaconda](https://docs.anaconda.com/anaconda/install/) or [Canopy](https://www.enthought.com/products/canopy/)
- You have to run Rhino as adminstrator at first. 

The following image shows CPython plugin(down right) , and the first implemented plugin (top right) and the code ide (left)
![firstplugin](https://user-images.githubusercontent.com/6969514/28816126-f27d5a8c-76a3-11e7-8dd0-3d368657b862.JPG)

![plgn](https://user-images.githubusercontent.com/6969514/28817868-cb7993a0-76a9-11e7-9e06-fc059594cbbc.png)

Add description, documentation to each input and output 
![documentations](https://user-images.githubusercontent.com/6969514/29185199-d42dca46-7e08-11e7-89be-1e78e05fe14d.jpg)

![image](https://user-images.githubusercontent.com/6969514/29340173-fee91a6a-821d-11e7-8cfe-3cd43c3c2310.png)

Supports any version on Python. 2.7, 3.4, 3.5, 3.6 ..
![image](https://user-images.githubusercontent.com/6969514/29262322-494ad3dc-80d4-11e7-895f-2b8110e892e0.png)
### How to choose between different Python interpreters? 
By clicking on the thin blue line at the bottom of any component, a new window will popup and you can choose your preferred interpreter from this window.
![221](https://user-images.githubusercontent.com/6969514/29310266-55758290-81ad-11e7-80cd-5b3dcde4d66a.jpg)
## What can and What can't it do?
### It can: 
1. Add as many input and output variables as needed. 
2. Change its name, NickName and discription.
3. Import all CPython modules such as numpy, scipy, matplotlib, sympy, etc..
4. Change Icon (simply drag a 24x24 pixel image to the component).
5. Save as user object. 
6. Create Rhino Geometry (in progress)

![01](https://user-images.githubusercontent.com/6969514/28823476-0cccdf92-76be-11e7-8001-7ed91f00f10e.JPG)
![02](https://user-images.githubusercontent.com/6969514/28823500-2ae2afc0-76be-11e7-80be-4a00164e721f.JPG)
![04](https://user-images.githubusercontent.com/6969514/28823504-349dce28-76be-11e7-810b-7ae71bf52cde.JPG)
![06](https://user-images.githubusercontent.com/6969514/30004276-7e8fd47c-90cc-11e7-9251-5eafbcc74b5c.png)
-------------------
### GhPython Vs. Gh_CPython
![speedtest](https://user-images.githubusercontent.com/6969514/28900506-f88cb34c-77f1-11e7-825d-d34d3c4d14b9.JPG)
------------------
## License
BSD 2-Clause License

This project has been started by: Mahmoud M. Abdelrahman 

<arch.mahmoud.ouf111[at]gmail.com>

Copyright (c) 2017, Mahmoud AbdelRahman

All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
