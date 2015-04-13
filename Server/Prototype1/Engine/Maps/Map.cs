
using System.Threading.Tasks;
using Engine.Maps.Cells;
using Shared.Maps.Cells;

namespace Engine.Maps
{
  public class Map : CellContainer
  {
    public string Name;

    public Map(string name, int width, int height) : base(width, height)
    {
      Name = name;
      Scale = 1;

      // Load map
      Parallel.For(0, 10, i => Parallel.For(0, 10, i1 =>  ((this as ICellContainer).Cells[i, i1]) = new Ground()));
    }

    internal override void OnTick(long time)
    {
      base.OnTick(time);

    }
  }
}
