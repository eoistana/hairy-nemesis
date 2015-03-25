using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class Entity
  {

    internal void SetRotation(double rotation)
    {
      Position.Rotation = rotation;
    }

    internal void SetPosition(double x, double y)
    {
      Position.X = x;
      Position.Y = y;
    }

    internal void SetParent(CellContainer cellContainer)
    {
      Position.Parent = cellContainer;
    }
  }
}
