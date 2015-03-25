using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Class1
    {
    }


    public partial class A1
    {
      public int Y;

      public int add(int x, int y)
      {
        return x + y;
      }

      public int GetX()
      {
        return X;
      }
    }

    public partial class A1
    {
      private int x = 5;
      public int X
      {
        get
        {
          return x;
        }
      }

      public int Add()
      {
        return add(X, Y);
      }
    }
}
