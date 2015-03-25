using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class ShapeDefinition
  {

    internal List<Edge> GetEdges(Position position)
    {
      return Edges.Select(edge => edge.Translate(position)).ToList();
    }
  }
}
