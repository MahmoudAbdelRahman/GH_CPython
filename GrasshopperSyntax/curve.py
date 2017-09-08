import Grasshopper02 as gh

def AddArc(plane, radius, angle_degrees):
    """Adds an arc curve to the document
    Parameters:
      plane = plane on which the arc will lie. The origin of the plane will be
        the center point of the arc. x-axis of the plane defines the 0 angle
        direction.
      radius = radius of the arc
      angle_degrees = interval of arc
    Returns:
      id of the new curve object
    """
    if not isinstance(plane, gh.Plane):
        raise Exception("plane should be an instance of Plane")
    elif not isinstance(radius, float):
        raise Exception("radius should be an instance of float")
    elif not isinstance(angle_degrees, float):
        raise Exception("angle_degrees should be an instance of float")
    else:
        rc = gh.Curve('<Curve>','AddArc', plane, radius, angle_degrees, '</Curve>')
        return rc


def AddArc3Pt(start, end, point_on_arc):
    """Adds a 3-point arc curve to the document
    Parameters:
      start, end = endpoints of the arc
      point_on_arc = a point on the arc
    Returns:
      id of the new curve object
    """
    if not (isinstance(start, gh.Point)):
        raise Exception("start should be an instance of a Point")
    elif not isinstance(end, gh.Point):
        raise Exception("end should be an instance of a Point")
    elif not isinstance(point_on_arc, gh.Point):
        raise Exception("point_on_arc should be an instance of a Point")
    else:
        rc = gh.Curve('<Curve>','AddArc3Pt', start, end, point_on_arc, '</Curve>')
        return rc


def AddArcPtTanPt(start, direction, end):
    """Adds an arc curve, created from a start point, a start direction, and an
    end point, to the document
    Returns:
      id of the new curve object
    """
    if not (isinstance(start, gh.Point)):
        raise Exception("start should be an instance of a gh.Point")
    elif not isinstance(end, gh.Point):
        raise Exception("end should be an instance of a gh.Point")
    elif not isinstance(direction, gh.Vector):
        raise Exception("direction should be an instance of a gh.Vector")
    else:
        rc = gh.Curve('<Curve>','AddArcPtTanPt', start, direction, end, '</Curve>')
        return rc


def AddBlendCurve(curves, parameters, reverses, continuities):
    """Makes a curve blend between two curves
    Parameters:
      curves = two curves
      parameters = two curve parameters defining the blend end points
      reverses = two boolean values specifying to use the natural or opposite direction of the curve
      continuities = two numbers specifying continuity at end points
        0 = position, 1 = tangency, 2 = curvature
    Returns:
      identifier of new curve on success
    """
    if not isinstance(curves, list) \
                            or len(curves) != 2 \
                            or not isinstance(curves[0], gh.Curve) \
                            or not isinstance(curves[1], gh.Curve):
        raise Exception("curves should be a list of two curves")
    elif not isinstance(parameters, list) \
                            or len(parameters)!=2 \
                            or not isinstance(parameters[0], float) \
                            or not isinstance(parameters[1], float):
        raise Exception("parameters should be a list of two floats defining the blend end points ")
    elif not isinstance(reverses, list) \
                            or len(reverses)!= 2 \
                            or not isinstance(reverses[0], bool) \
                            or not isinstance(reverses[1], bool):
        raise Exception("reverses should be a list of two boolean values specifying to use the natural or opposite direction of the curve")
    elif not isinstance(continuities, list) \
                            or len(continuities)!= 2 \
                            or not isinstance(continuities[0],int) \
                            or not isinstance(continuities[1], int)\
                            or continuities[0]>2 or continuities[1]>2\
                            or continuities[0]<0 or continuities[1]<0 :
        raise Exception("continuities should be a list of two numbers specifying continuity at end points 0 = position, 1 = tangency, 2 = curvature")
    else:
        rc = gh.Curve('<Curve>','AddBlendCurve', curves, parameters, reverses, continuities, '</Curve>')
        return rc

cc = AddBlendCurve([gh.Curve(), gh.Curve()],[1.3, 4.5], [True, False], [1, 0])
print cc


def AddCircle(plane_or_center, radius):
    """Adds a circle curve to the document
    Parameters:
      plane_or_center = plane on which the circle will lie. If a point is
        passed, this will be the center of the circle on the active
        construction plane
      radius = the radius of the circle
    Returns:
      id of the new curve object
    """

    return rc


def AddCircle3Pt(first, second, third):
    """Adds a 3-point circle curve to the document
    Parameters:
      first, second, third = points on the circle
    Returns:
      id of the new curve object
    """

    return rc


def AddCurve(points, degree=3):
    """Adds a control points curve object to the document
    Parameters:
      points = a list of points
      degree[opt] = degree of the curve
    Returns:
      id of the new curve object
    """

    return rc


def AddEllipse(plane, radiusX, radiusY):
    """Adds an elliptical curve to the document
    Parameters:
      plane = the plane on which the ellipse will lie. The origin of
              the plane will be the center of the ellipse
      radiusX, radiusY = radius in the X and Y axis directions
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddEllipse3Pt(center, second, third):
    """Adds a 3-point elliptical curve to the document
    Parameters:
      center = center point of the ellipse
      second = end point of the x axis
      third  = end point of the y axis
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddFilletCurve(curve0id, curve1id, radius=1.0, base_point0=None, base_point1=None):
    """Adds a fillet curve between two curve objects
    Parameters:
      curve0id = identifier of the first curve object
      curve1id = identifier of the second curve object
      radius [opt] = fillet radius
      base_point0 [opt] = base point of the first curve. If omitted,
                          starting point of the curve is used
      base_point1 [opt] = base point of the second curve. If omitted,
                          starting point of the curve is used
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddInterpCrvOnSrf(surface_id, points):
    """Adds an interpolated curve object that lies on a specified
    surface.  Note, this function will not create periodic curves,
    but it will create closed curves.
    Parameters:
      surface_id = identifier of the surface to create the curve on
      points = list of 3D points that lie on the specified surface.
               The list must contain at least 2 points
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddInterpCrvOnSrfUV(surface_id, points):
    """Adds an interpolated curve object based on surface parameters,
    that lies on a specified surface. Note, this function will not
    create periodic curves, but it will create closed curves.
    Parameters:
      surface_id = identifier of the surface to create the curve on
      points = list of 2D surface parameters. The list must contain
               at least 2 sets of parameters
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddInterpCurve(points, degree=3, knotstyle=0, start_tangent=None, end_tangent=None):
    """Adds an interpolated curve object to the document. Options exist to make
    a periodic curve or to specify the tangent at the endpoints. The resulting
    curve is a non-rational NURBS curve of the specified degree.
    Parameters:
      points = list containing 3D points to interpolate. For periodic curves,
          if the final point is a duplicate of the initial point, it is
          ignored. The number of control points must be >= (degree+1).
      degree[opt] = The degree of the curve (must be >=1).
          Periodic curves must have a degree >= 2. For knotstyle = 1 or 2,
          the degree must be 3. For knotstyle = 4 or 5, the degree must be odd
      knotstyle[opt]
          0 Uniform knots.  Parameter spacing between consecutive knots is 1.0.
          1 Chord length spacing.  Requires degree = 3 with arrCV1 and arrCVn1 specified.
          2 Sqrt (chord length).  Requires degree = 3 with arrCV1 and arrCVn1 specified.
          3 Periodic with uniform spacing.
          4 Periodic with chord length spacing.  Requires an odd degree value.
          5 Periodic with sqrt (chord length) spacing.  Requires an odd degree value.
      start_tangent [opt] = 3d vector that specifies a tangency condition at the
          beginning of the curve. If the curve is periodic, this argument must be omitted.
      end_tangent [opt] = 3d vector that specifies a tangency condition at the
          end of the curve. If the curve is periodic, this argument must be omitted.
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddLine(start, end):
    """Adds a line curve to the current model.
    Parameters:
      start, end = end points of the line
    Returns:
      id of the new curve object
    """

    return rc


def AddNurbsCurve(points, knots, degree, weights=None):
    """Adds a NURBS curve object to the document
    Parameters:
      points = list containing 3D control points
      knots = Knot values for the curve. The number of elements in knots must
          equal the number of elements in points plus degree minus 1
      degree = degree of the curve. must be greater than of equal to 1
      weights[opt] = weight values for the curve. Number of elements should
          equal the number of elements in points. Values must be greater than 0
    """

    return rc


def AddPolyline(points, replace_id=None):
    """Adds a polyline curve to the current model
    Parameters:
      points = list of 3D points. Duplicate, consecutive points will be
               removed. The list must contain at least two points. If the
               list contains less than four points, then the first point and
               last point must be different.
      replace_id[opt] = If set to the id of an existing object, the object
               will be replaced by this polyline
    Returns:
      id of the new curve object if successful
    """

    return rc


def AddRectangle(plane, width, height):
    """Add a rectangular curve to the document
    Paramters:
      plane = plane on which the rectangle will lie
      width, height = width and height of rectangle as measured along the plane's
        x and y axes
    Returns:
      id of new rectangle
    """

    return rc


def AddSpiral(point0, point1, pitch, turns, radius0, radius1=None):
    """Adds a spiral or helical curve to the document
    Parameters:
      point0 = helix axis start point or center of spiral
      point1 = helix axis end point or point normal on spiral plane
      pitch = distance between turns. If 0, then a spiral. If > 0 then the
              distance between helix "threads"
      turns = number of turns
      radius0, radius1 = starting and ending radius
    Returns:
      id of new curve on success
    """

    return rc


def AddSubCrv(curve_id, param0, param1):
    """Add a curve object based on a portion, or interval of an existing curve
    object. Similar in operation to Rhino's SubCrv command
    Parameters:
      curve_id = identifier of a closed planar curve object
      param0, param1 = first and second parameters on the source curve
    Returns:
      id of the new curve object if successful
    """

    return rc


def ArcAngle(curve_id, segment_index=-1):
    """Returns the angle of an arc curve object.
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      curve_id identifies a polycurve
    Returns:
      The angle in degrees if successful.
    """

    return arc.AngleDegrees


def ArcCenterPoint(curve_id, segment_index=-1):
    """Returns the center point of an arc curve object
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      curve_id identifies a polycurve
    Returns:
      The 3D center point of the arc if successful.
    """

    return arc.Center


def ArcMidPoint(curve_id, segment_index=-1):
    """Returns the mid point of an arc curve object
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      curve_id identifies a polycurve
    Returns:
      The 3D mid point of the arc if successful.
    """

    return arc.MidPoint


def ArcRadius(curve_id, segment_index=-1):
    """Returns the radius of an arc curve object
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      curve_id identifies a polycurve
    Returns:
      The radius of the arc if successful.
    """

    return arc.Radius

#Point
def CircleCenterPoint(curve_id, segment_index=-1, return_plane=False):
    """Returns the center point of a circle curve object
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      return_plane [opt] = if True, the circle's plane is returned
      curve_id identifies a polycurve
    Returns:
      The 3D center point of the circle if successful.
      The plane of the circle if return_plane is True
    """

    return circle.Center


def CircleCircumference(curve_id, segment_index=-1):
    """Returns the circumference of a circle curve object
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      curve_id identifies a polycurve
    Returns:
      The circumference of the circle if successful.
    """

    return circle.Circumference


def CircleRadius(curve_id, segment_index=-1):
    """Returns the radius of a circle curve object
    Parameters:
      curve_id = identifier of a curve object
      segment_index [opt] = identifies the curve segment if
      curve_id identifies a polycurve
    Returns:
      The radius of the circle if successful.
    """

    return circle.Radius


def CloseCurve(curve_id, tolerance=-1.0):
    """Closes an open curve object by making adjustments to the end points so
    they meet at a point
    Parameters:
      curve_id = identifier of a curve object
      tolerance[opt] = maximum allowable distance between start and end
          point. If omitted, the current absolute tolerance is used
    Returns:
      id of the new curve object if successful
    """

    return rc


def ClosedCurveOrientation(curve_id, direction=(0, 0, 1)):
    """Determine the orientation (counter-clockwise or clockwise) of a closed,
    planar curve
    Parameters:
      curve_id = identifier of a curve object
      direction[opt] = 3d vector that identifies up, or Z axs, direction of
          the plane to test against
    Returns:
      1 if the curve's orientation is clockwise
      -1 if the curve's orientation is counter-clockwise
      0 if unable to compute the curve's orientation
    """

    return int(orientation)


def ConvertCurveToPolyline(curve_id, angle_tolerance=5.0, tolerance=0.01, delete_input=False, min_edge_length=0,
                           max_edge_length=0):
    """Convert curve to a polyline curve
    Parameters:
      curve_id = identifier of a curve object
      angle_tolerance [opt] = The maximum angle between curve tangents at line
        endpoints. If omitted, the angle tolerance is set to 5.0.
      tolerance[opt] = The distance tolerance at segment midpoints. If omitted,
        the tolerance is set to 0.01.
      delete_input[opt] = Delete the curve object specified by curve_id. If
        omitted, curve_id will not be deleted.
      min_edge_length[opt] = Minimum segment length
      max_edge_length[opt] = Maximum segment length
    Returns:
      The new curve if successful.
    """

    return id

#point
def CurveArcLengthPoint(curve_id, length, from_start=True):
    """Returns the point on the curve that is a specified arc length
    from the start of the curve.
    Parameters:
      curve_id = identifier of a curve object
      length = The arc length from the start of the curve to evaluate.
      from_start[opt] = If not specified or True, then the arc length point is
          calculated from the start of the curve. If False, the arc length
          point is calculated from the end of the curve.
    Returns:
      Point3d if successful
    """



def CurveArea(curve_id):
    """Returns area of closed planar curves. The results are based on the
    current drawing units.
    Parameters:
      curve_id = The identifier of a closed, planar curve object.
    Returns:
      List of area information. The list will contain the following information:
        Element  Description
        0        The area. If more than one curve was specified, the
                 value will be the cumulative area.
        1        The absolute (+/-) error bound for the area.
    """

    return mp.Area, mp.AreaError


def CurveAreaCentroid(curve_id):
    """Returns area centroid of closed, planar curves. The results are based
    on the current drawing units.
    Parameters:
      curve_id = The identifier of a closed, planar curve object.
    Returns:
      Tuple of area centroid information containing the following information:
        Element  Description
        0        The 3d centroid point. If more than one curve was specified,
                 the value will be the cumulative area.
        1        A 3d vector with the absolute (+/-) error bound for the area
                 centroid.
    """

    return mp.Centroid, mp.CentroidError


def CurveArrows(curve_id, arrow_style=None):
    """Enables or disables a curve object's annotation arrows
    Parameters:
      curve_id = identifier of a curve
      arrow_style[opt] = the style of annotation arrow to be displayed
        0 = no arrows
        1 = display arrow at start of curve
        2 = display arrow at end of curve
        3 = display arrow at both start and end of curve
      Returns:
        if arrow_style is not specified, the current annotation arrow style
        if arrow_style is specified, the previos arrow style
    """



def CurveBooleanDifference(curve_id_0, curve_id_1):
    """Calculates the difference between two closed, planar curves and
    adds the results to the document. Note, curves must be coplanar.
    Parameters:
      curve_id_0 = identifier of the first curve object.
      curve_id_1 = identifier of the second curve object.
    Returns:
      The identifiers of the new objects if successful, None on error.
    """

    return curves


def CurveBooleanIntersection(curve_id_0, curve_id_1):
    """Calculates the intersection of two closed, planar curves and adds
    the results to the document. Note, curves must be coplanar.
    Parameters:
      curve_id_0 = identifier of the first curve object.
      curve_id_1 = identifier of the second curve object.
    Returns:
      The identifiers of the new objects.
    """

    return curves


def CurveBooleanUnion(curve_id):
    """Calculate the union of two or more closed, planar curves and
    add the results to the document. Note, curves must be coplanar.
    Parameters:
      curve_id = list of two or more close planar curves identifiers
    Returns:
      The identifiers of the new objects.
    """

    return curves


def CurveBrepIntersect(curve_id, brep_id, tolerance=None):
    """Intersects a curve object with a brep object. Note, unlike the
    CurveSurfaceIntersection function, this function works on trimmed surfaces.
    Parameters:
      curve_id = identifier of a curve object
      brep_id = identifier of a brep object
      tolerance [opt] = distance tolerance at segment midpoints.
                        If omitted, the current absolute tolerance is used.
    Returns:
      List of identifiers for the newly created intersection curve and
      point objects if successful. None on error.
    """

    return curves, points


def CurveClosestObject(curve_id, object_ids):
    """Returns the 3D point locations on two objects where they are closest to
    each other. Note, this function provides similar functionality to that of
    Rhino's ClosestPt command.
    Parameters:
      curve_id = identifier of the curve object to test
      object_ids = list of identifiers of point cloud, curve, surface, or
        polysurface to test against
    Returns:
      Tuple containing the results of the closest point calculation.
      The elements are as follows:
        0    The identifier of the closest object.
        1    The 3-D point that is closest to the closest object.
        2    The 3-D point that is closest to the test curve.
    """



def CurveClosestPoint(curve_id, test_point, segment_index=-1):
    """Returns parameter of the point on a curve that is closest to a test point.
    Parameters:
      curve_id = identifier of a curve object
      point = sampling point
      segment_index [opt] = curve segment if curve_id identifies a polycurve
    Returns:
      The parameter of the closest point on the curve
    """

    return t


def CurveContourPoints(curve_id, start_point, end_point, interval=None):
    """Returns the 3D point locations calculated by contouring a curve object.
    Parameters:
      curve_id = identifier of a curve object.
      start_point = 3D starting point of a center line.
      end_point = 3D ending point of a center line.
      interval [opt] = The distance between contour curves. If omitted,
      the interval will be equal to the diagonal distance of the object's
      bounding box divided by 50.
    Returns:
      A list of 3D points, one for each contour
    """

    return list(rc)


def CurveCurvature(curve_id, parameter):
    """Returns the curvature of a curve at a parameter. See the Rhino help for
    details on curve curvature
    Parameters:
      curve_id = identifier of the curve
      parameter = parameter to evaluate
    Returns:
      Tuple of curvature information on success
        element 0 = point at specified parameter
        element 1 = tangent vector
        element 2 = center of radius of curvature
        element 3 = radius of curvature
        element 4 = curvature vector
      None on failure
    """

    return point, tangent, center, radius, cv


def CurveCurveIntersection(curveA, curveB=None, tolerance=-1):
    """Calculates intersection of two curve objects.
    Parameters:
      curveA = identifier of the first curve object.
      curveB = identifier of the second curve object. If omitted, then a
               self-intersection test will be performed on curveA.
      tolerance [opt] = absolute tolerance in drawing units. If omitted,
                        the document's current absolute tolerance is used.
    Returns:
      List of tuples of intersection information if successful.
      The list will contain one or more of the following elements:
        Element Type     Description
        [n][0]  Number   The intersection event type, either Point (1) or Overlap (2).
        [n][1]  Point3d  If the event type is Point (1), then the intersection point
                         on the first curve. If the event type is Overlap (2), then
                         intersection start point on the first curve.
        [n][2]  Point3d  If the event type is Point (1), then the intersection point
                         on the first curve. If the event type is Overlap (2), then
                         intersection end point on the first curve.
        [n][3]  Point3d  If the event type is Point (1), then the intersection point
                         on the second curve. If the event type is Overlap (2), then
                         intersection start point on the second curve.
        [n][4]  Point3d  If the event type is Point (1), then the intersection point
                         on the second curve. If the event type is Overlap (2), then
                         intersection end point on the second curve.
        [n][5]  Number   If the event type is Point (1), then the first curve parameter.
                         If the event type is Overlap (2), then the start value of the
                         first curve parameter range.
        [n][6]  Number   If the event type is Point (1), then the first curve parameter.
                         If the event type is Overlap (2), then the end value of the
                         first curve parameter range.
        [n][7]  Number   If the event type is Point (1), then the second curve parameter.
                         If the event type is Overlap (2), then the start value of the
                         second curve parameter range.
        [n][8]  Number   If the event type is Point (1), then the second curve parameter.
                         If the event type is Overlap (2), then the end value of the
                         second curve parameter range.
    """



def CurveDegree(curve_id, segment_index=-1):
    """Returns the degree of a curve object.
    Parameters:
      curve_id = identifier of a curve object.
      segment_index [opt] = the curve segment if curve_id identifies a polycurve.
    Returns:
      The degree of the curve if successful. None on error.
    """

    return curve.Degree


def CurveDeviation(curve_a, curve_b):
    """Returns the minimum and maximum deviation between two curve objects
    Parameters:
      curve_a, curve_b = identifiers of two curves
    Returns:
      tuple of deviation information on success
        element 0 = curve_a parameter at maximum overlap distance point
        element 1 = curve_b parameter at maximum overlap distance point
        element 2 = maximum overlap distance
        element 3 = curve_a parameter at minimum overlap distance point
        element 4 = curve_b parameter at minimum overlap distance point
        element 5 = minimum distance between curves
      None on error
    """

    return maxa, maxb, maxd, mina, minb, mind


def CurveDim(curve_id, segment_index=-1):
    """Returns the dimension of a curve object
    Parameters:
      curve_id = identifier of a curve object.
      segment_index [opt] = the curve segment if curve_id identifies a polycurve.
    Returns:
      The dimension of the curve if successful. None on error.
    """

    return curve.Dimension


def CurveDirectionsMatch(curve_id_0, curve_id_1):
    """Tests if two curve objects are generally in the same direction or if they
    would be more in the same direction if one of them were flipped. When testing
    curve directions, both curves must be either open or closed - you cannot test
    one open curve and one closed curve.
    Parameters:
      curve_id_0 = identifier of first curve object
      curve_id_1 = identifier of second curve object
    Returns:
      True if the curve directions match, otherwise False.
    """

    return Rhino.Geometry.Curve.DoDirectionsMatch(curve0, curve1)


def CurveDiscontinuity(curve_id, style):
    """Search for a derivatitive, tangent, or curvature discontinuity in
    a curve object.
    Parameters:
      curve_id = identifier of curve object
      style = The type of continuity to test for. The types of
          continuity are as follows:
          Value    Description
          1        C0 - Continuous function
          2        C1 - Continuous first derivative
          3        C2 - Continuous first and second derivative
          4        G1 - Continuous unit tangent
          5        G2 - Continuous unit tangent and curvature
    Returns:
      List 3D points where the curve is discontinuous
    """

    return points


def CurveDomain(curve_id, segment_index=-1):
    """Returns the domain of a curve object.
    Parameters:
      curve_id = identifier of the curve object
      segment_index[opt] = the curve segment if curve_id identifies a polycurve.
    """

    return [dom.Min, dom.Max]


def CurveEditPoints(curve_id, return_parameters=False, segment_index=-1):
    """Returns the edit, or Greville, points of a curve object.
    For each curve control point, there is a corresponding edit point.
    Parameters:
      curve_id = identifier of the curve object
      return_parameters[opt] = if True, return as a list of curve parameters.
        If False, return as a list of 3d points
      segment_index[opt] = the curve segment is curve_id identifies a polycurve
    Returns:
      curve parameters of 3d points on success
      None on error
    """

    return nc.GrevillePoints()


def CurveEndPoint(curve_id, segment_index=-1):
    """Returns the end point of a curve object
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The 3-D end point of the curve if successful.
    """

    return curve.PointAtEnd


def CurveFilletPoints(curve_id_0, curve_id_1, radius=1.0, base_point_0=None, base_point_1=None, return_points=True):
    """Find points at which to cut a pair of curves so that a fillet of a
    specified radius fits. A fillet point is a pair of points (point0, point1)
    such that there is a circle of radius tangent to curve curve0 at point0 and
    tangent to curve curve1 at point1. Of all possible fillet points, this
    function returns the one which is the closest to the base point base_point_0,
    base_point_1. Distance from the base point is measured by the sum of arc
    lengths along the two curves.
    Parameters:
      curve_id_0 = identifier of the first curve object.
      curve_id_1 = identifier of the second curve object.
      radius [opt] = The fillet radius. If omitted, a radius
                     of 1.0 is specified.
      base_point_0 [opt] = The base point on the first curve.
                     If omitted, the starting point of the curve is used.
      base_point_1 [opt] = The base point on the second curve. If omitted,
                     the starting point of the curve is used.
      return_points [opt] = If True (Default), then fillet points are
                     returned. Otherwise, a fillet curve is created and
                     it's identifier is returned.
    Returns:
      If return_points is True, then a list of point and vector values
      if successful. The list elements are as follows:

      0    A point on the first curve at which to cut (arrPoint0).
      1    A point on the second curve at which to cut (arrPoint1).
      2    The fillet plane's origin (3-D point). This point is also
           the center point of the fillet
      3    The fillet plane's X axis (3-D vector).
      4    The fillet plane's Y axis (3-D vector).
      5    The fillet plane's Z axis (3-D vector).

      If return_points is False, then the identifier of the fillet curve
      if successful.
      None if not successful, or on error.
    """

    return scriptcontext.errorhandler()


def CurveFrame(curve_id, parameter, segment_index=-1):
    """Returns the plane at a parameter of a curve. The plane is based on the
    tangent and curvature vectors at a parameter.
    Parameters:
      curve_id = identifier of the curve object.
      parameter = parameter to evaluate.
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The plane at the specified parameter if successful.
      None if not successful, or on error.
    """

    return scriptcontext.errorhandler()


def CurveKnotCount(curve_id, segment_index=-1):
    """Returns the knot count of a curve object.
    Parameters:
      curve_id = identifier of the curve object.
      segment_index [opt] = the curve segment if curve_id identifies a polycurve.
    Returns:
      The number of knots if successful.
      None if not successful or on error.
    """

    return nc.Knots.Count


def CurveKnots(curve_id, segment_index=-1):
    """Returns the knots, or knot vector, of a curve object
    Parameters:
      curve_id = identifier of the curve object.
      segment_index [opt] = the curve segment if curve_id identifies a polycurve.
    Returns:
      knot values if successful.
      None if not successful or on error.
    """

    return rc


def CurveLength(curve_id, segment_index=-1, sub_domain=None):
    """Returns the length of a curve object.
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
      sub_domain [opt] = list of two numbers identifing the sub-domain of the
          curve on which the calculation will be performed. The two parameters
          (sub-domain) must be non-decreasing. If omitted, the length of the
          entire curve is returned.
    Returns:
      The length of the curve if successful.
      None if not successful, or on error.
    """

    return curve.GetLength()


def CurveMidPoint(curve_id, segment_index=-1):
    """Returns the mid point of a curve object.
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The 3D mid point of the curve if successful.
      None if not successful, or on error.
    """

    return scriptcontext.errorhandler()


def CurveNormal(curve_id, segment_index=-1):
    """Returns the normal direction of the plane in which a planar curve object lies.
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The 3D normal vector if sucessful.
      None if not successful, or on error.
    """

    return scriptcontext.errorhandler()


def CurveNormalizedParameter(curve_id, parameter):
    """Converts a curve parameter to a normalized curve parameter;
    one that ranges between 0-1
    Parameters:
      curve_id = identifier of the curve object
      parameter = the curve parameter to convert
    Returns:
      normalized curve parameter
    """

    return curve.Domain.NormalizedParameterAt(parameter)


def CurveParameter(curve_id, parameter):
    """Converts a normalized curve parameter to a curve parameter;
    one within the curve's domain
    Parameters:
      curve_id = identifier of the curve object
      parameter = the normalized curve parameter to convert
    Returns:
      curve parameter
    """

    return curve.Domain.ParameterAt(parameter)


def CurvePerpFrame(curve_id, parameter):
    """Returns the perpendicular plane at a parameter of a curve. The result
    is relatively parallel (zero-twisting) plane
    Parameters:
      curve_id = identifier of the curve object
      parameter = parameter to evaluate
    Returns:
      Plane on success
      None on error
    """

    if rc: return plane


def CurvePlane(curve_id, segment_index=-1):
    """Returns the plane in which a planar curve lies. Note, this function works
    only on planar curves.
    Parameters:
      curve_id = identifier of the curve object
      segment_index[opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The plane in which the curve lies if successful.
      None if not successful, or on error.
    """

    return scriptcontext.errorhandler()


def CurvePointCount(curve_id, segment_index=-1):
    """Returns the control points count of a curve object.
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      Number of control points if successful.
      None if not successful
    """



def CurvePoints(curve_id, segment_index=-1):
    """Returns the control points, or control vertices, of a curve object.
    If the curve is a rational NURBS curve, the euclidean control vertices
    are returned.
    """



def CurveRadius(curve_id, test_point, segment_index=-1):
    """Returns the radius of curvature at a point on a curve.
    Parameters:
      curve_id = identifier of the curve object
      test_point = sampling point
      segment_index[opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The radius of curvature at the point on the curve if successful.
      None if not successful, or on error.
    """



def CurveSeam(curve_id, parameter):
    """Adjusts the seam, or start/end, point of a closed curve.
    Parameters:
      curve_id = identifier of the curve object
      parameter = The parameter of the new start/end point.
                  Note, if successful, the resulting curve's
                  domain will start at dblParameter.
    Returns:
      True or False indicating success or failure.
    """



def CurveStartPoint(curve_id, segment_index=-1, point=None):
    """Returns the start point of a curve object
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
      point [opt] = new start point
    Returns:
      The 3D starting point of the curve if successful.
    """



def CurveSurfaceIntersection(curve_id, surface_id, tolerance=-1, angle_tolerance=-1):
    """Calculates intersection of a curve object with a surface object.
    Note, this function works on the untrimmed portion of the surface.
    Parameters:
      curve_id = The identifier of the first curve object.
      surface_id = The identifier of the second curve object. If omitted,
          the a self-intersection test will be performed on curve.
      tolerance [opt] = The absolute tolerance in drawing units. If omitted,
          the document's current absolute tolerance is used.
      angle_tolerance [opt] = angle tolerance in degrees. The angle
          tolerance is used to determine when the curve is tangent to the
          surface. If omitted, the document's current angle tolerance is used.
    Returns:
      Two-dimensional list of intersection information if successful.
      The list will contain one or more of the following elements:
        Element Type     Description
        (n, 0)  Number   The intersection event type, either Point(1) or Overlap(2).
        (n, 1)  Point3d  If the event type is Point(1), then the intersection point
                         on the first curve. If the event type is Overlap(2), then
                         intersection start point on the first curve.
        (n, 2)  Point3d  If the event type is Point(1), then the intersection point
                         on the first curve. If the event type is Overlap(2), then
                         intersection end point on the first curve.
        (n, 3)  Point3d  If the event type is Point(1), then the intersection point
                         on the second curve. If the event type is Overlap(2), then
                         intersection start point on the surface.
        (n, 4)  Point3d  If the event type is Point(1), then the intersection point
                         on the second curve. If the event type is Overlap(2), then
                         intersection end point on the surface.
        (n, 5)  Number   If the event type is Point(1), then the first curve parameter.
                         If the event type is Overlap(2), then the start value of the
                         first curve parameter range.
        (n, 6)  Number   If the event type is Point(1), then the first curve parameter.
                         If the event type is Overlap(2), then the end value of the
                         curve parameter range.
        (n, 7)  Number   If the event type is Point(1), then the U surface parameter.
                         If the event type is Overlap(2), then the U surface parameter
                         for curve at (n, 5).
        (n, 8)  Number   If the event type is Point(1), then the V surface parameter.
                         If the event type is Overlap(2), then the V surface parameter
                         for curve at (n, 5).
        (n, 9)  Number   If the event type is Point(1), then the U surface parameter.
                         If the event type is Overlap(2), then the U surface parameter
                         for curve at (n, 6).
        (n, 10) Number   If the event type is Point(1), then the V surface parameter.
                         If the event type is Overlap(2), then the V surface parameter
                         for curve at (n, 6).
    """



def CurveTangent(curve_id, parameter, segment_index=-1):
    """Returns a 3D vector that is the tangent to a curve at a parameter.
    Parameters:
      curve_id = identifier of the curve object
      parameter = parameter to evaluate
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      A 3D vector if successful.
      None on error.
    """



def CurveWeights(curve_id, segment_index=-1):
    """Returns list of weights that are assigned to the control points of a curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index[opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      The weight values of the curve if successful.
      None if not successful, or on error.
    """



def DivideCurve(curve_id, segments, create_points=False, return_points=True):
    """Divides a curve object into a specified number of segments.
    Parameters:
      curve_id = identifier of the curve object
      segments = The number of segments.
      create_points [opt] = Create the division points. If omitted or False,
          points are not created.
      return_points [opt] = If omitted or True, points are returned.
          If False, then a list of curve parameters are returned.
    Returns:
      If return_points is not specified or True, then a list containing 3D
      division points.
      If return_points is False, then an array containing division curve
      parameters.
      None if not successful, or on error.
    """



def DivideCurveEquidistant(curve_id, distance, create_points=False, return_points=True):
    """Divides a curve such that the linear distance between the points is equal.
    Parameters:
      curve_id = the object's identifier
      distance = linear distance between division points
      create_points[opt] = create the division points
      return_points[opt] = If True, return a list of points.
          If False, return a list of curve parameters
    Returns:
      A list of points or curve parameters based on the value of return_points
      None on error
    """



def DivideCurveLength(curve_id, length, create_points=False, return_points=True):
    """Divides a curve object into segments of a specified length.
    Parameters:
      curve_id = identifier of the curve object
      length = The length of each segment.
      create_points [opt] = Create the division points. If omitted or False,
          points are not created.
      return_points [opt] = If omitted or True, points are returned.
          If False, then a list of curve parameters are returned.
    Returns:
      If return_points is not specified or True, then a list containing 3D
      division points if successful.
      If return_points is False, then an array containing division curve
      parameters if successful.
      None if not successful, or on error.
    """



def EllipseCenterPoint(curve_id):
    """Returns the center point of an elliptical-shaped curve object.
    Parameters:
      curve_id = identifier of the curve object.
    Returns:
      The 3D center point of the ellipse if successful.
    """



def EllipseQuadPoints(curve_id):
    """Returns the quadrant points of an elliptical-shaped curve object.
    Parameters:
      curve_id = identifier of the curve object.
    Returns:
      Four 3D points identifying the quadrants of the ellipse
    """



def EvaluateCurve(curve_id, t, segment_index=-1):
    """Evaluates a curve at a parameter and returns a 3D point
    Parameters:
      curve_id = identifier of the curve object
      t = the parameter to evaluate
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    """



def ExplodeCurves(curve_ids, delete_input=False):
    """Explodes, or un-joins, one curves. Polycurves will be exploded into curve
    segments. Polylines will be exploded into line segments. ExplodeCurves will
    return the curves in topological order.
    Parameters:
      curve_ids = the curve object(s) to explode.
      delete_input[opt] = Delete input objects after exploding.
    Returns:
      List identifying the newly created curve objects
    """



def ExtendCurve(curve_id, extension_type, side, boundary_object_ids):
    """Extends a non-closed curve object by a line, arc, or smooth extension
    until it intersects a collection of objects.
    Parameters:
      curve_id: identifier of curve to extend
      extension_type: 0 = line, 1 = arc, 2 = smooth
      side: 0=extend from the start of the curve, 1=extend from the end of the curve
      boundary_object_ids: curve, surface, and polysurface objects to extend to
    Returns:
      The identifier of the new object if successful.
      None if not successful
    """



def ExtendCurveLength(curve_id, extension_type, side, length):
    """Extends a non-closed curve by a line, arc, or smooth extension for a
    specified distance
    Parameters:
      curve_id: curve to extend
      extension_type: 0 = line, 1 = arc, 2 = smooth
      side: 0=extend from start of the curve, 1=extend from end of the curve
      length: distance to extend
    Returns:
      The identifier of the new object
      None if not successful
    """



def ExtendCurvePoint(curve_id, side, point):
    """Extends a non-closed curve by smooth extension to a point
    Parameters:
      curve_id: curve to extend
      side: 0=extend from start of the curve, 1=extend from end of the curve
      point: point to extend to
    Returns:
      The identifier of the new object if successful.
      None if not successful, or on error.
    """



def FairCurve(curve_id, tolerance=1.0):
    """Fairs a curve. Fair works best on degree 3 (cubic) curves. Fair attempts
    to remove large curvature variations while limiting the geometry changes to
    be no more than the specified tolerance. Sometimes several applications of
    this method are necessary to remove nasty curvature problems.
    Parameters:
      curve_id = curve to fair
      tolerance[opt] = fairing tolerance
    Returns:
      True or False indicating success or failure
    """



def FitCurve(curve_id, degree=3, distance_tolerance=-1, angle_tolerance=-1):
    """Reduces number of curve control points while maintaining the curve's same
    general shape. Use this function for replacing curves with many control
    points. For more information, see the Rhino help for the FitCrv command.
    Parameters:
      curve_id = Identifier of the curve object
      degree [opt] = The curve degree, which must be greater than 1.
                     The default is 3.
      distance_tolerance [opt] = The fitting tolerance. If distance_tolerance
          is not specified or <= 0.0, the document absolute tolerance is used.
      angle_tolerance [opt] = The kink smoothing tolerance in degrees. If
          angle_tolerance is 0.0, all kinks are smoothed. If angle_tolerance
          is > 0.0, kinks smaller than angle_tolerance are smoothed. If
          angle_tolerance is not specified or < 0.0, the document angle
          tolerance is used for the kink smoothing.
    Returns:
      The identifier of the new object
      None if not successful, or on error.
    """



def InsertCurveKnot(curve_id, parameter, symmetrical=False):
    """Inserts a knot into a curve object
    Parameters:
      curve_id = identifier of the curve object
      parameter = parameter on the curve
      symmetrical[opt] = if True, then knots are added on both sides of
          the center of the curve
    Returns:
      True or False indicating success or failure
    """



def IsArc(curve_id, segment_index=-1):
    """Verifies an object is an arc curve
    Parameters:
      curve_id = Identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False
    """


def IsCircle(curve_id, tolerance=None):
    """Verifies an object is a circle curve
    Parameters:
      curve_id = Identifier of the curve object
      tolerance [opt] = If the curve is not a circle, then the tolerance used
        to determine whether or not the NURBS form of the curve has the
        properties of a circle. If omitted, Rhino's internal zero tolerance is used
    Returns:
      True or False
    """


def IsCurve(object_id):
    "Verifies an object is a curve"


def IsCurveClosable(curve_id, tolerance=None):
    """Decide if it makes sense to close off the curve by moving the end point
    to the start point based on start-end gap size and length of curve as
    approximated by chord defined by 6 points
    Parameters:
      curve_id = identifier of the curve object
      tolerance[opt] = maximum allowable distance between start point and end
        point. If omitted, the document's current absolute tolerance is used
    Returns:
      True or False
    """


def IsCurveClosed(object_id):
    return " "

def IsCurveInPlane(object_id, plane=None):
    """Test a curve to see if it lies in a specific plane
    Parameters:
      object_id = the object's identifier
      plane[opt] = plane to test. If omitted, the active construction plane is used
    Returns:
      True or False
    """



def IsCurveLinear(object_id, segment_index=-1):
    """Verifies an object is a linear curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False indicating success or failure
    """


def IsCurvePeriodic(curve_id, segment_index=-1):
    """Verifies an object is a periodic curve object
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False
    """


def IsCurvePlanar(curve_id, segment_index=-1):
    """Verifies an object is a planar curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False indicating success or failure
    """


def IsCurveRational(curve_id, segment_index=-1):
    """Verifies an object is a rational NURBS curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False indicating success or failure
    """


def IsEllipse(object_id, segment_index=-1):
    """Verifies an object is an elliptical-shaped curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False indicating success or failure
    """


def IsLine(object_id, segment_index=-1):
    """Verifies an object is a line curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False indicating success or failure
    """


def IsPointOnCurve(object_id, point, segment_index=-1):
    """Verifies that a point is on a curve
    Parameters:
      curve_id = identifier of the curve object
      point = the test point
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False indicating success or failure
    """


def IsPolyCurve(object_id, segment_index=-1):
    """Verifies an object is a PolyCurve curve
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False
    """


def IsPolyline(object_id, segment_index=-1):
    """Verifies an object is a Polyline curve object
    Parameters:
      curve_id = identifier of the curve object
      segment_index [opt] = the curve segment if curve_id identifies a polycurve
    Returns:
      True or False
    """


def JoinCurves(object_ids, delete_input=False, tolerance=None):
    """Joins multiple curves together to form one or more curves or polycurves
    Parameters:
      object_ids = list of multiple curves
      delete_input[opt] = delete input objects after joining
      tolerance[opt] = join tolerance. If omitted, 2.1 * document absolute
          tolerance is used
    Returns:
      List of Guids representing the new curves
    """



def LineFitFromPoints(points):
    """Returns a line that was fit through an array of 3D points
    Parameters:
      points = a list of at least two 3D points
    Returns:
      line on success
    """


def MakeCurveNonPeriodic(curve_id, delete_input=False):
    """Makes a periodic curve non-periodic. Non-periodic curves can develop
    kinks when deformed
    Parameters:
      curve_id = identifier of the curve object
      delete_input[opt] = delete the input curve
    Returns:
      id of the new or modified curve if successful
      None on error
    """


def MeanCurve(curve0, curve1, tolerance=None):
    """Creates an average curve from two curves
    Parameters:
      curve0, curve1 = identifiers of two curves
      tolerance[opt] = angle tolerance used to match kinks between curves
    Returns:
      id of the new or modified curve if successful
      None on error
    """



def MeshPolyline(polyline_id):
    """Creates a polygon mesh object based on a closed polyline curve object.
    The created mesh object is added to the document
    Parameters:
      polyline_id = identifier of the polyline curve object
    Returns:
      identifier of the new mesh object
      None on error
    """


def OffsetCurve(object_id, direction, distance, normal=None, style=1):
    """Offsets a curve by a distance. The offset curve will be added to Rhino
    Parameters:
      object_id = identifier of a curve object
      direction = point describing direction of the offset
      distance = distance of the offset
      normal[opt] = normal of the plane in which the offset will occur.
          If omitted, the normal of the active construction plane will be used
      style[opt] = the corner style
          0 = None, 1 = Sharp, 2 = Round, 3 = Smooth, 4 = Chamfer
    Returns:
      List of ids for the new curves on success
      None on error
    """



def OffsetCurveOnSurface(curve_id, surface_id, distance_or_parameter):
    """Offset a curve on a surface. The source curve must lie on the surface.
    The offset curve or curves will be added to Rhino
    Parameters:
      curve_id, surface_id = curve and surface identifiers
      distance_or_parameter = If a single number is passed, then this is the
        distance of the offset. Based on the curve's direction, a positive value
        will offset to the left and a negative value will offset to the right.
        If a tuple of two values is passed, this is interpreted as the surface
        U,V parameter that the curve will be offset through
    Returns:
      Identifiers of the new curves if successful
      None on error
    """



def PlanarClosedCurveContainment(curve_a, curve_b, plane=None, tolerance=None):
    """Determines the relationship between the regions bounded by two coplanar
    simple closed curves
    Parameters:
      curve_a, curve_b = identifiers of two planar, closed curves
      plane[opt] = test plane. If omitted, the currently active construction
        plane is used
      tolerance[opt] = if omitted, the document absolute tolerance is used
    Returns:
      a number identifying the relationship if successful
        0 = the regions bounded by the curves are disjoint
        1 = the two curves intersect
        2 = the region bounded by curve_a is inside of curve_b
        3 = the region bounded by curve_b is inside of curve_a
      None if not successful
    """


def PlanarCurveCollision(curve_a, curve_b, plane=None, tolerance=None):
    """Determines if two coplanar curves intersect
    Parameters:
      curve_a, curve_b = identifiers of two planar curves
      plane[opt] = test plane. If omitted, the currently active construction
        plane is used
      tolerance[opt] = if omitted, the document absolute tolerance is used
    Returns:
      True if the curves intersect; otherwise False
    """



def PointInPlanarClosedCurve(point, curve, plane=None, tolerance=None):
    """Determines if a point is inside of a closed curve, on a closed curve, or
    outside of a closed curve
    Parameters:
      point = text point
      curve = identifier of a curve object
      plane[opt] = plane containing the closed curve and point. If omitted,
          the currently active construction plane is used
      tolerance[opt] = it omitted, the document abosulte tolerance is used
    Returns:
      number identifying the result if successful
          0 = point is outside of the curve
          1 = point is inside of the curve
          2 = point in on the curve
    """



def PolyCurveCount(curve_id, segment_index=-1):
    """Returns the number of curve segments that make up a polycurve"""



def PolylineVertices(curve_id, segment_index=-1):
    "Returns the vertices of a polyline curve on success"



def ProjectCurveToMesh(curve_ids, mesh_ids, direction):
    """Projects one or more curves onto one or more surfaces or meshes
    Parameters:
      curve_ids = identifiers of curves to project
      mesh_ids = identifiers of meshes to project onto
      direction = projection direction
    Returns:
      list of identifiers
    """



def ProjectCurveToSurface(curve_ids, surface_ids, direction):
    """Projects one or more curves onto one or more surfaces or polysurfaces
    Parameters:
      curve_ids = identifiers of curves to project
      surface_ids = identifiers of surfaces to project onto
      direction = projection direction
    Returns:
      list of identifiers
    """



def RebuildCurve(curve_id, degree=3, point_count=10):
    """Rebuilds a curve to a given degree and control point count. For more
    information, see the Rhino help for the Rebuild command.
    Parameters:
      curve_id = identifier of the curve object
      degree[opt] = new degree (must be greater than 0)
      point_count [opt] = new point count, which must be bigger than degree.
    Returns:
      True of False indicating success or failure
    """


def ReverseCurve(curve_id):
    """Reverses the direction of a curve object. Same as Rhino's Dir command
    Parameters:
      curve_id = identifier of the curve object
    Returns:
      True or False indicating success or failure
    """



def SimplifyCurve(curve_id, flags=0):
    "Replace a curve with a geometrically equivalent polycurve"



def SplitCurve(curve_id, parameter, delete_input=True):
    """Splits, or divides, a curve at a specified parameter. The parameter must
    be in the interior of the curve's domain
    Parameters:
      curve_id = the curve to split
      parameter = one or more parameters to split the curve at
      delete_input[opt] = delete the input curve
    Returns:
      list of new curves on success
      None on error
    """



def TrimCurve(curve_id, interval, delete_input=True):
    """Trims a curve by removing portions of the curve outside a specified interval
    Paramters:
      curve_id = the curve to trim
      interval = two numbers indentifying the interval to keep. Portions of
        the curve before domain[0] and after domain[1] will be removed. If the
        input curve is open, the interval must be increasing. If the input
        curve is closed and the interval is decreasing, then the portion of
        the curve across the start and end of the curve is returned
      delete_input[opt] = delete the input curve
    Reutrns:
      identifier of the new curve on success
      None on failure
    """

