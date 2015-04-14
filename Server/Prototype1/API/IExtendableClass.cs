using System.Collections.Generic;

namespace API
{
  public interface IExtendableClass<TExtendableClass>
  {
    SortedList<int, TExtendableClass> ExtensionClasses { get; }
  }
}