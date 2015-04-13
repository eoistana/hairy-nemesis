using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Maths;

namespace Engine.Maps.Definitions.ShapeDefinitions
{
  public class Line
  {
    public Edge Point1, Point2;

    public Line(Edge edge1, Edge edge2)
    {
      Point1 = edge1;
      Point2 = edge2;
    }

    internal bool Intersects(Line line)
    {
      bool linesIntersect;
      bool segmentsIntersect;
      FindIntersection(Point1.Vector, Point2.Vector, line.Point1.Vector, line.Point2.Vector, out linesIntersect, out segmentsIntersect);
      return segmentsIntersect;
    }



    // Find the point of intersection between
    // the lines p1 --> p2 and p3 --> p4.
    private void FindIntersection(
        Vector v1, Vector v2, Vector v3, Vector v4,
        out bool linesIntersect, out bool segmentsIntersect)
    {
      // Get the segments' parameters.
      var dx12 = v2.X - v1.X;
      var dy12 = v2.Y - v1.Y;
      var dx34 = v4.X - v3.X;
      var dy34 = v4.Y - v3.Y;

      // Solve for t1 and t2
      var denominator = (dy12 * dx34 - dx12 * dy34);

      var t1 =
          ((v1.X - v3.X) * dy34 + (v3.Y - v1.Y) * dx34)
              / denominator;
      if (double.IsInfinity(t1))
      {
        // The lines are parallel (or close enough to it).
        linesIntersect = false;
        segmentsIntersect = false;
        return;
      }
      linesIntersect = true;

      var t2 = ((v3.X - v1.X) * dy12 + (v1.Y - v3.Y) * dx12)
              / -denominator;

      // Find the point of intersection.
      //intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

      // The segments intersect if t1 and t2 are between 0 and 1.
      segmentsIntersect =
          ((t1 >= 0) && (t1 <= 1) &&
           (t2 >= 0) && (t2 <= 1));

    }
  }
}
