using System.Linq;
using System.Threading.Tasks;
using Engine.Maps.Cells.CellContainers;
using Engine.Maps.Shapes;
using Shared.Maps.Cells;
using Shared.Maps;
using Shared.Maps.Shapes;

namespace Engine.Maps.Cells
{
  public class CellContainer : Cell, ICellContainer
  {
    public Position Position = new Position(0,0,0);

    public int Width;
    public int Height;
    public Cell[,] Cells;
    public Shape Shape;

    public CellContainer(int width, int height)
    {
      Width = width;
      Height = height;
      if (width != 0 && height != 0) Cells = new Cell[width, height];
      else Cells = null;
    }

    public void AddMapEntity(Entity entity, double x, double y)
    {
      if (Cells == null) return;
      if (x < 0 || x >= Width) return;
      if (y < 0 || y >= Height) return;
      Cells[(int)x, (int)y].AddEntity(entity);
      entity.SetPosition(x, y);
      AddEntity(entity);
      entity.SetParent(this);
    }

    public void RemoveMapEntity(Entity entity)
    {
      if (Cells == null) return;
      if (entity.Position.Parent != this) return;
      Cells[(int)entity.Position.X, (int)entity.Position.Y].RemoveEntity(entity);
      RemoveEntity(entity);
    }

    internal void MoveMapEntity(Entity entity, double x, double y, double rotation)
    {
      if (DetectTransition(x, y))
      {
        Position.Parent.HandleTransition(entity, this, x, y, rotation);
        return;
      }

      Entity otherEntity;
      if (DetectCollision(entity, x, y, rotation, out otherEntity))
      {
        HandleCollision(entity, otherEntity, x, y);
      }
      else
      {
        MoveMapEntityInternal(this, entity, x, y, rotation);
      }
    }

    private void MoveMapEntityInternal(CellContainer parent, Entity entity, double x, double y, double rotation)
    {
      parent.RemoveMapEntity(entity);
      AddMapEntity(entity, x, y);
      entity.SetRotation(rotation);
      entity.OnPositionChanged();
    }

    private bool DetectTransition(double x, double y)
    {
      return x < 0 || x >= Width || y < 0 || y >= Height;
    }

    private void HandleTransition(Entity entity, CellContainer source, double x, double y, double rotation)
    {
      var xx = x < 0 ? x : x - source.Width + 1;
      var newX = source.Position.X + xx*source.Scale;
      var yy = y < 0 ? y : y - source.Height + 1;
      var newY = source.Position.Y + yy*source.Scale;

      if (DetectTransition(newX, newY))
      {
        Position.Parent.HandleTransition(entity, this, newX, newY, rotation);
        return;
      }

      MoveMapEntityInternal(entity.Position.Parent, entity, newX, newY, rotation);
    }

    protected virtual bool DetectCollision(Entity entity, double x, double y, double rotation, out Entity otherEntity)
    {
      var position = new Position(x, y, rotation);
      var entityShape = entity.Shape.TranslateShape(position);

      otherEntity = Entities.Where(entity1 => entity1!=entity).FirstOrDefault(entity2 => entityShape.Intersects(position, entity2.Shape, entity2.Position));
      return otherEntity != null;
    }

    protected virtual void HandleCollision(Entity entity, Entity otherEntity, double x, double y)
    {
      entity.Collide(otherEntity, x, y);
      otherEntity.Collide(entity, x, y);
    }


    internal override void OnTick(long time)
    {
      if (Cells == null) return;

      Parallel.For(0, Cells.GetLength(0),
        x => Parallel.For(0, Cells.GetLength(1), 
          y => { if (Cells[x, y] != null) Cells[x, y].Tick(time); }));

      //for(var x = 0;x<Width;x++)
      //  for(var y=0;y<Height;y++)
      //    if (Cells[x, y] != null) Cells[x, y].Tick(time);
    }


    public override string ToString()
    {
      return string.Format("Map: {0}, X: {1}, Y: {2}", Position.Parent is Map ? (Position.Parent as Map).Name: "", Position.X, Position.Y);
    }

    #region ICellContainer
    IPosition ICellContainer.Position
    {
      get { return Position; }
    }

    int ICellContainer.Width
    {
      get { return Width; }
    }

    int ICellContainer.Height
    {
      get { return Height; }
    }

    ICell[,] ICellContainer.Cells
    {
      get { return (ICell[,])Cells; }
    }

    IShape ICellContainer.Shape
    {
      get { return Shape; }
    }
    #endregion
  }
}
