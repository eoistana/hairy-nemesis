using System.Collections.Generic;
using Shared.Maps.Cells.CellContainers;

namespace Shared.Maps
{
  public interface ICell
  {
    double Scale { get; }
    ICellType Type { get; }
    IEnumerable<IEntity> Entities { get; }
  }
}
