using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using API;


namespace ConsoleApplication2
{
  class Program
  {
    static void Main(string[] args)
    {

      var f = new Foo(0);

      var i = f.Calc();


      var assembly = ExtensionAssemblies.AddAssembly(@"C:\Users\staale.kvernes\Documents\GitHub\hairy-nemesis\ApIGeneratorPrototype\ConsoleApplication2\Extension1\bin\Debug\Extension1.dll");
      
      ExtensionAssemblies.RegisterTypes<XFoo>();

      var i2 = f.Calc();
      f = new Foo(0);

      var i3 = f.Calc();


      var i4 = f.Do2(4, 6);

      f = new Foo(1);
      var i5 = f.Calc();

      var ifoo = ClassExtender.GetExtensionClass<IFoo>(f, "Extension1", "Bar");



    }
  }
}
