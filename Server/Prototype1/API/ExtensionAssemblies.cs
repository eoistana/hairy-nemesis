using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace API
{
  public static class ExtensionAssemblies
  {
    public static Dictionary<Assembly, bool> Assemblies = new Dictionary<Assembly, bool>();

    public static void LoadExtensions()
    {
      var files = Directory.EnumerateFiles("mods", "*.dll");
      foreach (var f in files)
      {
        AddAssembly(Path.GetFullPath(f));
      }
    }

    public static Assembly AddAssembly(string path)
    {
      var assembly = Assembly.LoadFile(path);
      if (Assemblies.ContainsKey(assembly)) UnregisterTypes(assembly);
      Assemblies[assembly] = false;
      return assembly;
    }

    internal static void UnregisterTypes(Assembly assembly)
    {
    }

    public static void RegisterTypes<T>(IEnumerable<Type> types = null)
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

    private static SortedList<int, TExtendableClass> GetExtensionClasses<TExtendableClass>(TExtendableClass obj)
    {
      if (!(obj is IExtendableClass<TExtendableClass>))
        throw new ArgumentException("Supplied object is not an IExtendableClass", "obj");
      return (obj as IExtendableClass<TExtendableClass>).ExtensionClasses;
    }

    #region Actions

    public static void CallExtendedAction<TExtendableClass>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Action<ExtensionContext>>> overrideMethod)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext();
      var par = new object[] {context};

      ProcessExtendedCall(extensionClasses.Values.Select(arg => (object) arg), overrideMethod, par);
    }

    public static void CallExtendedAction<TExtendableClass, T1>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Action<ExtensionContext, T1>>> overrideMethod,
      T1 t1)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext();
      var par = new object[] { context, t1};

      ProcessExtendedCall(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par);
    }

    public static void CallExtendedAction<TExtendableClass, T1, T2>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Action<ExtensionContext, T1, T2>>> overrideMethod,
      T1 t1,
      T2 t2)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext();
      var par = new object[] { context, t1, t2 };

      ProcessExtendedCall(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par);
    }

    public static void CallExtendedAction<TExtendableClass, T1, T2, T3>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Action<ExtensionContext, T1, T2, T3>>> overrideMethod,
      T1 t1,
      T2 t2,
      T3 t3)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext();
      var par = new object[] { context, t1, t2, t3 };

      ProcessExtendedCall(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par);
    }

    public static void CallExtendedAction<TExtendableClass, T1, T2, T3, T4>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Action<ExtensionContext, T1, T2, T3, T4>>> overrideMethod,
      T1 t1,
      T2 t2,
      T3 t3,
      T4 t4)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext();
      var par = new object[] { context, t1, t2, t3, t4 };

      ProcessExtendedCall(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par);
    }

    public static void CallExtendedAction<TExtendableClass, T1, T2, T3, T4, T5>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Action<ExtensionContext, T1, T2, T3, T4, T5>>> overrideMethod,
      T1 t1,
      T2 t2,
      T3 t3,
      T4 t4,
      T5 t5)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext();
      var par = new object[] {context, t1, t2, t3, t4, t5};

      ProcessExtendedCall(extensionClasses.Values.Select(arg => (object) arg), overrideMethod, par);
    }

    #endregion

    #region Functions

    public static TReturn CallExtended<TExtendableClass, TReturn>(
      TExtendableClass obj, 
      Expression<Func<TExtendableClass, Func<ExtensionContext<TReturn>, TReturn>>> overrideMethod)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] {context};

      return ProcessExtendedCall<TReturn>(extensionClasses.Values.Select(arg => (object) arg), overrideMethod, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1>(
      TExtendableClass obj, 
      Expression<Func<TExtendableClass, Func<ExtensionContext<TReturn>, T1, TReturn>>> overrideMethod, 
      T1 t1)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] {context, t1};

      return ProcessExtendedCall<TReturn>(extensionClasses.Values.Select(arg => (object) arg), overrideMethod, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1, T2>(
      TExtendableClass obj, 
      Expression<Func<TExtendableClass, Func<ExtensionContext<TReturn>, T1, T2, TReturn>>> overrideMethod, 
      T1 t1, 
      T2 t2)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] {context, t1, t2};

      return ProcessExtendedCall<TReturn>(extensionClasses.Values.Select(arg => (object) arg), overrideMethod, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1, T2, T3>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Func<ExtensionContext<TReturn>, T1, T2, T3, TReturn>>> overrideMethod,
      T1 t1,
      T2 t2,
      T3 t3)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] { context, t1, t2, t3 };

      return ProcessExtendedCall<TReturn>(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1, T2, T3, T4>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Func<ExtensionContext<TReturn>, T1, T2, T3, T4, TReturn>>> overrideMethod,
      T1 t1,
      T2 t2,
      T3 t3,
      T4 t4)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] { context, t1, t2, t3, t4 };

      return ProcessExtendedCall<TReturn>(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par, context);
    }

    public static TReturn CallExtended<TExtendableClass, TReturn, T1, T2, T3, T4, T5>(
      TExtendableClass obj,
      Expression<Func<TExtendableClass, Func<ExtensionContext<TReturn>, T1, T2, T3, T4, T5, TReturn>>> overrideMethod,
      T1 t1,
      T2 t2,
      T3 t3,
      T4 t4,
      T5 t5)
    {
      var extensionClasses = GetExtensionClasses(obj);
      var context = new ExtensionContext<TReturn> { LastValue = default(TReturn) };
      var par = new object[] { context, t1, t2, t3, t4, t5 };

      return ProcessExtendedCall<TReturn>(extensionClasses.Values.Select(arg => (object)arg), overrideMethod, par, context);
    }

    #endregion

    private static TReturn ProcessExtendedCall<TReturn>(IEnumerable<object> extensionClasses, Expression overrideMethod, object[] par,
      ExtensionContext context)
    {
      var mi =
        ((((((LambdaExpression) overrideMethod).Body as UnaryExpression).Operand as MethodCallExpression).Object as
          ConstantExpression).Value) as MethodInfo;
      foreach (
        var result in
          extensionClasses.Select(
            foo =>
              (TReturn)
                mi.Invoke(foo, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, par, null))
        )
      {
        context.SetValue(result);
      }
      return context.GetValue<TReturn>();
    }

    private static void ProcessExtendedCall(IEnumerable<object> extensionClasses, Expression overrideMethod, object[] par)
    {
      var mi = ((((((LambdaExpression)overrideMethod).Body as UnaryExpression).Operand as MethodCallExpression).Object as ConstantExpression).Value) as MethodInfo;
      foreach (
        var result in
          extensionClasses)
        mi.Invoke(result, BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, par, null);
    }

    public static void AddExtensions<TExtendableClass>(TExtendableClass obj)
      where TExtendableClass : class
    {
      var extensionClasses = GetExtensionClasses(obj);
      extensionClasses.Add(0, obj);
      foreach (var f in ClassExtender.GetInstanceCreators<TExtendableClass>())
      {
        var ff = f.Value(obj);
        if (ff != null) extensionClasses.Add(f.Key, ff);
      }
    }
  }
}