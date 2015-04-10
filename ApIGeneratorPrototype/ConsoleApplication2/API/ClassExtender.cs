using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

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

    public static void Extend<TApiInterface, TExtenderClass>(Func<TApiInterface, TExtenderClass> createNew, int sortOrder)
      where TExtenderClass : TApiInterface
    {
      var extensionAttribute = typeof(TExtenderClass).GetCustomAttribute<ExtensionAttribute>();
      if (!ClassNames.ContainsKey(typeof (TApiInterface)))
        ClassNames.Add(typeof (TApiInterface), new Dictionary<string, Dictionary<string, Tuple<Type, int>>>());
      if (!ClassNames[typeof(TApiInterface)].ContainsKey(extensionAttribute.Mod))
        ClassNames[typeof(TApiInterface)].Add(extensionAttribute.Mod, new Dictionary<string, Tuple<Type, int>>());
      ClassNames[typeof(TApiInterface)][extensionAttribute.Mod].Add(extensionAttribute.Name, new Tuple<Type, int>(typeof(TExtenderClass), sortOrder));

      if (!InstanceCreators.ContainsKey(typeof(TApiInterface)))
        InstanceCreators.Add(typeof(TApiInterface), new SortedList<int, CreatorContainerBase>());

      InstanceCreators[typeof (TApiInterface)].Add(sortOrder,
        new CreatorContainer<TApiInterface, TExtenderClass> {Creator = createNew});
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

    public static int GetClassSortOrder<T>(string modName, string className)
    {
      if (!ClassNames.ContainsKey(typeof (T)))
        throw new ArgumentOutOfRangeException("T", "Type " + typeof (T).Name + " is not an extendable class.");
      if (!ClassNames[typeof (T)].ContainsKey(modName))
        throw new ArgumentOutOfRangeException("name", "Mod " + modName + " does not extend " + typeof (T).Name);
      if (!ClassNames[typeof(T)][modName].ContainsKey(className))
        throw new ArgumentOutOfRangeException("name", "Class " + className + " from mod " + modName + " does not extend " + typeof(T).Name);
      return ClassNames[typeof (T)][modName][className].Item2;
    }

    public static TExtendableClass GetExtensionClass<TExtendableClass>(TExtendableClass foo, string modName, string className)
    {
      var classSortOrder = GetClassSortOrder<TExtendableClass>(modName, className);
      var extendableClass = foo as IExtendableClass<TExtendableClass>;
      if (extendableClass != null)
        return extendableClass.ExtensionClasses[classSortOrder];
      throw new NotSupportedException(String.Format("{0} is not an API class.", typeof (TExtendableClass).Name));
    }
  }
}
