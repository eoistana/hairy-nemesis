using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
  public class Program
  {
    static void Main(string[] args)
    {
      var server = new Server.ServiceClient();
      var response = server.GetData("Hello world");

      Console.WriteLine(response);
    }
  }
}
