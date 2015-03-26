using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Field element
  /// </summary>
  public class FieldParser : Parser, IDeclaration
  {
    public string Type;
    public DataParser DataType;
    public EnumParser EnumType;
    public SimpleTypeParser SimpleType;

    public IDeclaration Declaration;
    public string Default;
    public string Description;

    public FieldParser(string name, XmlNode node):base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["type"] != null) Type = Node.Attributes["type"].InnerText;
      if (Node.Attributes["default"] != null) Default = Node.Attributes["default"].InnerText;
      Description = Node.Attributes["description"] != null ? Node.Attributes["description"].InnerText : null;
    }

    private bool validated = false;
    private bool ParentPointer;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (data.ContainsKey(Type)) Declaration = DataType = data[Type];
      if (enums.ContainsKey(Type)) Declaration = EnumType = enums[Type];
      if (simpleTypes.ContainsKey(Type)) Declaration = SimpleType = simpleTypes[Type];
      if (DataType == null && EnumType == null && SimpleType == null) throw new ArgumentException("Referenced type does not exist", Type);
    }

    public string GetTypeName()
    {
      return Declaration.GetTypeName();
    }

    public string GetDeclaration()
    {
      return string.Format("{0} {1}", Declaration.GetDeclaration(), Name);
    }

    public string GetDefaultValue()
    {
      return (Default != null) ? " = " + Default : "";
    }

    internal string GetPublicDeclaration()
    {
      return "public " + GetDeclaration() + ";";
    }

    private Dictionary<string, bool> implementedEvents = new Dictionary<string, bool>();
    public bool ImplementsEvent(string eventName)
    {
      if (implementedEvents.ContainsKey(eventName)) return implementedEvents[eventName];
      if (ParentPointer) return implementedEvents[eventName] = false;
      if (DataType == null) return implementedEvents[eventName] = false;
      return implementedEvents[eventName] = DataType.ImplementsEvent(eventName);
    }

    internal static FieldParser New(string name, DataParser dataParser)
    {
      var f = new FieldParser(name, null);
      f.Type = name;
      f.Declaration = f.DataType = dataParser;
      f.ParentPointer = true;
      return f;
    }


    public string GetName()
    {
      return Name;
    }

    internal string GetSummary(string tabs)
    {
      return DataParser.GetSummary(tabs, Description);
    }

  }
}
