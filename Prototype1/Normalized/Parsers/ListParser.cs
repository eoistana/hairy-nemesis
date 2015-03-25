using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public enum ListType
  {
    List,
    OrderedList,
    Dictionary
  }

  public class ListParser : Parser
  {
    public ListType Type;
    public IParser ListElement;

    public Dictionary<string, FieldRefParser> OrderBy = new Dictionary<string, FieldRefParser>();

    public ListParser(string name, XmlNode node) : base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["type"] != null)
        if (!Enum.TryParse(Node.Attributes["type"].InnerText, out Type)) throw new ArgumentOutOfRangeException("type", string.Format("Value: '{0}' is not defined.", Node.Attributes["type"].InnerText));

      Parse(moduleParser, data, OrderBy, "./en:OrderBy/en:FieldRef", "ref");

      var element = Node.SelectSingleNode("./en:Data|./en:DataRef|./en:Field|./en:EnumRef|./en:List|./en:Array", moduleParser.NsMan);
      if (element != null)
      {
        switch (element.Name)
        {
          case "Data":
            ListElement = new DataRefParser(element.Attributes["name"].InnerText, element);
            break;
          case "DataRef":
            ListElement = new DataRefParser(element.Attributes["ref"].InnerText, element);
            break;
          case "Field":
            ListElement = new FieldParser(element.Attributes["name"].InnerText, element);
            break;
          case "EnumRef":
            ListElement = new EnumRefParser(element.Attributes["ref"].InnerText, element);
            break;
          case "List":
            ListElement = new ListParser(element.Attributes["name"].InnerText, element);
            break;
          case "Array":
            ListElement = new ArrayParser(element.Attributes["name"].InnerText, element);
            break;
          default:
            throw new NotImplementedException();
        }

        ListElement.Parse(moduleParser, data);
      }
      
    }

    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      if (ListElement == null) throw new ArgumentException("No list element");

      ListElement.ValidateReferences(simpleTypes, data, enums, events);
    }
  }
}
