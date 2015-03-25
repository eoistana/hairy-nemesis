using System;
using Engine.Maps.Definitions;
using Engine.Maths;
using Shared.Maps.Cells.CellContainers.Entities;

namespace Engine.Maps.Cells.CellContainers.Entities
{
  public abstract class MobileEntity<T> : Entity<T>, IMobileEntity
    where T: MobileEntityDefinition, new()
  {
    public double Heading = Math.PI;
    public double Speed;
    public MovementType MovementType;

    protected MobileEntity() : this(0, 0)
    {
    }

    protected MobileEntity(int width, int height) : base(width, height)
    {
    }

    public virtual void Move()
    {
      var distance = Speed/Position.Parent.Scale;

      var dv = Vector.Pol2Cart(distance, Heading);

      var newX = (Position.X + dv.X);
      var newY = (Position.Y + dv.Y);
      var dx = Math.Abs(Math.Floor(Position.X - newX));
      var dy = Math.Abs(Math.Floor(Position.Y - newY));

      if (dx >= 1.0 || dy >= 1.0)
        Position.Parent.MoveMapEntity(this, newX, newY, Heading);
    }

    public virtual void Turn(double rad)
    {
      Heading = (Heading + rad) % (2 * Math.PI);
    }

    public virtual void TurnDeg(double degrees)
    {
      Turn(Vector.Deg2Rad(degrees));
    }
  }
}
