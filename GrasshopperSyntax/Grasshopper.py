# Grasshopper Module Version 0.0.1.5

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


class Doc:
    def __init__(self):
        self.DisplayName = "gCPy.Doc(DocDisplayName)"
        self.FilePath = "gCPy.Doc(DocFilePath)"


class Line:
    def __init__(self, *args):
        """Adds new line using two input points, or two input lists or 6  input doubles

        :param args:
        """
        if len(args) == 2:
            if isinstance(args[0], Point) and isinstance(args[1], Point):
                print "2 Point Instances"
                self.X1 = args[0].X
                self.Y1 = args[0].Y
                self.Z1 = args[0].Z
                self.X2 = args[1].X
                self.Y2 = args[1].Y
                self.Z2 = args[1].Z
            elif isinstance(args[0], list) and isinstance(args[1], list) and args[0][0] != '<Point>' and args[1][0] != '<Point>':
                print "2 List Instances"
                self.X1 = args[0][0]
                self.Y1 = args[0][1]
                self.Z1 = args[0][2]
                self.X2 = args[1][0]
                self.Y2 = args[1][1]
                self.Z2 = args[1][2]
            elif isinstance(args[0], list) and isinstance(args[1], list) and args[0][0] == '<Point>' and args[1][0] == '<Point>':
                print "2 List Instances of type 2"
                self.X1 = args[0][1]
                self.Y1 = args[0][2]
                self.Z1 = args[0][3]
                self.X2 = args[1][1]
                self.Y2 = args[1][2]
                self.Z2 = args[1][3]
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

    def addLine(self):
        return ['<Line>',self.X1, self.Y1, self.Z1, self.X2, self.Y2, self.Z2, '<Line>']

    def length(self):
        return ((self.X2 - self.X1) ** 2 + (self.Y2 - self.Y1) ** 2 + (self.Z2 - self.Z1) ** 2) ** 0.5

    def pointOnLine(self, parameter=0.5):
        return Point((self.X2 - self.X1) * parameter + self.X1, \
                     (self.Y2 - self.Y1) * parameter + self.Y1, \
                     (self.Z2 - self.Z1) * parameter + self.Z1)

    def __repr__(self):
        return ['<Line>',self.X1, self.Y1, self.Z1, self.X2, self.Y2, self.Z2, '</Line>']

    def __str__(self):
        return str(['<Line>',self.X1, self.Y1, self.Z1, self.X2, self.Y2, self.Z2, '</Line>'])


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
            else:
                self.X = x
                self.Y = y
                self.Z = z
        elif version == 3:
            if isinstance(x, list):
                self.X = x[0]
                self.Y = x[1]
                self.Z = x[2]
            else:
                self.X = x
                self.Y = y
                self.Z = z
        self.addPoint = ['<Point>', x, y, z, '</Point>']

    def __repr__(self):
        return ['<Point>', self.X, self.Y, self.Z, '</Point>']

    def __str__(self):
        return str(['<Point>', self.X, self.Y, self.Z, '</Point>'])


########################### DEFINE METHODS ################################

def addLine(*args):
    """Adds new line between two points
    :param: args
        pointa, pointb (Point3): instance of points
        [x1, y1, z1], [x2, y2, z2]: two points as lists
        x1, y1, z1, x2, y2, z2 : 6 doubles represent coordinates of line end points.
    :return:
    """
    if len(args) == 2:
        return Line(args[0], args[1])
    elif len(args) == 6:
        return Line(args[0], args[1], args[2], args[3], args[4], args[5])


def addPoint(*args):
    """

    :param args:
    :return:
    """
    if len(args) == 1:
        return Point(args[0])
    elif len(args) == 3:
        return Point(args[0], args[1], args[2]).addPoint


##################################vars#################################

doc = Doc();

if __name__ == '__main__':
    print __name__
