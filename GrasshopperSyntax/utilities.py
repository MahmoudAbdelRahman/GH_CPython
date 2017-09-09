# Grasshopper Module Version 0.0.1.5
# Developed by Mahmoud Abdelrahman

"""
This Module is part of GH_CPython
BSD 2-Clause License

This project has been started by: Mahmoud M. Abdelrahman
<arch.mahmoud.ouf111[at]gmail.com>
Copyright (c) 2017, Mahmoud AbdelRahman

All rights reserved.
https://github.com/MahmoudAbdelRahman/GH_CPython/blob/master/LICENSE
"""

import Grasshopper02 as gh

def isPlane(input, name):
    if isinstance(input, gh.Plane):
        return True
    else:
        raise Exception(name + "should be an instance of gh.Plane")
        return False

def isPoint(input, name):
    if isinstance(input, gh.Point):
        return True
    else:
        raise Exception(name + "should be an instance of a gh.Point")
        return False