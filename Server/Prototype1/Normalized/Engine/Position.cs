using Normalized.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class Position
  {
    public static Vector operator +(Position a, Position b)
    {
      return (Vector)a + (Vector)b;
    }

    public static Vector operator -(Position a, Position b)
    {
      return (Vector)a - (Vector)b;
    }

    public static explicit operator Vector(Position p)
    {
      return new Vector(p.X, p.Y);
    }

  }
}
