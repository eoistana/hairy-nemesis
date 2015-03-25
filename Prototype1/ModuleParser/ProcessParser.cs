using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public class ProcessParser : Parser
  {
    public string Message;
    public MessageParser Declaration;

    public ProcessParser(string message, XmlNode node)
      : base(message, node)
    {
      Message = message;
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (!messages.ContainsKey(Message)) throw new ArgumentException("Referenced message does not exist", Message);
      Declaration = messages[Message];
    }
  }
}
