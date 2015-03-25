using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public enum ExtendsFieldType
  {
    Field,
    List
  }
  public class ExtendsParser : Parser
  {
    public string Ref;
    public string ParentVar;
    public ExtendsFieldType ParentType;
    public bool IsPartOfKey;

    public ExtendsParser(string @ref, XmlNode node):base(@ref,node)
    {
      Ref = @ref;
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      ParentVar = (Node.Attributes["parentvar"] != null) ? Node.Attributes["parentvar"].InnerText : "Parent";
      ParentType = ExtendsFieldType.Field;
      if (Node.Attributes["parenttype"] != null) Enum.TryParse(Node.Attributes["parenttype"].InnerText, out ParentType);
      IsPartOfKey = (Node.Attributes["partofkey"] != null) ? Node.Attributes["partofkey"].InnerText == "true" : false;
    }


    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (!data.ContainsKey(Ref)) throw new ArgumentException("Referred data class does not exist", Ref);
    }
  }
}
