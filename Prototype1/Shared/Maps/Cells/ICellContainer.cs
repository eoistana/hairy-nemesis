
using Shared.Maps.Cells.CellContainers;
using Shared.Maps.Shapes;

namespace Shared.Maps.Cells
{
  public interface ICellContainer
  {
    IPosition Position { get; }

    int Width { get; }
    int Height { get; }
    ICell[,] Cells { get; }
    IShape Shape { get; }
  }
}
