
using System.Collections.Generic;
using System.Linq;
using Engine.Maps.Definitions;

namespace Engine.Maps
{
  public class MovementType
  {
    public Dictionary<int, MovementTypeDefinition> Definitions;
    
    public MovementType(params int[] id)
    {
      Definitions = new Dictionary<int, MovementTypeDefinition>();
      foreach (var i in id)
      {
        Definitions[i] =  MovementTypeDefinition.Definitions[i];
      }
    }

    public MovementType(MovementTypeDefinition def, params int[] id) :this(id)
    {
      Definitions[def.Id] = def;
    }

    public MovementType(MovementType definitions, MovementTypeDefinition def)
    {
      Definitions = new Dictionary<int, MovementTypeDefinition>();
      foreach (var kv in definitions.Definitions) Definitions.Add(kv.Key, kv.Value);
      Definitions[def.Id] = def;
    }

    public static MovementType operator |(MovementType a, MovementType b)
    {
      return new MovementType(a.Definitions.Keys.Concat(b.Definitions.Keys).Distinct().ToArray());
    }

    public static MovementType operator |(MovementType a, MovementTypeDefinition b)
    {
      return new MovementType(a, b);
    }

    public static MovementType operator |(MovementTypeDefinition a, MovementType b)
    {
      return new MovementType(b, a);
    }

    public static MovementType operator &(MovementType a, MovementType b)
    {
      return new MovementType(a.Definitions.Keys.Intersect(b.Definitions.Keys).ToArray());
    }

    public static MovementType operator &(MovementType a, MovementTypeDefinition b)
    {
      return new MovementType(a.Definitions.Keys.Intersect(new[]{b.Id}).ToArray());
    }

    public static MovementType operator &(MovementTypeDefinition a, MovementType b)
    {
      return new MovementType(b.Definitions.Keys.Intersect(new[] { a.Id }).ToArray());
    }
  }
}
