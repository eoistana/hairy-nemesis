using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a reference to a Data element
  /// </summary>
  public class DataRefParser : Parser, IDeclaration
  {
    public string Ref;
    public DataParser DataType;

    public DataRefParser(string @ref, XmlNode node):base(@ref,node)
    {
      Ref = @ref;
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

    private Dictionary<string, bool> implementedEvents = new Dictionary<string, bool>();
    public bool ImplementsEvent(string eventName)
    {
      if (implementedEvents.ContainsKey(eventName)) return implementedEvents[eventName];
      return implementedEvents[eventName] = (DataType != null) ? DataType.ImplementsEvent(eventName) : false;
    }

    internal static DataRefParser New(string name, DataParser dataParser)
    {
      var d = new DataRefParser(name, null);
      d.DataType = dataParser;
      return d;
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
