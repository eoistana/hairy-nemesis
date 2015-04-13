
using Shared.Maps;
namespace Engine.Maps
{
  public class CellType : ICellType
  {
    public int Id;

    #region ICellType
    int ICellType.Id
    {
      get { return Id; }
    }
    #endregion
  }
}
