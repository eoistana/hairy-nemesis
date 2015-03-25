using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class FieldParser : Parser
  {
    public string Type;
    public DataParser DataType;
    public EnumParser EnumType;
    public SimpleTypeParser SimpleType;
    public string Default;

    public FieldParser(string name, XmlNode node):base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["type"] != null) Type = Node.Attributes["type"].InnerText;
      if (Node.Attributes["default"] != null) Default = Node.Attributes["type"].InnerText;
    }

    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      if (data.ContainsKey(Type)) DataType = data[Type];
      if (enums.ContainsKey(Type)) EnumType = enums[Type];
      if (simpleTypes.ContainsKey(Type)) SimpleType = simpleTypes[Type];
      if (DataType == null && EnumType == null && SimpleType == null) throw new ArgumentException("Referenced type does not exist", Type);
    }
  }
}
