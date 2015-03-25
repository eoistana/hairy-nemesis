using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a reference to an Event element
  /// </summary>
  public class EventRefParser : Parser
  {
    public string Ref;
    public EventParser Declaration;

    public Dictionary<string, ProcessParser> Processes = new Dictionary<string, ProcessParser>();

    public EventRefParser(string @ref, XmlNode node)
      : base(@ref, node)
    {
      Ref = @ref;
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Parse(moduleParser, data, Processes, "./en:Process", "message");
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (!events.ContainsKey(Ref)) throw new ArgumentException("The referenced event does not exist", Ref);
      Declaration = events[Ref];
    }
  }
}
