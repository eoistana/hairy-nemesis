using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public enum MessageHandler
  {
    ByEvent,
    Directly
  }

  public class MessageRefParser : Parser
  {
    public string Ref;
    public MessageHandler Handled = MessageHandler.ByEvent;
    public MessageParser Declaration;

    public MessageRefParser(string @ref, XmlNode node)
      : base(@ref, node)
    {
      Ref = @ref;
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["handled"] != null) Enum.TryParse(Node.Attributes["handled"].InnerText, out Handled);
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (!messages.ContainsKey(Ref)) throw new ArgumentException("The referenced message does not exist", Ref);
      Declaration = messages[Ref];
    }
  }
}
