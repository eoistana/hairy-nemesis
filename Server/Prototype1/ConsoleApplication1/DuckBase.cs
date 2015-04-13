using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ConsoleApplication1
{
  public abstract class DuckBase
  {
    private static readonly ModuleBuilder Mb = SetupEmit();
    private static AssemblyBuilder ab;

    protected internal static ModuleBuilder SetupEmit()
    {
      var aName = new AssemblyName("DuckType");
      ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
      return ab.DefineDynamicModule(aName.Name);
    }

    protected internal static TypeBuilder GetTypeBuilder<T, TInterface>()
    {
      return Mb.DefineType(
        "Duck_" + typeof (TInterface).Name + "_" + typeof (T).Name,
        TypeAttributes.Public, 
        typeof (object),
        new[] {typeof (TInterface)});
    }

    protected internal static Type MakeDuck<T, TInterface>()
    {
      var tb = GetTypeBuilder<T, TInterface>();

      var underlyingObject = DefineUnderlyingObjectField<T>(tb);
      DefineConstructor<T>(tb, underlyingObject);

      DefineInterfaceProperties<T, TInterface>(tb, underlyingObject);
      DefineInterfaceMethods<T, TInterface>(tb, underlyingObject);

      var t = tb.CreateType();
      //ab.Save("Test2.dll");
      return t;
    }

    private static void DefineInterfaceMethods<T, TInterface>(TypeBuilder tb, FieldInfo underlyingObject)
    {
      var iType = typeof (TInterface);
      var tType = typeof (T);

      var tTypeMeths = tType.GetMethods();

      foreach (var ip in iType.GetMethods())
      {
        var tp = tTypeMeths.GetMethod(ip);
        if (tp == null)
          throw new ArgumentException(
            string.Format("Type [{0}] does not implement method [{1}] with parameters matching interface [{2}]",
              tType.Name, ip.Name, iType.Name));

        // Define a method that passes the call along to the underlyingObject
        var mbM = tb.DefineMethod(ip.Name,
          MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot |
          MethodAttributes.Final);// public hidebysig newslot virtual final
        //var gp = mbM.DefineGenericParameters("");
        //ip.GetGenericArguments()
        
        

        mbM.SetSignature(tp.ReturnType, null, null, ip.GetParameters().Select(p => p.ParameterType).ToArray(), null, null);
        
        var il = mbM.GetILGenerator();

        if(tp.ReturnType != typeof(void)) il.DeclareLocal(tp.ReturnType);
        il.Emit(OpCodes.Nop);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldfld, underlyingObject);
        var argnr = 1;
        for (var count = ip.GetParameters().Count(); count > 0; count--)
        {
          il.EmitLdArg(argnr++);
        }
        il.EmitCall(OpCodes.Callvirt, tp, null);
        if (tp.ReturnType != typeof (void))
        {
          il.Emit(OpCodes.Stloc_0);
          var lab = il.DefineLabel();
          il.Emit(OpCodes.Br_S, lab);
          il.MarkLabel(lab);
          il.Emit(OpCodes.Ldloc_0);
        }
        il.Emit(OpCodes.Ret);
      }
    }

    private static void DefineInterfaceProperties<T, TInterface>(TypeBuilder tb, FieldInfo underlyingObject)
    {
      var iType = typeof (TInterface);
      var tType = typeof (T);

      foreach (var ip in iType.GetProperties())
      {
        var tp = tType.GetProperty(ip.Name, ip.PropertyType);
        if (tp == null)
          throw new ArgumentException(
            string.Format("Type [{0}] does not implement property [{1}] with type matching interface [{2}]",
              tType.Name, ip.Name, iType.Name));

        var mprop = tb.DefineProperty(ip.Name, ip.Attributes, ip.PropertyType, null);
        if (tp.CanRead)
        {
          var rmeth = tb.DefineMethod(ip.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final);
          rmeth.SetParameters(null);
          rmeth.SetReturnType(ip.PropertyType);
          var il = rmeth.GetILGenerator();
          il.DeclareLocal(tp.PropertyType);
          il.Emit(OpCodes.Nop);
          il.Emit(OpCodes.Ldarg_0);
          il.Emit(OpCodes.Ldfld, underlyingObject);
          il.EmitCall(OpCodes.Callvirt, tp.GetGetMethod(), null);
          il.Emit(OpCodes.Stloc_0);
          il.Emit(OpCodes.Ldloc_0);
          il.Emit(OpCodes.Ret);

          mprop.SetGetMethod(rmeth);
        }
        if (tp.CanWrite)
        {
          var rmeth = tb.DefineMethod(ip.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final);
          rmeth.SetParameters(new []{ip.PropertyType});
          rmeth.SetReturnType(typeof(void));
          var il = rmeth.GetILGenerator();
          il.DeclareLocal(tp.PropertyType);
          il.Emit(OpCodes.Nop);
          il.Emit(OpCodes.Ldarg_0);
          il.Emit(OpCodes.Ldfld, underlyingObject);
          il.Emit(OpCodes.Ldarg_1);
          il.EmitCall(OpCodes.Callvirt, tp.GetSetMethod(), null);
          il.Emit(OpCodes.Ret);

          mprop.SetSetMethod(rmeth);
        }
      }
    }

    private static void DefineConstructor<T>(TypeBuilder tb, FieldInfo underlyingObject)
    {
      var ctor1 = tb.DefineConstructor(
        MethodAttributes.Public,
        CallingConventions.Standard,
        new[] {typeof (T)});

      var ctor1IL = ctor1.GetILGenerator();
      // For a constructor, argument zero is a reference to the new 
      // instance. Push it on the stack before calling the base 
      // class constructor. Specify the default constructor of the  
      // base class (System.Object) by passing an empty array of  
      // types (Type.EmptyTypes) to GetConstructor.
      ctor1IL.Emit(OpCodes.Ldarg_0);
      ctor1IL.Emit(OpCodes.Call,
// ReSharper disable once AssignNullToNotNullAttribute
        typeof(object).GetConstructor(Type.EmptyTypes));
      // Push the instance on the stack before pushing the argument 
      // that is to be assigned to the private field m_number.
      ctor1IL.Emit(OpCodes.Ldarg_0);
      ctor1IL.Emit(OpCodes.Ldarg_1);
      ctor1IL.Emit(OpCodes.Stfld, underlyingObject);
      ctor1IL.Emit(OpCodes.Ret);
    }

    private static FieldBuilder DefineUnderlyingObjectField<T>(TypeBuilder tb)
    {
      return tb.DefineField(
        "_underlyingObject",
        typeof (T),
        FieldAttributes.Private);
    }
  }

  public static class ILGeneratorExtensions
  {
    public static void EmitLdArg(this ILGenerator il, int argnr)
    {
      switch (argnr)
      {
        case 0:
          il.Emit(OpCodes.Ldarg_0);
          return;
        case 1:
          il.Emit(OpCodes.Ldarg_1);
          return;
        case 2:
          il.Emit(OpCodes.Ldarg_2);
          return;
        case 3:
          il.Emit(OpCodes.Ldarg_3);
          return;
        default:
          if (argnr < 256)
            il.Emit(OpCodes.Ldarg_S, (short)argnr);
          else
            il.Emit(OpCodes.Ldarg, argnr);
          return;
      }
    }

    public static MethodInfo GetMethod(this IEnumerable<MethodInfo> tTypeMeths, MethodInfo ip)
    {
      return tTypeMeths.FirstOrDefault(m => m.Name == ip.Name
                                            && m.ContainsGenericParameters == ip.ContainsGenericParameters
                                            && m.IsGenericMethod == ip.IsGenericMethod
                                            && m.IsGenericMethodDefinition == ip.IsGenericMethodDefinition
                                            && m.ReturnType == ip.ReturnType
                                            && m.GetParameters().All(p => ip.GetParameters().Any(f => f.Name == p.Name)));
    }
  }
}