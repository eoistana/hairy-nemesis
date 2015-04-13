
using Shared.Maps.Cells;

namespace Shared.Maps
{
  public interface IPosition
  {
    double X { get; }
    double Y { get; }
    double Rotation { get; }

    ICellContainer Parent { get; }
  }
}
