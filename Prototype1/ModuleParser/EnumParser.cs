using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModulesParser
{
  /// <summary>
  /// Parses an Enum element
  /// </summary>
  public class EnumParser : Parser, IDeclaration
  {
    public bool Flags;
    public Dictionary<string, EnumValueParser> EnumValues = new Dictionary<string, EnumValueParser>();

    public EnumParser(string name, System.Xml.XmlNode node): base(name, node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Flags = (Node.Attributes["flags"] != null) ? Node.Attributes["flags"].InnerText == "true" : false;

      Parse(moduleParser, data, EnumValues, "./en:Value");
    }


    public string GetDeclaration()
    {
      return Name;
    }


    public bool ImplementsEvent(string eventName)
    {
      return false;
    }


    public string GetTypeName()
    {
      return Name;
    }


    public string GetName()
    {
      return Name;
    }
  }
}
