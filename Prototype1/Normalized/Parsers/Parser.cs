using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Normalized.Parsers
{
  public abstract class Parser : IParser
  {
    public string Name;
    public XmlNode Node;

    public Parser(string name, XmlNode node)
    {
      Name = name;
      Node = node;
    }

    public virtual void Parse(ModuleParser moduleParser, DataParser data)
    {
    }

    public void Parse<T>(ModuleParser moduleParser, DataParser data, Dictionary<string, T> dict, string xPath, string attr = "name")
      where T : IParser
    {
      foreach (XmlNode n in Node.SelectNodes(xPath, moduleParser.NsMan))
      {
        var name = n.Attributes[attr].InnerText;
        var t = (T)Activator.CreateInstance(typeof(T), name, n);
        dict.Add(name, t);
        t.Parse(moduleParser, data);
      }
    }

    public virtual void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      var x = 9;
      //throw new NotImplementedException();
    }
  }
}
