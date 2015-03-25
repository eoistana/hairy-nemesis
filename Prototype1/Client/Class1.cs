using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Class1
    {
      public static void x()
      {
      }
    }

    public class CompositeType
    {
      bool boolValue = true;
      string stringValue = "Hello ";

      public bool BoolValue
      {
        get { return boolValue; }
        set { boolValue = value; }
      }

      public string StringValue
      {
        get { return stringValue; }
        set { stringValue = value; }
      }
    }
}
