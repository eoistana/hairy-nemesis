using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Message element
  /// </summary>
  public class MessageParser : Parser
  {
    public bool ExposedToClient;
    public string Description;
    public Dictionary<string, ParameterParser> Parameters = new Dictionary<string, ParameterParser>();
    public Dictionary<string, ReturnsParser> ReturnValue = new Dictionary<string, ReturnsParser>();

    public MessageParser(string name, XmlNode node)
      : base(name, node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      ExposedToClient = (Node.Attributes["expose"] != null) ? Node.Attributes["expose"].InnerText == "true" : false;
      Description = Node.Attributes["description"] != null ? Node.Attributes["description"].InnerText : null;
      Parse(moduleParser, data, Parameters, "./en:Parameter");
      Parse(moduleParser, data, ReturnValue, "./en:Returns", "ref");
    }

    private bool validated;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;

      foreach (var p in Parameters.Values) p.ValidateReferences(simpleTypes, data, enums, events, messages);
      foreach (var r in ReturnValue.Values) r.ValidateReferences(simpleTypes, data, enums, events, messages);
    }

    internal string GetSummary(string tabs)
    {
      return DataParser.GetSummary(tabs, Description);
    }

  }
}
