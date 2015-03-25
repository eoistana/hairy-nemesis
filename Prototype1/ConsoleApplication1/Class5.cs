using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public class Class5<T>
  {
    
  }

  public class Class5Base
  {
  }

  public class Class5Sub1 : Class5Base
  {
    public void t<T>(Class5<T> p) where T: Class5Base
    {
      t(new Class5<Class5Sub1>());
    }
  }
}
