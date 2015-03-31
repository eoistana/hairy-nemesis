using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Data element
  /// </summary>
  public class DataParser : Parser, IDeclaration
  {
    public string Base;
    public bool Singleton;
    public DataParser BaseRef;

    public SummaryParser Summary;
    public Dictionary<string, ExtendsParser> Extends = new Dictionary<string, ExtendsParser>();
    public Dictionary<string, FieldParser> Fields = new Dictionary<string, FieldParser>();
    public Dictionary<string, ListParser> Lists = new Dictionary<string, ListParser>();
    public Dictionary<string, ArrayParser> Arrays = new Dictionary<string, ArrayParser>();

    public Dictionary<string, KeyParser> Required = new Dictionary<string, KeyParser>();
    public Dictionary<string, KeyParser> Keys = new Dictionary<string, KeyParser>();
    public Dictionary<string, EventRefParser> Events = new Dictionary<string, EventRefParser>();
    public Dictionary<string, MessageRefParser> Messages = new Dictionary<string, MessageRefParser>();
    public Dictionary<string, DataParser> Subclasses = new Dictionary<string, DataParser>();

    public DataParser(string name, XmlNode node): base(name, node)
    {
    }

    internal void ParseData(ModuleParser moduleParser)
    {
      Summary = SummaryParser.Parse(moduleParser, this);
      Parse(moduleParser, this, Extends, "./en:Extends", "ref");
      Parse(moduleParser, this, Fields, "./en:Field");
      Parse(moduleParser, this, Lists, "./en:List");
      Parse(moduleParser, this, Arrays, "./en:Array");

      Parse(moduleParser, this, Required, "./en:Required/en:FieldRef", "ref");
      Parse(moduleParser, this, Keys, "./en:Key/en:FieldRef", "ref");
      Parse(moduleParser, this, Events, "./en:Events/en:Implement", "event");
      Parse(moduleParser, this, Messages, "./en:Messages/en:Message");
      Parse(moduleParser, this, Messages, "./en:Messages/en:MessageRef", "ref");

    }

    

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["base"] != null) Base = Node.Attributes["base"].InnerText;
      if (Node.Attributes["singleton"] != null) Singleton = Node.Attributes["singleton"].InnerText == "true";
      ParseData(moduleParser);
    }

    private bool validated = false;
    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages)
    {
      if (validated) return;
      validated = true;
      foreach (var x in Extends.Values)
      {
        x.ValidateReferences(simpleTypes, data, enums, events, messages);
        switch (x.ParentType)
        {
          case ExtendsFieldType.Field:
            if (data[x.Ref].Fields.ContainsKey(Name)) throw new ArgumentException("Field already exists", Name);
            data[x.Ref].Fields.Add(Name, FieldParser.New(Name, this));
            Fields.Add(x.ParentVar, FieldParser.New(x.ParentVar, data[x.Ref]));
            break;
          case ExtendsFieldType.List:
            if (data[x.Ref].Lists.ContainsKey(Name)) throw new ArgumentException("ListField already exists", Name);
            data[x.Ref].Lists.Add(Name, ListParser.New(Name, this));
            Fields.Add(x.ParentVar, FieldParser.New(x.ParentVar, data[x.Ref]));
            break;
          default:
            throw new NotImplementedException(x.ParentType.ToString());
        }
      }

      foreach (var f in Fields.Values) f.ValidateReferences(simpleTypes, data, enums, events, messages);
      foreach (var l in Lists.Values) l.ValidateReferences(simpleTypes, data, enums, events, messages);
      foreach (var a in Arrays.Values) a.ValidateReferences(simpleTypes, data, enums, events, messages);

      foreach (var r in Required.Values) r.ValidateReference(Fields, Lists, Arrays);
      foreach (var k in Keys.Values) k.ValidateReference(Fields, Lists, Arrays);
      foreach (var e in Events.Values) e.ValidateReferences(simpleTypes, data, enums, events, messages);
      foreach (var m in Messages.Values) m.ValidateReferences(simpleTypes, data, enums, events, messages);
      if (Base != null)
      {
        if (!data.ContainsKey(Base)) throw new ArgumentException("Base class not found", Base);
        BaseRef = data[Base];
        BaseRef.Subclasses[Name] = this;
      }
    }

    public string GetDeclaration()
    {
      return Name;
    }

    public string GetTypeName()
    {
      return Name;
    }

    public string GetBase()
    {
      return (Base != null) ? " : " + Base : "";
    }

    public string GetEqualityInterface()
    {
      var eq = "IEquatable<" + Name + ">";
      return ((Base == null) ? " : " : ", ") + eq;
    }

    public string GetCtorParams()
    {
      var basector = (BaseRef != null) ? BaseRef.GetCtorParams() : "";
      var ctor = string.Join(", ", Keys.Values.Concat(Required.Values).Select(k => k.GetDeclaration()));
      return basector + ((basector!="" && ctor!="")?", ":"") + ctor;
    }

    public string GetCtorParamsNames()
    {
      var basector = (BaseRef != null) ? BaseRef.GetCtorParamsNames() : "";
      var ctor = string.Join(", ", Keys.Values.Concat(Required.Values).Select(k=>k.GetName()));
      return basector + ((basector != "" && ctor != "") ? ", " : "") + ctor;
    }

    public string GetBaseCtor()
    {
      return (BaseRef != null) ? " : base(" + BaseRef.GetCtorParamsNames() + ")" : "";
    }

    public string GetFieldDeclarations(string tabs)
    {
      var field = string.Join("\n" + tabs, Fields.Values.Select(f => f.GetSummary(tabs) + "\n" + tabs + f.GetPublicDeclaration()));
      if (Singleton)
      {
        var single = new StringBuilder();
        single.AppendLine("private static " + Name + " singleton = null;");
        single.AppendLine(tabs + "public static " + Name + " Singleton");
        single.AppendLine(tabs + "{");
        single.AppendLine(tabs + "  get");
        single.AppendLine(tabs + "  {");
        single.AppendLine(tabs + "    if (singleton == null) singleton = new " + Name + "();");
        single.AppendLine(tabs + "    return singleton;");
        single.AppendLine(tabs + "  }");
        single.AppendLine(tabs + "}");
        field = single.ToString() + (field.Length > 0 ? tabs + field + "\n" : "");
      }
      return field.Length > 0 ? tabs + field + "\n" : "";
    }

    public string GetListDeclarations(string tabs)
    {
      var list = string.Join("\n" + tabs, Lists.Values.Select(l => l.GetPublicDeclaration()));
      return list.Length > 0 ? tabs + list + "\n" : "";
    }

    public string GetArrayDeclarations(string tabs)
    {
      var arr = string.Join("\n" + tabs, Arrays.Values.Select(a => a.GetSummary(tabs) + "\n" + tabs + a.GetPublicDeclaration()));
      return arr.Length > 0 ? tabs + arr + "\n" : "";
    }

    private Dictionary<string, bool> implementedEvents = new Dictionary<string, bool>();
    public bool ImplementsEvent(string eventName)
    {
      if (implementedEvents.ContainsKey(eventName)) return implementedEvents[eventName];
      implementedEvents[eventName] = false; // Stop circular loops
      if (Events.ContainsKey(eventName)) return implementedEvents[eventName] = true;
      if (Fields.Values.Any(f => f.ImplementsEvent(eventName))) return implementedEvents[eventName] = true;
      if (Lists.Values.Any(l => l.ImplementsEvent(eventName))) return implementedEvents[eventName] = true;
      if (Arrays.Values.Any(a => a.ImplementsEvent(eventName))) return implementedEvents[eventName] = true;
      if (Subclasses.Values.Any(s => s.ImplementsEvent(eventName))) return implementedEvents[eventName] = true;
      return implementedEvents[eventName] = false;
    }

    public bool RespondsToMessage(string messageName)
    {
      return Messages.ContainsKey(messageName);
    }

    public string GetName()
    {
      return Name;
    }

    public Dictionary<string, string> GetKeyNamesAndTypes(string prefix)
    {
      var bases = (BaseRef!=null) ? BaseRef.GetKeyNamesAndTypes(prefix) : new Dictionary<string,string>();
      return bases.Concat(Keys.Values.SelectMany(
        k => k.RefDeclaration is DataParser ? 
          (k.RefDeclaration as DataParser).GetKeyNamesAndTypes(prefix) : 
          new Dictionary<string, string> { { prefix + k.RefDeclaration.GetName(), k.RefDeclaration.GetTypeName() } })
        ).ToDictionary(k=>k.Key, v=>v.Value);
    }

    public string GetSummary(string tabs)
    {
      return GetSummary(tabs, Summary != null ? Summary.Summary : null);
    }

    public static string GetSummary(string tabs, string summary)
    {
      if (summary != null)
      {
        var reg = new Regex("\n\\s*");
        summary = reg.Replace(summary, "\n");
        var separator = "\n" + tabs + "/// ";
        return string.Format(@"/// <summary>
{0}/// {1}
{0}/// </summary>", tabs, string.Join(separator, summary.Split('\n')));
      }
      return "";
    }
  }

  public interface IParser
  {
    void Parse(ModuleParser moduleParser, DataParser data);
    void Parse<T>(ModuleParser moduleParser, DataParser data, Dictionary<string, T> dict, string xPath, string attr = "name")
      where T:IParser;

    void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events, Dictionary<string, MessageParser> messages);
  }

  public interface IDeclaration
  {
    string GetDeclaration();
    bool ImplementsEvent(string eventName);

    string GetTypeName();
    string GetName();
  }
}
