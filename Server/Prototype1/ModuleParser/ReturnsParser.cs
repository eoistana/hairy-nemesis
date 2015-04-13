using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public enum ReturnMethod 
  {
    Asynchronized,
    Synchronized
  }


  /// <summary>
  /// Parses a reference to a Data element
  /// </summary>
  public class ReturnsParser : Parser
  {
    public string Ref;
    public MessageParser MessageType;
    public ReturnMethod ReturnMethod;

    public ReturnsParser(string @ref, XmlNode node) : base(@ref, node)
    {
      Ref = @ref;
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      Enum.TryParse(Node.Attributes["returnmethod"].InnerText, out ReturnMethod);
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (messages.ContainsKey(Ref)) MessageType = messages[Ref];

      if (MessageType == null) throw new ArgumentException("Referenced message element does not exist", Ref);
    }

  }
}


