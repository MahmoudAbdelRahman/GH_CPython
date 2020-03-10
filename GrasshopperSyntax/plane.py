
def DistanceToPlane(plane, point):
    """Returns the distance from a 3D point to a plane
    Parameters:
      plane (plane): the plane
      point (point): List of 3 numbers or Point3d
    Returns:
      number: The distance if successful, otherwise None
    Example:
      import rhinoscriptsyntax as rs
      point = rs.GetPoint("Point to test")
      if point:
      plane = rs.ViewCPlane()
      if plane:
      distance = rs.DistanceToPlane(plane, point)
      if distance is not None:
      print("Distance to plane: ", distance)
    See Also:
      Distance
      PlaneClosestPoint
    """



def EvaluatePlane(plane, parameter):
    """Evaluates a plane at a U,V parameter
    Parameters:
      plane (plane): the plane to evaluate
      parameter ([number, number]): list of two numbers defining the U,V parameter to evaluate
    Returns:
      point: Point3d on success
    Example:
      import rhinoscriptsyntax as rs
      view = rs.CurrentView()
      plane = rs.ViewCPlane(view)
      point = rs.EvaluatePlane(plane, (5,5))
      rs.AddPoint( point )
    See Also:
      PlaneClosestPoint
    """



def IntersectPlanes(plane1, plane2, plane3):
    """Calculates the intersection of three planes
    Parameters:
      plane1 (plane): the 1st plane to intersect
      plane2 (plane): the 2nd plane to intersect
      plane3 (plane): the 3rd plane to intersect
    Returns:
      point: the intersection point between the 3 planes on success
      None: on error
    Example:
      import rhinoscriptsyntax as rs
      plane1 = rs.WorldXYPlane()
      plane2 = rs.WorldYZPlane()
      plane3 = rs.WorldZXPlane()
      point = rs.IntersectPlanes(plane1, plane2, plane3)
      if point: rs.AddPoint(point)
    See Also:
      LineLineIntersection
      LinePlaneIntersection
      PlanePlaneIntersection
    """



def MovePlane(plane, origin):
    """Moves the origin of a plane
    Parameters:
      plane (plane): Plane or ConstructionPlane
      origin (point): Point3d or list of three numbers
    Returns:
      plane: moved plane
    Example:
      import rhinoscriptsyntax as rs
      origin = rs.GetPoint("CPlane origin")
      if origin:
      plane = rs.ViewCPlane()
      plane = rs.MovePlane(plane,origin)
      rs.ViewCplane(plane)
    See Also:
      PlaneFromFrame
      PlaneFromNormal
      RotatePlane
    """



def PlaneClosestPoint(plane, point, return_point=True):
    """Returns the point on a plane that is closest to a test point.
    Parameters:
      plane (plane): The plane
      point (point): The 3-D point to test.
      return_point (bool, optional): If omitted or True, then the point on the plane
         that is closest to the test point is returned. If False, then the
         parameter of the point on the plane that is closest to the test
         point is returned.
    Returns:
      point: If return_point is omitted or True, then the 3-D point
      point: If return_point is False, then an array containing the U,V parameters
      of the point
      None: if not successful, or on error.
    Example:
      import rhinoscriptsyntax as rs
      point = rs.GetPoint("Point to test")
      if point:
      plane = rs.ViewCPlane()
      if plane:
      print(rs.PlaneClosestPoint(plane, point))
    See Also:
      DistanceToPlane
      EvaluatePlane
    """



def PlaneCurveIntersection(plane, curve, tolerance=None):
    """Intersect an infinite plane and a curve object
    Parameters:
      plane (plane): The plane to intersect.
      curve (guid): The identifier of the curve object
      torerance (number, optional): The intersection tolerance. If omitted, the document's absolute tolerance is used.
    Returns:
      A list of intersection information tuple if successful.  The list will contain one or more of the following tuple:

        Element Type        Description

        [0]       Number      The intersection event type, either Point (1) or Overlap (2).

        [1]       Point3d     If the event type is Point (1), then the intersection point on the curve.
                            If the event type is Overlap (2), then intersection start point on the curve.

        [2]       Point3d     If the event type is Point (1), then the intersection point on the curve.
                            If the event type is Overlap (2), then intersection end point on the curve.

        [3]       Point3d     If the event type is Point (1), then the intersection point on the plane.
                            If the event type is Overlap (2), then intersection start point on the plane.

        [4]       Point3d     If the event type is Point (1), then the intersection point on the plane.

                            If the event type is Overlap (2), then intersection end point on the plane.

        [5]       Number      If the event type is Point (1), then the curve parameter.
                            If the event type is Overlap (2), then the start value of the curve parameter range.

        [6]       Number      If the event type is Point (1), then the curve parameter.
                            If the event type is Overlap (2),  then the end value of the curve parameter range.

        [7]       Number      If the event type is Point (1), then the U plane parameter.
                            If the event type is Overlap (2), then the U plane parameter for curve at (n, 5).

        [8]       Number      If the event type is Point (1), then the V plane parameter.
                            If the event type is Overlap (2), then the V plane parameter for curve at (n, 5).

        [9]       Number      If the event type is Point (1), then the U plane parameter.
                            If the event type is Overlap (2), then the U plane parameter for curve at (n, 6).

        [10]      Number      If the event type is Point (1), then the V plane parameter.
                            If the event type is Overlap (2), then the V plane parameter for curve at (n, 6).

      None: on error
    Example:
      import rhinoscriptsyntax as rs

      curve = rs.GetObject("Select curve", rs.filter.curve)
      if curve:
      plane = rs.WorldXYPlane()
      intersections = rs.PlaneCurveIntersection(plane, curve)
      if intersections:
      for intersection in intersections:
      rs.AddPoint(intersection[1])
    See Also:
      IntersectPlanes
      PlanePlaneIntersection
      PlaneSphereIntersection
    """



def PlaneEquation(plane):
    """Returns the equation of a plane as a tuple of four numbers. The standard
    equation of a plane with a non-zero vector is Ax+By+Cz+D=0
    Parameters:
      plane (plane): the plane to deconstruct
    Returns:
      tuple(number, number, number, number): containing four numbers that represent the coefficients of the equation  (A, B, C, D) if successful
      None: if not successful
    Example:
      import rhinoscriptsyntax as rs
      plane = rs.ViewCPlane()
      equation = rs.PlaneEquation(plane)
      print("A =", equation[0])
      print("B =", equation[1])
      print("C =", equation[2])
      print("D =", equation[3])
    See Also:
      PlaneFromFrame
      PlaneFromNormal
      PlaneFromPoints
    """



def PlaneFitFromPoints(points):
    """Returns a plane that was fit through an array of 3D points.
    Parameters:
    points (point): An array of 3D points.
    Returns:
      plane: The plane if successful
      None: if not successful
    Example:
      import rhinoscriptsyntax as rs
      points = rs.GetPoints()
      if points:
      plane = rs.PlaneFitFromPoints(points)
      if plane:
      magX = plane.XAxis.Length
      magY = plane.YAxis.Length
      rs.AddPlaneSurface( plane, magX, magY )
    See Also:
      PlaneFromFrame
      PlaneFromNormal
      PlaneFromPoints
    """



def PlaneFromFrame(origin, x_axis, y_axis):
    """Construct a plane from a point, and two vectors in the plane.
    Parameters:
      origin (point): A 3D point identifying the origin of the plane.
      x_axis (vector): A non-zero 3D vector in the plane that determines the X axis
               direction.
      y_axis (vector): A non-zero 3D vector not parallel to x_axis that is used
               to determine the Y axis direction. Note, y_axis does not
               have to be perpendicular to x_axis.
    Returns:
      plane: The plane if successful.
    Example:
      import rhinoscriptsyntax as rs
      origin = rs.GetPoint("CPlane origin")
      if origin:
      xaxis = (1,0,0)
      yaxis = (0,0,1)
      plane = rs.PlaneFromFrame( origin, xaxis, yaxis )
      rs.ViewCPlane(None, plane)
    See Also:
      MovePlane
      PlaneFromNormal
      PlaneFromPoints
      RotatePlane
    """


def PlaneFromNormal(origin, normal, xaxis=None):
    """Creates a plane from an origin point and a normal direction vector.
    Parameters:
      origin (point): A 3D point identifying the origin of the plane.
      normal (vector): A 3D vector identifying the normal direction of the plane.
      xaxis (vector, optional): optional vector defining the plane's x-axis
    Returns:
      plane: The plane if successful.
    Example:
      import rhinoscriptsyntax as rs
      origin = rs.GetPoint("CPlane origin")
      if origin:
      direction = rs.GetPoint("CPlane direction")
      if direction:
      normal = direction - origin
      normal = rs.VectorUnitize(normal)
      rs.ViewCPlane( None, rs.PlaneFromNormal(origin, normal) )
    See Also:
      MovePlane
      PlaneFromFrame
      PlaneFromPoints
      RotatePlane
    """


def PlaneFromPoints(origin, x, y):
    """Creates a plane from three non-colinear points
    Parameters:
      origin (point): origin point of the plane
      x, y (point): points on the plane's x and y axes
    Returns:
      plane: The plane if successful, otherwise None
    Example:
      import rhinoscriptsyntax as rs
      corners = rs.GetRectangle()
      if corners:
      rs.ViewCPlane( rs.PlaneFromPoints(corners[0], corners[1], corners[3]))
    See Also:
      PlaneFromFrame
      PlaneFromNormal
    """



def PlanePlaneIntersection(plane1, plane2):
    """Calculates the intersection of two planes
    Parameters:
      plane1 (plane): the 1st plane to intersect
      plane2 (plane): the 2nd plane to intersect
    Returns:
      line:  a line with two 3d points identifying the starting/ending points of the intersection
      None: on error
    Example:
      import rhinoscriptsyntax as rs
      plane1 = rs.WorldXYPlane()
      plane2 = rs.WorldYZPlane()
      line = rs.PlanePlaneIntersection(plane1, plane2)
      if line: rs.AddLine(line[0], line[1])
    See Also:
      IntersectPlanes
      LineLineIntersection
      LinePlaneIntersection
    """



def PlaneSphereIntersection(plane, sphere_plane, sphere_radius):
    """Calculates the intersection of a plane and a sphere
    Parameters:
      plane (plane): the plane to intersect
      sphere_plane (plane): equatorial plane of the sphere. origin of the plane is
        the center of the sphere
      sphere_radius (number): radius of the sphere
    Returns:
      list(number, point|plane, number): of intersection results
          Element    Type      Description
          [0]       number     The type of intersection, where 0 = point and 1 = circle.
          [1]   point or plane If a point intersection, the a Point3d identifying the 3-D intersection location.
                               If a circle intersection, then the circle's plane. The origin of the plane will be the center point of the circle
          [2]       number     If a circle intersection, then the radius of the circle.
      None: on error
    Example:
      import rhinoscriptsyntax as rs
      plane = rs.WorldXYPlane()
      radius = 10
      results = rs.PlaneSphereIntersection(plane, plane, radius)
      if results:
      if results[0]==0:
      rs.AddPoint(results[1])
      else:
      rs.AddCircle(results[1], results[2])
    See Also:
      IntersectPlanes
      LinePlaneIntersection
      PlanePlaneIntersection
    """



def PlaneTransform(plane, xform):
    """Transforms a plane
    Parameters:
      plane (plane): Plane to transform
      xform (transform): Transformation to apply
    Returns:
      plane:the resulting plane if successful
      None: if not successful
    Example:
      import rhinoscriptsyntax as rs
      plane = rs.ViewCPlane()
      xform = rs.XformRotation(45.0, plane.Zaxis, plane.Origin)
      plane = rs.PlaneTransform(plane, xform)
      rs.ViewCPlane(None, plane)
    See Also:
      PlaneFromFrame
      PlaneFromNormal
      PlaneFromPoints
    """



def RotatePlane(plane, angle_degrees, axis):
    """Rotates a plane
    Parameters:
      plane (plane): Plane to rotate
      angle_degrees (number): rotation angle in degrees
      axis (vector): Axis of rotation or list of three numbers
    Returns:
      plane: rotated plane on success
    Example:
      import rhinoscriptsyntax as rs
      plane = rs.ViewCPlane()
      rotated = rs.RotatePlane(plane, 45.0, plane.XAxis)
      rs.ViewCPlane( None, rotated )
    See Also:
      MovePlane
      PlaneFromFrame
      PlaneFromNormal
    """



def WorldXYPlane():
    """Returns Rhino's world XY plane
    Returns:
      plane: Rhino's world XY plane
    Example:
      import rhinoscriptsyntax as rs
      view = rs.CurrentView()
      rs.ViewCPlane( view, rs.WorldXYPlane() )
    See Also:
      WorldYZPlane
      WorldZXPlane
    """



def WorldYZPlane():
    """Returns Rhino's world YZ plane
    Returns:
      plane: Rhino's world YZ plane
    Example:
      import rhinoscriptsyntax as rs
      view = rs.CurrentView()
      rs.ViewCPlane( view, rs.WorldYZPlane() )
    See Also:
      WorldXYPlane
      WorldZXPlane
    """



def WorldZXPlane():
    """Returns Rhino's world ZX plane
    Returns:
      plane: Rhino's world ZX plane
    Example:
      import rhinoscriptsyntax as rs
      view = rs.CurrentView()
      rs.ViewCPlane( view, rs.WorldZXPlane() )
    See Also:
      WorldXYPlane
      WorldYZPlane
    """
