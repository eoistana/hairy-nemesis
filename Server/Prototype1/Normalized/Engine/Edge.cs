using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class Edge
  {

    internal Edge Translate(Position position)
    {
      return new Edge(Vector.Rotate(position.Rotation).Translate(position.X, position.Y), Height);
    }

    internal Line ToLine(Edge edge)
    {
      return new Line(this, edge);
    }
  }
}
