using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public class Class3 : DynamicObject
  {
    public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    {
      return base.TryInvokeMember(binder, args, out result);
    }

    public override bool TryConvert(ConvertBinder binder, out object result)
    {
      return base.TryConvert(binder, out result);
    }

    public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
    {
      return base.TryBinaryOperation(binder, arg, out result);
    }

    public static void Test(ITest it)
    {
      it.Add();
    }

    public void Add()
    {
      throw new NotImplementedException();
    }
  }

  public interface ITest
  {
    void Add();
  }
}
