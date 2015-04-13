using System;
using Engine.Maps.Definitions;
using Shared.Maps.Cells.CellContainers;

namespace Engine.Maps.Cells.CellContainers
{
  public abstract class Entity : CellContainer, IEntity
  {
    protected Entity() : this(0, 0)
    {
    }

    protected Entity(int width, int height) : base(width, height)
    {
    }


    public string Name;


    internal virtual void Collide(Entity otherEntity, double x, double y)
    {
      throw new NotImplementedException();
    }

    internal void SetParent(CellContainer cellContainer)
    {
      Position.Parent = cellContainer;
    }

    internal void SetRotation(double rotation)
    {
      Position.Rotation = rotation;
    }

    internal void SetPosition(double x, double y)
    {
      Position.X = x;
      Position.Y = y;
    }

    internal void OnPositionChanged()
    {
      Shape.Position = Position;
    }


    public void ResetName()
    {
      ResetNameInternal();
    }

    protected abstract void ResetNameInternal();

    public EntityDefinition GetDefinition()
    {
      return GetDefinitionInternal();
    }

    protected abstract EntityDefinition GetDefinitionInternal();
  }

  public abstract class Entity<T> : Entity
    where T : EntityDefinition, new()
  {
    public static readonly T Definition = new T();

    protected Entity()
      : this(0, 0)
    {
    }

    protected Entity(int width, int height)
      : base(width, height)
    {
    }

    protected override void ResetNameInternal()
    {
      Name = Definition.Name;
    }

    protected override EntityDefinition GetDefinitionInternal()
    {
      return Definition;
    }
  }
}
