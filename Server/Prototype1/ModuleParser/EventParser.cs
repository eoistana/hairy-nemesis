﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses and Event element
  /// </summary>
  public class EventParser : Parser
  {
    public string Description;
    public Dictionary<string, ParameterParser> Parameters = new Dictionary<string, ParameterParser>();

    public EventParser(string name, XmlNode node):base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Description = Node.Attributes["description"] != null ? Node.Attributes["description"].InnerText : null;

      Parse(moduleParser, data, Parameters, "./en:Parameter");
    }

    private bool validated;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;

      foreach (var p in Parameters.Values) p.ValidateReferences(simpleTypes, data, enums, events, messages);
    }

    public string GetSummary(string tabs)
    {
      return DataParser.GetSummary(tabs, Description);
    }
  }
}
