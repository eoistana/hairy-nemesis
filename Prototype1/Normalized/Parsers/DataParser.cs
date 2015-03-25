using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Normalized.Parsers
{
  public class DataParser : Parser
  {
    public string Base;

    public Dictionary<string, ExtendsParser> Extends = new Dictionary<string, ExtendsParser>();
    public Dictionary<string, FieldParser> Fields = new Dictionary<string, FieldParser>();
    public Dictionary<string, ListParser> Lists = new Dictionary<string, ListParser>();
    public Dictionary<string, ArrayParser> Arrays = new Dictionary<string, ArrayParser>();
    public Dictionary<string, EnumRefParser> Enums = new Dictionary<string, EnumRefParser>();

    public Dictionary<string, KeyParser> Keys = new Dictionary<string, KeyParser>();
    public Dictionary<string, EventRefParser> Events = new Dictionary<string, EventRefParser>();

    public DataParser(string name, XmlNode node): base(name, node)
    {
    }

    internal void ParseData(ModuleParser moduleParser)
    {
      Parse(moduleParser, this, Extends, "./en:Extends", "ref");
      Parse(moduleParser, this, Fields, "./en:Field");
      Parse(moduleParser, this, Lists, "./en:List");
      Parse(moduleParser, this, Arrays, "./en:Array");
      Parse(moduleParser, this, Enums, "./en:Enum");
      Parse(moduleParser, this, Enums, "./en:EnumRef", "ref");

      Parse(moduleParser, this, Keys, "./en:Key/en:FieldRef", "ref");
      Parse(moduleParser, this, Events, "./en:Events/en:Event");
      Parse(moduleParser, this, Events, "./en:Events/en:EventRef", "ref");
    }

    

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      if (Node.Attributes["base"] != null) Base = Node.Attributes["base"].InnerText;
      ParseData(moduleParser);
    }

    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      foreach (var f in Fields.Values) f.ValidateReferences(simpleTypes, data, enums, events);
      foreach (var l in Lists.Values) l.ValidateReferences(simpleTypes, data, enums, events);
      foreach (var a in Arrays.Values) a.ValidateReferences(simpleTypes, data, enums, events);
      foreach (var e in Enums.Values) e.ValidateReferences(simpleTypes, data, enums, events);

      foreach (var k in Keys.Values) k.ValidateReference(Fields, Lists, Arrays, Enums);
    }
  }

  public interface IParser
  {
    void Parse(ModuleParser moduleParser, DataParser data);
    void Parse<T>(ModuleParser moduleParser, DataParser data, Dictionary<string, T> dict, string xPath, string attr = "name")
      where T:IParser;

    void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events);
  }
}
