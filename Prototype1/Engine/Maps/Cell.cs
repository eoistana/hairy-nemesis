using System.Collections.Generic;
using System.Linq;
using Engine.Maps.Cells.CellContainers;
using Shared.Maps;
using Shared.Maps.Cells.CellContainers;

namespace Engine.Maps
{
  public class Cell : ICell
  {
    public float Scale = 1;
    public CellType Type;

    protected readonly object entitiesSyncRoot = new object();
    public List<Entity> Entities;


    public void AddEntity(Entity entity)
    {
      lock (entitiesSyncRoot)
      {
        if (Entities == null) Entities = new List<Entity>();
        Entities.Add(entity);
      }
    }

    internal void RemoveEntity(Entity entity)
    {
      lock (entitiesSyncRoot)
      {
        Entities.Remove(entity);
        if (!Entities.Any()) Entities = null;
      }
    }

    private double lastUpdateTime = double.MinValue;
    internal void Tick(long time)
    {
      if (time <= lastUpdateTime) return;
      lastUpdateTime = time;
      Entity[] entitiesCopy;
      lock (entitiesSyncRoot)
      {
        entitiesCopy = (Entities != null ? Entities.ToArray() : null);
      }

      if(entitiesCopy!=null) entitiesCopy.AsParallel().ForAll(e => e.OnTick(time));
      OnTick(time);
    }

    internal virtual void OnTick(long time)
    {
    }

    #region ICell
    double ICell.Scale
    {
      get { return Scale; }
    }

    ICellType ICell.Type
    {
      get { return Type; }
    }

    IEnumerable<IEntity> ICell.Entities
    {
      get { foreach(var e in Entities)yield return e; }
    }
    #endregion
  }
}
