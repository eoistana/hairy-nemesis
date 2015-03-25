using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Using element
  /// </summary>
  public class UsingParser : Parser
  {
    public XmlDocument Doc;

    public UsingParser(string name, XmlNode node):base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Doc = ModuleParser.Parse(Path.GetDirectoryName(moduleParser.Filename) + "\\" + Name + ".xml", ref moduleParser);
    }
  }
}
