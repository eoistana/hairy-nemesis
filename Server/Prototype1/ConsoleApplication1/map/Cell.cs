using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.map
{
  public class Cell
  {
    public float Scale;
    public CellType Type;

    public List<Entity> Entities;

    public void AddEntity(Entity entity, double x, double y)
    {
      Entities.Add(entity);
      entity.Position.X = x;
      entity.Position.Y = y;
    }

    internal void RemoveEntity(Entity entity)
    {
      Entities.Remove(entity);
    }
  }
}
