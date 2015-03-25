
namespace Engine.Maps.Definitions.EntityDefinitions
{
  public class PlayerDefinition : MobileEntityDefinition
  {
    public PlayerDefinition()
    {
      MovementType = new MovementType(1,2,4,8,16);
    }
  }
}
