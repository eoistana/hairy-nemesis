using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.map
{
  public class CellContainer : Cell
  {
    public Position Position;

    public int Width;
    public int Height;
    public Cell[,] Cells;
    public CellMaskContainer Mask;

    internal void Move(Entity entity, double x, double y)
    {
      Entity otherEntity;
      if (DetectCollision(entity, x, y, out otherEntity))
      {
        HandleCollision(entity, otherEntity, x, y);
      }
      else
      {
        Cells[(int) entity.Position.X, (int) entity.Position.Y].RemoveEntity(entity);
        Cells[(int) x, (int) y].AddEntity(entity, x, y);
      }
    }

    protected static Box GetMaskBox(Entity entity, double? x = null, double? y = null)
    {
      var xi = x??entity.Position.X;
      var yi = y??entity.Position.Y;
      var mask = entity.Mask;
      var x1 = xi - mask.CenterX;
      var y1 = yi - mask.CenterY;
      var x2 = xi + (mask.Width - mask.CenterX);
      var y2 = yi + (mask.Height - mask.CenterY);
      return new Box(x1, y1, x2, y2);
    }

    protected virtual bool DetectCollision(Entity entity, double x, double y, out Entity otherEntity)
    {
      var entityBox = GetMaskBox(entity, x, y);

      otherEntity = Entities.FirstOrDefault(entity2 => entityBox.Intersects(GetMaskBox(entity2)));
      return otherEntity != null;
    }

    protected virtual void HandleCollision(Entity entity, Entity otherEntity, double x, double y)
    {
      entity.Collide(otherEntity, x, y);
      otherEntity.Collide(entity, x, y);
    }

  }
}
