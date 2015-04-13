using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApplication1
{
  public abstract class Duck<TInterface> : DuckBase
  {
    private Duck()
    {
    }

    [SuppressMessage("Microsoft.Design", "CA1000")]
// ReSharper disable once StaticFieldInGenericType
// ReSharper disable once InconsistentNaming
    private static readonly Dictionary<Type, Type> _cacheDucks;

    static Duck()
    {
      _cacheDucks = new Dictionary<Type, Type>();
    }


    public static TInterface Factory<T>(T t)
    {
      Type tType;
      lock (_cacheDucks)
      {
        if (!_cacheDucks.ContainsKey(typeof(T)))
        {
          _cacheDucks[typeof(T)] = MakeDuck<T, TInterface>();
        }
        tType = _cacheDucks[typeof (T)];
      }

// ReSharper disable once PossibleNullReferenceException
      return (TInterface) tType.GetConstructor(new[] {typeof (T)}).Invoke(new object[] {t});
    }

  }
}