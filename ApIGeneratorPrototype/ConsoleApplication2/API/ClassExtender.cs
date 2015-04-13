using System;
using System.Collections.Generic;
using System.Reflection;

namespace API
{
  internal abstract class CreatorContainerBase
  {
    public abstract Func<TApiInterface, TExtenderClass> GetCreator<TApiInterface, TExtenderClass>();
  }

  internal class CreatorContainer<TApiInterface, TExtenderClass> : CreatorContainerBase
  {
    public Func<TApiInterface, TExtenderClass> Creator; 
    public override Func<T1, T2> GetCreator<T1, T2>()
    {
      return (Func<T1, T2>) (object)Creator;
    }
  }

  public static class ClassExtender
  {
    private static readonly Dictionary<Type, Dictionary<string, Dictionary<string, Tuple<Type, int>>>> ClassNames =
      new Dictionary<Type, Dictionary<string, Dictionary<string, Tuple<Type, int>>>>();

    private static readonly Dictionary<Type, SortedList<int, CreatorContainerBase>> InstanceCreators =
      new Dictionary<Type, SortedList<int, CreatorContainerBase>>();

    /// <summary>
    /// Call this from the register method, to extend the API class
    /// </summary>
    /// <typeparam name="TApiInterface">The API type to extend</typeparam>
    /// <typeparam name="TExtenderClass">The type of the extender class</typeparam>
    /// <param name="createNew">A delegate to create a new instance of the extender class. Return null if the extender should not be added.</param>
    /// <param name="executionOrder">The execution order of the extension.</param>
    public static void Extend<TApiInterface, TExtenderClass>(Func<TApiInterface, TExtenderClass> createNew, int executionOrder)
      where TExtenderClass : TApiInterface
    {
      var extensionAttribute = typeof(TExtenderClass).GetCustomAttribute<ExtensionAttribute>();
      if (!ClassNames.ContainsKey(typeof (TApiInterface)))
        ClassNames.Add(typeof (TApiInterface), new Dictionary<string, Dictionary<string, Tuple<Type, int>>>());
      if (!ClassNames[typeof(TApiInterface)].ContainsKey(extensionAttribute.Mod))
        ClassNames[typeof(TApiInterface)].Add(extensionAttribute.Mod, new Dictionary<string, Tuple<Type, int>>());
      ClassNames[typeof(TApiInterface)][extensionAttribute.Mod].Add(extensionAttribute.Name, new Tuple<Type, int>(typeof(TExtenderClass), executionOrder));

      if (!InstanceCreators.ContainsKey(typeof(TApiInterface)))
        InstanceCreators.Add(typeof(TApiInterface), new SortedList<int, CreatorContainerBase>());

      InstanceCreators[typeof (TApiInterface)].Add(executionOrder,
        new CreatorContainer<TApiInterface, TExtenderClass> {Creator = createNew});
    }

    /// <summary>
    /// Not implemented
    /// </summary>
    /// <param name="TApiInterface"></param>
    /// <param name="TExtenderClass"></param>
    public static void RemoveExtension(Type TApiInterface, Type TExtenderClass)
    {
      throw new NotImplementedException();
      //if (ClassNames.ContainsKey(TApiInterface))
      //{
      //  var extensionAttribute = TExtenderClass.GetCustomAttribute<ExtensionAttribute>();
      //  ClassNames[TApiInterface].Remove(extensionAttribute.Mod);
      //}
    }

    public static IEnumerable<KeyValuePair<int, Func<TApiInterface, TApiInterface>>> GetInstanceCreators<TApiInterface>()
    {
      if(!InstanceCreators.ContainsKey(typeof(TApiInterface))) yield break;
      foreach (var instanceCreator in InstanceCreators[typeof(TApiInterface)])
      {
        yield return
          new KeyValuePair<int, Func<TApiInterface, TApiInterface>>(instanceCreator.Key,
            instanceCreator.Value.GetCreator<TApiInterface, TApiInterface>());
      }
    }

    public static int GetClassExecutionOrder<T>(string modName, string className)
    {
      if (!ClassNames.ContainsKey(typeof (T)))
        throw new ArgumentOutOfRangeException("T", "Type " + typeof (T).Name + " is not an extendable class.");
      if (!ClassNames[typeof (T)].ContainsKey(modName))
        throw new ArgumentOutOfRangeException("modName", "Mod " + modName + " does not extend " + typeof (T).Name);
      if (!ClassNames[typeof(T)][modName].ContainsKey(className))
        throw new ArgumentOutOfRangeException("className", "Class " + className + " from mod " + modName + " does not extend " + typeof(T).Name);
      return ClassNames[typeof (T)][modName][className].Item2;
    }

    public static TExtendableClass GetExtensionClass<TExtendableClass>(TExtendableClass foo, string modName, string className)
    {
      var classExecutionOrder = GetClassExecutionOrder<TExtendableClass>(modName, className);
      var extendableClass = foo as IExtendableClass<TExtendableClass>;
      if (extendableClass != null)
        return extendableClass.ExtensionClasses[classExecutionOrder];
      throw new NotSupportedException(string.Format("{0} is not an API class.", typeof (TExtendableClass).Name));
    }
  }
}
