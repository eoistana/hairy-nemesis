using Engine.Maps.Definitions.CellDefinitions;

namespace Engine.Maps.Cells
{
  public class Ground : Cell
  {
  }

  public class Ground<T> : Ground
    where T : GroundDefinition, new()
  {
    public static readonly T Defintion = new T();


  }
}
