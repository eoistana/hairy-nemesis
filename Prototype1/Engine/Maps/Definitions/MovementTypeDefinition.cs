
using System.Collections.Generic;

namespace Engine.Maps.Definitions
{
  public class MovementTypeDefinition
  {
    public static Dictionary<int, MovementTypeDefinition> Definitions = LoadAll();

    public int Id;
    public string Name;

    public MovementTypeDefinition(int id, string name)
    {
      Id = id;
      Name = name;
    }


    internal static Dictionary<int, MovementTypeDefinition> LoadAll()
    {
      return new Dictionary<int, MovementTypeDefinition>
      {
        {0x0000, new MovementTypeDefinition(0x0000, "None")},
        {0x0001, new MovementTypeDefinition(0x0001, "Walk")},
        {0x0002, new MovementTypeDefinition(0x0002, "Climb")},
        {0x0004, new MovementTypeDefinition(0x0004, "Run")},
        {0x0008, new MovementTypeDefinition(0x0008, "Crawl")},
        {0x0010, new MovementTypeDefinition(0x0010, "Jump")},
        {0x0020, new MovementTypeDefinition(0x0020, "Fly")},
        {0x0040, new MovementTypeDefinition(0x0040, "Ride")},
        {0x0080, new MovementTypeDefinition(0x0080, "Drive")},
        {0x0100, new MovementTypeDefinition(0x0100, "Teleport")},
      };
    }


    public static implicit operator MovementType(MovementTypeDefinition definition)
    {
      return new MovementType (definition.Id);
    }

    public static MovementType operator |(MovementTypeDefinition a, MovementTypeDefinition b)
    {
      return new MovementType (a.Id | b.Id );
    }
    public static MovementType operator &(MovementTypeDefinition a, MovementTypeDefinition b)
    {
      return new MovementType (a.Id & b.Id );
    }
  }
}
