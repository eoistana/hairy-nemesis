using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses an enum value
  /// </summary>
  public class EnumValueParser : Parser
  {
    public string Value;

    public EnumValueParser(string name, XmlNode node)
      : base(name, node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Value = (Node.Attributes["value"] != null) ? Node.Attributes["value"].InnerText : null;
    }
  }
}
