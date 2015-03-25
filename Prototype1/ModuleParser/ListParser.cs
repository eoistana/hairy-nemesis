using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public enum ListType
  {
    List,
    OrderedList,
    Dictionary
  }

  public class ListParser : Parser, IDeclaration
  {
    public ListType Type;
    public IDeclaration ListElement;

    public Dictionary<string, FieldRefParser> OrderBy = new Dictionary<string, FieldRefParser>();

    public ListParser(string name, XmlNode node) : base(name,node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["type"] != null)
        if (!Enum.TryParse(Node.Attributes["type"].InnerText, out Type)) throw new ArgumentOutOfRangeException("type", string.Format("Value: '{0}' is not defined.", Node.Attributes["type"].InnerText));

      Parse(moduleParser, data, OrderBy, "./en:OrderBy/en:FieldRef", "ref");

      var element = Node.SelectSingleNode("./en:Element|./en:List|./en:Array", moduleParser.NsMan);
      if (element != null)
      {
        switch (element.Name)
        {
          case "Element":
            ListElement = new FieldParser(element.Attributes["name"].InnerText, element);
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

        (ListElement as Parser).Parse(moduleParser, data);
      }
      
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (ListElement == null) throw new ArgumentException("No list element");

      (ListElement as Parser).ValidateReferences(simpleTypes, data, enums, events, messages);
    }

    public string GetDeclaringType()
    {
      return "List<" + (ListElement as IDeclaration).GetTypeName() + "> ";
    }

    public string GetDeclaration()
    {
      return GetDeclaringType() + Name;
    }

    public string GetDefaultValue()
    {
      return "";
    }

    internal string GetPublicDeclaration()
    {
      return "public " + GetDeclaration() + GetDefaultValue() + ";";
    }

    public bool ImplementsEvent(string eventName)
    {
      return ListElement.ImplementsEvent(eventName);
    }

    internal static ListParser New(string name, DataParser dataParser)
    {
      var f = new ListParser(name, null);
      f.Type = ListType.List;
      f.ListElement = DataRefParser.New(name, dataParser);
      return f;
    }

    public bool IsExtendedProperty(DataParser data)
    {
      return (ListElement is DataRefParser) ? (ListElement as DataRefParser).DataType.Extends.ContainsKey(data.Name) : false;
    }



    public string GetTypeName()
    {
      return GetDeclaringType();
    }


    public string GetName()
    {
      return Name;
    }
  }
}
