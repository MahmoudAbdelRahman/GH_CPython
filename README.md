![python_logo-100](https://user-images.githubusercontent.com/6969514/28852793-2ad6ac70-772c-11e7-92d1-cd9087e3b661.png)
# GH_CPython
CPython IDE for grasshopper. 


![gowentgone](https://user-images.githubusercontent.com/6969514/28756153-1cd1bc92-7569-11e7-9ce2-75ca52090598.JPG)
-----------

#### The Idea of this plugin is to provide a component that implements CPython codes inside grasshopper.
- It requires having Python2.7 installed in your machine `C:\Python27\`, it is pereferred that you download one of the bundles that include all the required python scientific libraries such as [Python(x,y)](https://python-xy.github.io/), [Andconda](https://www.continuum.io/downloads) or [Canopy](https://www.enthought.com/products/canopy/)
- You have to run Rhino as adminstrator at first. 

The following image shows CPython plugin(down right) , and the first implemented plugin (top right) and the code ide (left)
![firstplugin](https://user-images.githubusercontent.com/6969514/28816126-f27d5a8c-76a3-11e7-8dd0-3d368657b862.JPG)

![plgn](https://user-images.githubusercontent.com/6969514/28817868-cb7993a0-76a9-11e7-9e06-fc059594cbbc.png)

Add description, documentation to each input and output 
![documentations](https://user-images.githubusercontent.com/6969514/29185199-d42dca46-7e08-11e7-89be-1e78e05fe14d.jpg)

## What can and What can't it do?
### It can: 
1. Add as many input and output variables as needed. 
2. Change its name, NickName and discription.
3. Import all CPython modules such as numpy, scipy, matplotlib, sympy, etc..
4. Change Icon (simply drag a 24x24 pixel image to the component).
5. Save as user object. 

![01](https://user-images.githubusercontent.com/6969514/28823476-0cccdf92-76be-11e7-8001-7ed91f00f10e.JPG)
![02](https://user-images.githubusercontent.com/6969514/28823500-2ae2afc0-76be-11e7-80be-4a00164e721f.JPG)
![04](https://user-images.githubusercontent.com/6969514/28823504-349dce28-76be-11e7-810b-7ae71bf52cde.JPG)
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
