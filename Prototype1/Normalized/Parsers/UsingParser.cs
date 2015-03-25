using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class UsingParser : Parser
  {
    public XmlDocument Doc;

    public UsingParser(string name, XmlNode node):base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Doc = ModuleParser.Parse(Name + ".xml", ref moduleParser);
    }
  }
}
