using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using API;

namespace ConsoleApplication2
{
  public static class ExtensionAssemblies
  {
    public static Dictionary<Assembly, bool> Assemblies = new Dictionary<Assembly, bool>();

    internal static void RegisterTypes<T>(IEnumerable<Type> types = null)
    {
      RegisterTypes(typeof (T), types);
    }

    internal static void RegisterTypes(Type tType, IEnumerable<Type> types = null)
    {
      if (types == null)
        types = Assemblies.Where(pair => pair.Value == false).SelectMany(module => module.Key.GetTypes()).ToArray();
      foreach (var t in types.Where(type => type.IsSubclassOf(tType)))
      {
        var attr = t.GetCustomAttribute<ExtensionAttribute>();
        if (attr != null)
          t.InvokeMember(attr.Register, BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null,
            new object[0]);
        Assemblies[t.Assembly] = true;
      }
    }

    internal static Assembly AddAssembly(string path)
    {
      var assembly = Assembly.LoadFile(path);
      Assemblies.Add(assembly, false);
      return assembly;
    }

    public static void CallExtended<TExtendableClass>(TExtendableClass obj, SortedList<int, TExtendableClass> foos, Expression<Func<TExtendableClass, Action<ExtendContext>>> f)
    {
      var context = new ExtendContext();
      var par = new object[] {context};

      LastValue(foos.Values.Select(arg => (object)arg), f, par);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn>(TExtendableClass obj, SortedList<int, TExtendableClass> foos, Expression<Func<TExtendableClass, Func<ExtendContext<TReturn>, TReturn>>> f)
    {
      var context = new ExtendContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] { context };

      return LastValue<TReturn>(foos.Values.Select(arg => (object)arg), f, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1>(TExtendableClass obj, SortedList<int, TExtendableClass> foos, Expression<Func<TExtendableClass, Func<ExtendContext<TReturn>, T1, TReturn>>> f, T1 t1)
    {
      var context = new ExtendContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] { context, t1 };

      return LastValue<TReturn>(foos.Values.Select(arg => (object)arg), f, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1, T2>(TExtendableClass obj, SortedList<int, TExtendableClass> foos, Expression<Func<TExtendableClass, Func<ExtendContext<TReturn>, T1, T2, TReturn>>> f, T1 t1, T2 t2)
    {
      var context = new ExtendContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] { context, t1, t2 };

      return LastValue<TReturn>(foos.Values.Select(arg => (object)arg), f, par, context);
    }

    private static TReturn LastValue<TReturn>(IEnumerable<object> foos, Expression f, object[] par,
      ExtendContext context)
    {
      var mi =
        ((((((LambdaExpression) f).Body as UnaryExpression).Operand as MethodCallExpression).Object as
          ConstantExpression).Value) as MethodInfo;
      foreach (
        var result in
          foos.Select(
            foo =>
              (TReturn)
                mi.Invoke(foo, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, par, null))
        )
      {
        context.SetValue(result);
      }
      return context.GetValue<TReturn>();
    }

    private static void LastValue(IEnumerable<object> foos, Expression f, object[] par)
    {
      var mi = ((((((LambdaExpression)f).Body as UnaryExpression).Operand as MethodCallExpression).Object as ConstantExpression).Value) as MethodInfo;
      foreach (
        var result in
          foos)
        mi.Invoke(result, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, par, null);
    }

    public static void AddExtensions<TExtendableClass>(TExtendableClass obj, SortedList<int, TExtendableClass> foos)
    {
      foos.Add(0, obj);
      foreach (var f in ClassExtender.GetInstanceCreators<TExtendableClass>())
      {
        var ff = f.Value(obj);
        if (ff != null) foos.Add(f.Key, ff);
      }
    }
  }
}