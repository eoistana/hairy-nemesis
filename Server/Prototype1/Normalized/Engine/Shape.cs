using Modules.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class Shape
  {
    public Position Position
    {
      get { return position; }
      set
      {
        lock (syncRoot)
        {
          edgesRecalculated = false;
          linesRecalculated = false;
          boundingCircleRecalculated = false;
          position = value;
        }
      }
    }

    private object syncRoot = new object();
    public List<Edge> Edges
    {
      get
      {
        return LazyInitializer.EnsureInitialized(ref edges, ref edgesRecalculated, ref syncRoot,
          () => Definition.GetEdges(position));
      }
    }

    public List<Line> Lines
    {
      get { return LazyInitializer.EnsureInitialized(ref lines, ref linesRecalculated, ref syncRoot, CalculateLines); }
    }

    public double BoundingCircleRadiusSquared
    {
      get
      {
        return LazyInitializer.EnsureInitialized(ref boundingCircleRadiusSquared, ref boundingCircleRecalculated,
          ref syncRoot, () => Edges.Max(edge => edge.Vector.LengthSquared));
      }
    }


    internal Shape TranslateShape(Position position)
    {
      var shape = new Shape(Definition)
      {
        Position = position
      };
      return shape;
    }

    internal bool Intersects(Position position, Shape otherShape, Position otherPosition)
    {
      var radii = BoundingCircleRadiusSquared + otherShape.BoundingCircleRadiusSquared;
      var distanceSquared = (position - otherPosition).LengthSquared;
      if (radii <= distanceSquared) return false;
      return Lines.Any(otherShape.Intersects);
    }

    private bool Intersects(Line line)
    {
      return Lines.Any(line.Intersects);
    }

    private List<Line> CalculateLines()
    {
      var newlines = new List<Line>();
      var edgeCopy = Edges;
      var edgeCount = edgeCopy.Count;
      for (var i = 0; i < edgeCount; i++)
        newlines.Add(edgeCopy[i].ToLine(edgeCopy[i % edgeCount]));
      return newlines;
    }
  }
}
