using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Engine
{
  public partial class Map
  {
    partial void OnMapInit()
    {
      // Load map
      Parallel.For(0, Width, i => Parallel.For(0, Height, i1 =>  Cells[i, i1] = new Cell(100+i1*10+i)));
      Position = new Position(0, 0, 0);
    }
  }
}
