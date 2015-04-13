using System.Collections.Generic;
using System.Linq;

namespace Engine.Maps.Definitions.ShapeDefinitions
{
  public class ShapeDefinition
  {
    public double Height;
    public List<Edge> Edges;

    internal List<Edge> GetEdges(Position position)
    {
      return Edges.Select(edge => edge.Translate(position)).ToList();
    }
  }
}
