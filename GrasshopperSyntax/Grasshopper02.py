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
import sys

version = sys.version_info[0]


class Line:
    def __init__(self, *args):
        """Adds new line using two input points, or two input lists or 6  input doubles

        :param args:
        """
        if len(args) == 2:
            if isinstance(args[0], Point) and isinstance(args[1], Point):
                self.X1 = args[0].X
                self.Y1 = args[0].Y
                self.Z1 = args[0].X
                self.X2 = args[1].Z
                self.Y2 = args[1].Y
                self.Z2 = args[1].Z
            elif isinstance(args[0], list) and isinstance(args[1], list):
                self.X1 = args[0][0]
                self.Y1 = args[0][1]
                self.Z1 = args[0][2]
                self.X2 = args[1][0]
                self.Y2 = args[1][1]
                self.Z2 = args[1][2]
            elif version == 2:
                if isinstance(args[0], basestring) and isinstance(args[1], basestring):
                    pointa = Point(args[0])
                    pointb = Point(args[1])
                    self.X1 = pointa.X
                    self.Y1 = pointa.Y
                    self.Z1 = pointa.Z
                    self.X2 = pointb.X
                    self.Y2 = pointb.Y
                    self.Z2 = pointb.Z
            elif version == 3:
                if isinstance(args[0], str) and isinstance(args[1], str):
                    pointa = Point(args[0])
                    pointb = Point(args[1])
                    self.X1 = pointa.X
                    self.Y1 = pointa.Y
                    self.Z1 = pointa.Z
                    self.X2 = pointb.X
                    self.Y2 = pointb.Y
                    self.Z2 = pointb.Z
        elif len(args) == 6:
            self.X1 = args[0]
            self.Y1 = args[1]
            self.Z1 = args[2]
            self.X2 = args[3]
            self.Y2 = args[4]
            self.Z2 = args[5]

    '''def addLine(self):
        return "gCPy.Line(" + str(self.X1) + ", " \
               + str(self.Y1) + ", " \
               + str(self.Z1) + ", " \
               + str(self.X2) + ", " \
               + str(self.Y2) + ", " \
               + str(self.Z2) + ")"'''

    def addLine(self):
        return ["<Line>", self.X1, self.Y1, self.Z1, self.X2, self.Y2, self.Z2]

    def __repr__(self):
        return str(["<Line>",self.X1,self.Y1,self.Z1,self.X2,self.Y2,self.Z2,"</line>"])

    def length(self):
        return ((self.X2 - self.X1) ** 2 + (self.Y2 - self.Y1) ** 2 + (self.Z2 - self.Z1) ** 2) ** 0.5

    def pointOnLine(self, parameter=0.5):
        return Point((self.X2 - self.X1) * parameter + self.X1, \
                     (self.Y2 - self.Y1) * parameter + self.Y1, \
                     (self.Z2 - self.Z1) * parameter + self.Z1)

    def __str__(self):
        return str(["<Line>", self.X1, self.Y1, self.Z1, self.X2, self.Y2, self.Z2, "</Line>"])


class Point:
    """Adds new point in cartesian coordinates using 3 doubles x, y, z as input

        :param:
            x (double): is the x coordinate
            y (double): is the y coordinate
            z (double): is the z coordinate
        :return:
            representation of 3d point
        :Example:
            import Grasshopper as gh
            point1 = gh.Point(0, 0, 0)
            point2 = gh.Point(1., 1., 0.)
    """

    def __init__(self, x=0., y=0., z=0.):
        if version == 2:
            if isinstance(x, list):
                self.X = x[0]
                self.Y = x[1]
                self.Z = x[2]
                self.repr = ['<Point>', x[0], x[1], x[2], '</Point>']
            else:
                self.X = x
                self.Y = y
                self.Z = z
                self.repr = ['<Point>', x, y, z, '</Point>']

    def __repr__(self):
        return str(self.repr)

    def __str__(self):
        return str(self.repr)


class Surface:
    """

    """
    def __init__(self, *args):
        if len(args) == 4:
            if isinstance(args[0], Point) and isinstance(args[1], Point) and isinstance(args[2], Point) and isinstance(args[3], Point):
                self.P1 = args[0]
                self.P2 = args[1]
                self.P3 = args[2]
                self.P4 = args[3]
                self.addSurface = "gCPy.Surface("+ str(args[0].X) + "," \
                                    + str(args[0].Y) + "," \
                                    + str(args[0].Z) + "," \
                                    + str(args[1].X) + "," \
                                    + str(args[1].Y) + "," \
                                    + str(args[1].Z) + "," \
                                    + str(args[2].X) + "," \
                                    + str(args[2].Y) + "," \
                                    + str(args[2].Z) + "," \
                                    + str(args[3].X) + "," \
                                    + str(args[3].Y) + "," \
                                    + str(args[3].Z) + "," \
                                                     + ")"
        elif len(args) == 2:
            if isinstance(args[0],Line) and isinstance(args[1], Line):
                pass
        else:
            print "you have to create surface from 4 points"


class Curve:
    """
    Adds new curve object
    """
    def __init__(self, *args):
        if len(args)>1 and args[0] == '<Curve>' and args[-1] == '</Curve>':
            if   args[1] == 'AddArc':       self.repr = ['<Curve>', 'AddArc'        , args[2], args[3], args[4], args[5]]
            elif args[1] == 'AddArc3Pt':    self.repr = ['<Curve>', 'AddArc3Pt'     , args[2], args[3], args[4], args[5]]
            elif args[1] == 'AddArcPtTanPt':self.repr = ['<Curve>', 'AddArcPtTanPt' , args[2], args[3], args[4], args[5]]
            elif args[1] == 'AddBlendCurve':self.repr = ['<Curve>','AddBlendCurve'  , args[2], args[3], args[4], args[5], args[6]]
            elif args[1] == 'AddCircle':    self.repr = ['<Curve>', 'AddCircle'     , args[2], args[3], args[4]]
            elif args[1] == 'AddCircle3Pt': self.repr = ['<Curve>', 'AddCircle3Pt'  , args[2], args[3], args[4], args[5]]
            elif args[1] == 'AddCurve':     self.repr = ['<Curve>', 'AddCurve'      , args[2], args[3], args[4]]
            elif args[1] == 'AddEllipse':   self.repr = ['<Curve>', 'AddEllipse'    , args[2], args[3], args[4], args[5]]
            elif args[1] == 'AddEllipse3Pt':self.repr = ['<Curve>', 'AddEllipse3Pt' , args[2], args[3], args[4], args[5]]
            elif args[1] == 'AddFilletCurve': self.repr=['<Curve>','AddFilletCurve' , args[2], args[3], args[4], args[5], args[6], args[7]]
            elif args[1] == 'AddInterpCrvOnSrf':
                pass
            elif args[1] == 'AddInterpCrvOnSrfUV':
                pass
            elif args[1] == 'AddInterpCurve':
                pass
            elif args[1] == 'AddLine':
                pass
            elif args[1] == 'AddNurbsCurve':
                pass
            elif args[1] == 'AddPolyline':
                pass
            elif args[1] == 'AddRectangle':
                pass
            elif args[1] == 'AddSpiral':
                pass
            elif args[1] == 'AddSubCrv':
                pass
            elif args[1] == 'CloseCurve':
                pass
            elif args[1] == 'ConvertCurveToPolyline':
                pass
            elif args[1] == 'CurveBooleanDifference':
                pass
            elif args[1] == 'CurveBooleanIntersection':
                pass
            elif args[1] == 'CurveBooleanUnion':
                pass
            elif args[1] == 'ExplodeCurves':
                pass
            elif args[1] == 'ExtendCurve':
                pass
            elif args[1] == 'ExtendCurveLength':
                pass
            elif args[1] == 'ExtendCurvePoint':
                pass
            elif args[1] == 'FitCurve':
                pass
            elif args[1] == 'JoinCurves':
                pass
            elif args[1] == 'MakeCurveNonPeriodic':
                pass
            elif args[1] == 'MeanCurve':
                pass
            elif args[1] == 'OffsetCurve':
                pass
            elif args[1] == 'OffsetCurveOnSurface':
                pass
            elif args[1] == 'ProjectCurveToMesh':
                pass
            elif args[1] == 'ProjectCurveToSurface':
                pass
            elif args[1] == 'SimplifyCurve':
                pass
            elif args[1] == 'SplitCurve':
                pass
            elif args[1] == 'TrimCurve':
                pass
            else:
                self.repr = "Curve of other Type"
        else:
            points = [Point(0., 0., 0.), Point(1., 1., 1.)]
            degree = 3
            self.repr = ['<Curve>','AddCurve',points, degree, '</Curve>'] 
    
    def __repr__(self):
        return str(self.repr)
        
    def __str__(self):
        return str(self.repr)


class Plane:
    pass


class Vector:
    pass


class Mesh:
    pass


if __name__ == '__main__':
    print __name__
