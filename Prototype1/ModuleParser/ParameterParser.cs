using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Parameter element
  /// </summary>
  public class ParameterParser : Parser, IDeclaration
  {
    public string Type;
    public DataParser DataType;
    public EnumParser EnumType;
    public SimpleTypeParser SimpleType;

    public IDeclaration Declaration;

    public ParameterParser(string name, XmlNode node)
      : base(name, node)
    { }


    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Type = Node.Attributes["type"].InnerText;
    }


    private bool validated;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;

      if (data.ContainsKey(Type)) Declaration = DataType = data[Type];
      if (enums.ContainsKey(Type)) Declaration = EnumType = enums[Type];
      if (simpleTypes.ContainsKey(Type)) Declaration = SimpleType = simpleTypes[Type];
      if (DataType == null && EnumType == null && SimpleType == null) throw new ArgumentException("Referenced type does not exist", Type);
    }

    public string GetDeclaration()
    {
      return string.Format("{0} {1}", Declaration.GetDeclaration(), Name);
    }
    public bool ImplementsEvent(string eventName)
    {
      throw new NotImplementedException();
    }


    public string GetTypeName()
    {
      return Declaration.GetTypeName();
    }


    public string GetName()
    {
      return Name.ToLower();
    }

    public Dictionary<string, string> GetServiceType(string name)
    {
      if (DataType == null) return new Dictionary<string, string> { { name, Type } };

      return DataType.GetKeyNamesAndTypes(name);
    }
  }
}
