using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.map
{
  public class Entity : CellContainer
  {
    public EntityDefinition Definition;

    public string Name;

    public void ResetName()
    {
      Name = Definition.Name;
    }

    internal void Collide(Entity otherEntity, double x, double y)
    {
      throw new NotImplementedException();
    }
  }
}
