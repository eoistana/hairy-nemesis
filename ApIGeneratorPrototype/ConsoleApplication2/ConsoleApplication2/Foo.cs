using System.Collections.Generic;
using API;

namespace ConsoleApplication2
{
  public class Foo : IFoo
  {
    public int Type { get; set; }

    private readonly SortedList<int, IFoo> foos = new SortedList<int, IFoo>();

    static Foo()
    {
      ExtensionAssemblies.RegisterTypes<XFoo>();
    }

    public Foo(int type)
    {
      Type = type;
      ExtensionAssemblies.AddExtensions(this, foos);
    }

    public IFoo GetExtension(string name)
    {
      return foos[ClassExtender.GetClassSortOrder<IFoo>(name)];
    }

    public int Calc()
    {
      return ExtensionAssemblies.CallExtended<IFoo, int>(this, foos, x => x.Calc);
    }

    public void Do()
    {
      ExtensionAssemblies.CallExtended(this, foos, x => x.Do);
    }

    public int Do2(int x, int y)
    {
      return ExtensionAssemblies.CallExtended<IFoo, int, int, int>(this, foos, d => d.Do2, x, y);
    }

    #region IFoo

    int IFoo.Calc(ExtendContext<int> context)
    {
      if (context.Override)
        return context.LastValue;
      return 5;
    }

    void IFoo.Do(ExtendContext context)
    {
      if (context.Override)
        return;
    }

    int IFoo.Do2(ExtendContext<int> context, int x, int y)
    {
      if (context.Override)
        return context.LastValue;
      return 1;
    }

    #endregion
  }
}