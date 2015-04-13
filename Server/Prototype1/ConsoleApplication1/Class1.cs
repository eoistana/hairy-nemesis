using System;
using System.Linq.Expressions;
using System.Runtime.Remoting.Proxies;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public class Special
  {

    public static Special Create(int i)
    {
      return new Special();
    }
  }

  public class GenericNew
  {
    public T Create<[GenericNew(typeof (int))] T>()
    {
      return default (T);
    }
  }

  [AttributeUsage(AttributeTargets.GenericParameter)]
  public class GenericNewAttribute : Attribute
  {
    public Type[] Params { get; set; }

    public GenericNewAttribute(params Type[] parms)
    {
      Params = parms;

    }
  }

}
