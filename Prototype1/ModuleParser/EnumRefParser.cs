using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public class EnumRefParser : Parser, IDeclaration
  {
    public string Type;
    public EnumParser EnumType;
    public EnumRefParser(string name, XmlNode node) : base(name,node)
    {
    }


    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      Type = Node.Attributes["type"].InnerText;
      if (!enums.ContainsKey(Type)) throw new ArgumentException("Unknown enum type", Type);

      EnumType = enums[Type];
    }

    public string GetDeclaration()
    {
      return EnumType.Name + " " + Name;
    }


    public bool ImplementsEvent(string eventName)
    {
      return false;
    }

    internal string GetPublicDeclaration()
    {
      return "public " + GetDeclaration() + ";";
    }


    public string GetTypeName()
    {
      return EnumType.GetTypeName();
    }


    public string GetName()
    {
      return Name;
    }
  }
}
