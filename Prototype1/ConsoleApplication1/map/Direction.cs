using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.map
{
  public enum Direction
  {
    None,
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
    Up,
    Down,
  }

  public static class DirectionExtensions
  {
    public static Direction Turn(this Direction startDirection, float degrees)
    {
      degrees = degrees%360;
      if (degrees < 0) degrees += 360;
      var direction = (int)degrees/45;
      switch (direction)
      {
        case 0:
          return Direction.North;
        case 1:
          return Direction.NorthEast;
        case 2:
          return Direction.East;
        case 3:
          return Direction.SouthEast;
        case 4:
          return Direction.South;
        case 5:
          return Direction.SouthWest;
        case 6:
          return Direction.West;
        case 7:
          return Direction.NorthWest;
      }

      return Direction.None;
    }
  }
}
