using Normalized.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class CellContainer
  {
    partial void OnCellContainerInit()
    {
      if (Width > 0 && Height > 0)
        Cells = new Cell[Width, Height];
    }

    partial void OnProcessUpdatePositionMessage(UpdatePositionMessage message)
    {
      MoveMapEntity(message.Entity, message.X, message.Y, message.Entity.Position.Rotation);
    }

    partial void OnTick(TickEventParameters e)
    {
      e.Continue = true;
    }


    public void AddMapEntity(Entity entity, double x, double y, double rotation)
    {
      if (Cells == null) return;
      if (x < 0 || x >= Width) return;
      if (y < 0 || y >= Height) return;
      var cell = Cells[(int)x, (int)y];
      if (cell is CellContainer)
      {
        var childPos = ConvertPositionToChildPosition((cell as CellContainer),new Position(x, y, rotation));
        (cell as CellContainer).AddMapEntity(entity, childPos.X, childPos.Y, childPos.Rotation);
      }
      else
      {
        cell.AddEntities(entity);
        entity.SetPosition(x, y);
        AddEntities(entity);
        entity.SetParent(this);
      }
    }

    public void RemoveMapEntity(Entity entity)
    {
      if (Cells == null) return;
      if (entity.Position.Parent != this) return;
      Cells[(int)entity.Position.X, (int)entity.Position.Y].RemoveEntities(entity);
      RemoveEntities(entity);
    }

    internal void MoveMapEntity(Entity entity, double x, double y, double rotation)
    {
      if (DetectTransition(x, y))
      {
        if(Position != null && Position.Parent != null)
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
      AddMapEntity(entity, x, y, rotation);
      entity.SetRotation(rotation);
      entity.PositionChanged(new PositionChangedEventParameters());
    }

    private bool DetectTransition(double x, double y)
    {
      return x < 0 || x >= Width || y < 0 || y >= Height;
    }

    protected virtual Position ConvertPositionToParentPosition(CellContainer source, Position pos)
    {
      var xxx = (((pos.X * source.Scale) / Scale) / source.Width) * Scale;
      var yyy = (((pos.Y * source.Scale) / Scale) / source.Height) * Scale;
      var xxxyyy = new Vector(xxx,yyy).Rotate(source.Position.Rotation).Translate(source.Position.X, source.Position.Y);

      return new Position(xxxyyy.X, xxxyyy.Y, pos.Rotation - Position.Rotation);
    }

    protected virtual Position ConvertPositionToChildPosition(CellContainer destination, Position pos)
    {
      var s = destination.Scale;
      var xx = (((pos.X - destination.Position.X) * Scale / s) / Scale) * destination.Width;
      var yy = (((pos.Y - destination.Position.Y) * Scale / s) / Scale) * destination.Height;
      var xxxyyy = new Vector(xx, yy).Rotate(-destination.Position.Rotation);

      return new Position(xxxyyy.X, xxxyyy.Y, pos.Rotation + Position.Rotation);
    }

    private void HandleTransition(Entity entity, CellContainer source, double x, double y, double rotation)
    {
      var parentPost = ConvertPositionToParentPosition(source, new Position(x, y, rotation));

      if (DetectTransition(parentPost.X, parentPost.Y))
      {
        if(Position != null && Position.Parent != null)
          Position.Parent.HandleTransition(entity, this, parentPost.X, parentPost.Y, parentPost.Rotation);
        return;
      }

      MoveMapEntityInternal(entity.Position.Parent, entity, parentPost.X, parentPost.Y, parentPost.Rotation);
    }

    protected virtual bool DetectCollision(Entity entity, double x, double y, double rotation, out Entity otherEntity)
    {
      otherEntity = null;
      var position = new Position(x, y, rotation);
      var entityShape = entity.Shape.TranslateShape(position);

      otherEntity = Entities.Where(entity1 => entity1 != entity).FirstOrDefault(entity2 => entityShape.Intersects(position, entity2.Shape, entity2.Position));
      return otherEntity != null;
    }

    protected virtual void HandleCollision(Entity entity, Entity otherEntity, double x, double y)
    {
      entity.Collide(new CollideEventParameters(otherEntity, x, y));
      otherEntity.Collide(new CollideEventParameters(entity, x, y));
    }

    public override string ToString()
    {
      return string.Format("Map: {0}, X: {1}, Y: {2}", Position.Parent is Map ? (Position.Parent as Map).Name : "", Position.X, Position.Y);
    }
  }
}
