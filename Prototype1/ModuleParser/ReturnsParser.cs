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
  public class ReturnsParser : Parser, IDeclaration
  {
    public string Ref;
    public DataParser DataType;
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
      if (data.ContainsKey(Ref)) DataType = data[Ref];

      if (DataType == null) throw new ArgumentException("Referenced data element does not exist", Ref);
    }

    public string GetDeclaration()
    {
      throw new NotImplementedException();
    }

    public bool ImplementsEvent(string eventName)
    {
      return false;
    }

    public string GetTypeName()
    {
      return DataType.GetTypeName();
    }


    public string GetName()
    {
      return Ref;
    }
  }
}


