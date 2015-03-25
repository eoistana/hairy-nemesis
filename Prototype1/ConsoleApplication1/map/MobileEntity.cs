using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.map
{
  public class MobileEntity : Entity
  {
    public float Direction;
    public float Speed;

    public virtual void Move()
    {
      var distance = Speed/Position.Parent.Scale;

      var dv = Vector.Pol2Cart(distance, Direction);

      var newX = (Position.X + dv.X);
      var newY = (Position.Y + dv.Y);
      var dx = Math.Abs(Math.Floor(Position.X - newX));
      var dy = Math.Abs(Math.Floor(Position.Y - newY));

      if (dx >= 1.0 || dy >= 1.0)
        Position.Parent.Move(this, newX, newY);
    }

    public virtual void Turn(float degrees)
    {

    }
  }
}
