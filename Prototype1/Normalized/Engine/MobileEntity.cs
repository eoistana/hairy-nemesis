using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class MobileEntity
  {

    partial void OnTick(TickEventParameters e)
    {
    }

    partial void OnProcessTickMoveMessage(MoveMessage TickMoveMessage)
    {
      Position.Parent.RegisterUpdatePositionMessage(new UpdatePositionMessage(this, TickMoveMessage.X, TickMoveMessage.Y));
    }
  }
}
