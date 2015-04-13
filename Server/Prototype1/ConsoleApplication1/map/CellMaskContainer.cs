using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1.map
{
  public class CellMaskContainer
  {
    public int Height;
    public int Width;
    public int CenterX;
    public int CenterY;
    public Dictionary<Direction, CellMask[,]> CellMasks;

    protected static readonly CellMask Occupied = new CellMask {Occupied = true};
    protected static readonly CellMask[,] SingleOccupied = new CellMask[,] {{Occupied}};

    private static readonly CellMaskContainer singleCell = new CellMaskContainer
    {
      Height = 1,
      Width = 1,
      CellMasks = new Dictionary<Direction, CellMask[,]>
      {
        {Direction.North, SingleOccupied},
        {Direction.NorthEast, SingleOccupied},
        {Direction.East, SingleOccupied},
        {Direction.SouthEast, SingleOccupied},
        {Direction.South, SingleOccupied},
        {Direction.SouthWest, SingleOccupied},
        {Direction.West, SingleOccupied},
        {Direction.NorthWest, SingleOccupied},
        {Direction.Up, SingleOccupied},
        {Direction.Down, SingleOccupied}
      }
    };

    public static CellMaskContainer SingleCell
    {
      get { return singleCell; }
    }
  }
}
