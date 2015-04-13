using Engine.Maps.Cells;
using Engine.Maths;
using Shared.Maps;

namespace Engine.Maps
{
  public class Position : IPosition
  {
    public double X;
    public double Y;
    public double Rotation;

    public CellContainer Parent;

    public Position(double x, double y, double rotation, CellContainer parent = null)
    {
      X = x;
      Y = y;
      Rotation = rotation;
      Parent = parent;
    }

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

    #region IPosition
    double IPosition.X
    {
      get { return X; }
    }

    double IPosition.Y
    {
      get { return Y; }
    }

    double IPosition.Rotation
    {
      get { return Rotation; }
    }

    Shared.Maps.Cells.ICellContainer IPosition.Parent
    {
      get { return Parent; }
    }
    #endregion
  }
}
