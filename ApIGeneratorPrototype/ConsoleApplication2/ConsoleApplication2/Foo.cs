﻿using System;
using System.Collections.Generic;
using API;

namespace ConsoleApplication2
{
  public partial class Foo : IFoo, IExtendableClass<IFoo>
  {
    public int Type { get; set; }

    private readonly SortedList<int, IFoo> extensionClasses = new SortedList<int, IFoo>();
    SortedList<int, IFoo> IExtendableClass<IFoo>.ExtensionClasses { get { return extensionClasses; } }

    static Foo()
    {
      ExtensionAssemblies.RegisterTypes<XFoo>();
    }

    public Foo(int type)
    {
      Type = type;
      ExtensionAssemblies.AddExtensions<IFoo>(this);
    }

    public int Calc()
    {
      return ExtensionAssemblies.CallExtended<IFoo, int>(this, x => x.Calc);
    }

    public void Do()
    {
      ExtensionAssemblies.CallExtendedAction<IFoo>(this, x => x.Do);
    }

    public int Do2(int x, int y)
    {
      return ExtensionAssemblies.CallExtended<IFoo, int, int, int>(this, d => d.Do2, x, y);
    }

    #region IFoo

    int IFoo.Type
    {
      get { return Type; }
      set { Type = value; }
    }

    partial void IFooCalc(ExtensionContext<int> context);
    int IFoo.Calc(ExtensionContext<int> context)
    {
      IFooCalc(context);
      return context.LastValue;
    }

    partial void IFooDo(ExtensionContext context);
    void IFoo.Do(ExtensionContext context)
    {
      IFooDo(context);
    }

    partial void IFooDo2(ExtensionContext<int> context, int x, int y);
    int IFoo.Do2(ExtensionContext<int> context, int x, int y)
    {
      IFooDo2(context, x, y);
      return context.LastValue;
    }

    #endregion

  }


  public partial class Foo
  {
    partial void IFooCalc(ExtensionContext<int> context)
    {
      if (context.SupressOriginalCall) return;

      context.LastValue = 5;
    }

    partial void IFooDo(ExtensionContext context)
    {
      if (context.SupressOriginalCall) return;
    }

    partial void IFooDo2(ExtensionContext<int> context, int x, int y)
    {
      if (context.SupressOriginalCall) return;
      context.LastValue = 1;
    }
  }
}