using System;

namespace Engine.Maps.Definitions.ShapeDefinitions
{
  [Flags]
  public enum EdgeType
  {
    None = 0x0000,
    BlockingPassage = 0x0001,
    BlockingView = 0x0002,
    BlockingLight = 0x0004,

  }
}
