using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class ArrayParser : Parser
  {
    public IParser ListElement;

    public ArrayParser(string name, XmlNode node): base(name, node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      var element = Node.SelectSingleNode("./en:Data|./en:DataRef|./en:Field|./en:EnumRef|./en:List|./en:Array", moduleParser.NsMan);
      var name = element.Attributes["name"] != null ? element.Attributes["name"].InnerText : null;
      var @ref = element.Attributes["ref"] != null ? element.Attributes["ref"].InnerText : null;
      if (element != null)
      {
        switch (element.Name)
        {
          case "Data":
            ListElement = new DataRefParser(name, element);
            break;
          case "DataRef":
            ListElement = new DataRefParser(@ref, element);
            break;
          case "Field":
            ListElement = new FieldParser(name, element);
            break;
          case "EnumRef":
            ListElement = new EnumRefParser(@ref, element);
            break;
          case "List":
            ListElement = new ListParser(name, element);
            break;
          case "Array":
            ListElement = new ArrayParser(name??"", element);
            break;
          default:
            throw new NotImplementedException();
        }

        ListElement.Parse(moduleParser, data);
      }
    }

    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      if (ListElement == null) throw new ArgumentException("Array element type is not defined.");
      ListElement.ValidateReferences(simpleTypes, data, enums, events);
    }
  }
}
