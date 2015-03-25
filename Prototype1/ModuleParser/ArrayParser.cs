using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses an Array element
  /// </summary>
  public class ArrayParser : Parser, IDeclaration
  {
    public IDeclaration ListElement;

    public ArrayParser(string name, XmlNode node): base(name, node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      var element = Node.SelectSingleNode("./en:Element|./en:List|./en:Array", moduleParser.NsMan);
      var name = element.Attributes["name"] != null ? element.Attributes["name"].InnerText : null;
      var @ref = element.Attributes["ref"] != null ? element.Attributes["ref"].InnerText : null;
      if (element != null)
      {
        switch (element.Name)
        {
          case "Element":
            ListElement = new FieldParser(name, element);
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

        (ListElement as IParser).Parse(moduleParser, data);
      }
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      if (ListElement == null) throw new ArgumentException("Array element type is not defined.");
      (ListElement as IParser).ValidateReferences(simpleTypes, data, enums, events, messages);
    }

    public string GetDeclaringType(bool nested)
    {
      if (ListElement is ArrayParser) return (ListElement as ArrayParser).GetDeclaringType(true) + (nested ? "," : ",] ");
      return (ListElement as IDeclaration).GetTypeName() + "[" + (nested ? "" : "] ");
    }

    public string GetDeclaration()
    {
      return GetDeclaringType(false) + Name;
    }

    private Dictionary<string, bool> implementedEvents = new Dictionary<string, bool>();
    public bool ImplementsEvent(string eventName)
    {
      if (implementedEvents.ContainsKey(eventName)) return implementedEvents[eventName];
      return implementedEvents[eventName]=(ListElement as IDeclaration).ImplementsEvent(eventName);
    }

    public string GetTypeName()
    {
      return GetDeclaringType(false);
    }

    internal string GetPublicDeclaration()
    {
      return "public " + GetDeclaration() + ";";
    }

    public string GetEventCaller(string tabs, int nesting, string arrName = null)
    {
      return string.Format("{1}Parallel.For(0, {2}.GetLength({3}), \n{0}{4}", 
        tabs, 
        (nesting>0)?string.Format("x{0} => ",nesting):"", 
        arrName??Name, 
        nesting, 
        (ListElement is ArrayParser)?(ListElement as ArrayParser).GetEventCaller(tabs+"\t", nesting+1, Name):"");
    }

    public string GetEventLooper(int nesting)
    {
      return (ListElement is ArrayParser) ? string.Format("x{0}, {1}",++nesting, (ListElement as ArrayParser).GetEventLooper(nesting)) : "y";
    }

    public string GetEventClosure(int nesting)
    {
      return (ListElement is ArrayParser) ? ")" + (ListElement as ArrayParser).GetEventClosure(nesting+1) : ")";
    }


    public string GetName()
    {
      return ListElement.GetName();
    }
  }
}
