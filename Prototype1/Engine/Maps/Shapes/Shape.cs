using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Engine.Maps.Definitions.ShapeDefinitions;
using Shared.Maps.Shapes;

namespace Engine.Maps.Shapes
{
  public class Shape : IShape
  {
    public ShapeDefinition Definition;

    private Position position;
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

    private bool edgesRecalculated;
    private List<Edge> edges;
    public List<Edge> Edges
    {
      get
      {
        return LazyInitializer.EnsureInitialized(ref edges, ref edgesRecalculated, ref syncRoot,
          () => Definition.GetEdges(position));
      }
    }

    private bool linesRecalculated;
    private List<Line> lines;
    public List<Line> Lines
    {
      get { return LazyInitializer.EnsureInitialized(ref lines, ref linesRecalculated, ref syncRoot, CalculateLines); }
    }

    private bool boundingCircleRecalculated;
    private double boundingCircleRadiusSquared;
    public double BoundingCircleRadiusSquared 
    {
      get
      {
        return LazyInitializer.EnsureInitialized(ref boundingCircleRadiusSquared, ref boundingCircleRecalculated,
          ref syncRoot, () => Edges.Max(edge => edge.Vector.LengthSquared));
      }
    }

    private object syncRoot = new object();


    public Shape(ShapeDefinition definition)
    {
      Definition = definition;
    }



// ReSharper disable once ParameterHidesMember
    internal Shape TranslateShape(Position position)
    {
      var shape = new Shape(Definition)
      {
        Position = position
      };
      return shape;
    }

// ReSharper disable once ParameterHidesMember
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

    #region IShape
    Shared.Maps.IPosition IShape.Position
    {
      get { return Position; }
    }

    #endregion
  }
}
