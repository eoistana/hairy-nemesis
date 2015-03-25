using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace ModulesParser
{
  public class ModuleParser
  {
    public string Filename;
    public XmlDocument Doc;

    public Dictionary<string, string> Modules = new Dictionary<string, string>();
    public Dictionary<string, UsingParser> Usings = new Dictionary<string, UsingParser>();
    public Dictionary<string, SimpleTypeParser> SimpleTypes = new Dictionary<string, SimpleTypeParser>();
    public Dictionary<string, DataParser> Data = new Dictionary<string, DataParser>();
    public Dictionary<string, EnumParser> Enums = new Dictionary<string, EnumParser>();
    public Dictionary<string, EventParser> Events = new Dictionary<string, EventParser>();
    public Dictionary<string, MessageParser> Messages = new Dictionary<string, MessageParser>();
    
    internal XmlNamespaceManager NsMan;

    public ModuleParser(string filename)
    {
      this.Filename = filename;
      var mp = this;
      Doc = Parse(filename, ref mp);
    }

    public void ValidateReferences()
    {
      foreach (var d in Data.Values) d.ValidateReferences(SimpleTypes, Data, Enums, Events, Messages);
      foreach (var e in Events.Values) e.ValidateReferences(SimpleTypes, Data, Enums, Events, Messages);
      foreach (var m in Messages.Values) m.ValidateReferences(SimpleTypes, Data, Enums, Events, Messages);
    }

    internal static XmlDocument Parse(string filename, ref ModuleParser moduleParser)
    {
      if (moduleParser == null) moduleParser = new ModuleParser(filename);

      var schema = XmlSchema.Read(new System.IO.StreamReader(Path.GetDirectoryName(filename) + "\\engine.xsd"), validationEventHandler);
      var Doc = new XmlDocument();
      Doc.Schemas.Add(schema);
      Doc.Load(filename);
      moduleParser.NsMan = new XmlNamespaceManager(Doc.NameTable);
      moduleParser.NsMan.AddNamespace("en", "engine");
      Doc.Validate(validationEventHandler);

      var moduleName = Doc.SelectSingleNode("/en:Module", moduleParser.NsMan).Attributes["name"].InnerText;
      if (moduleParser.Modules.ContainsKey(moduleName)) return null;

      moduleParser.Modules.Add(moduleName, null);

      moduleParser.Parse(Doc, moduleParser.Usings, "/en:Module/en:Using", "module", true);
      moduleParser.Parse(Doc, moduleParser.SimpleTypes, "//en:SimpleType");
      moduleParser.Parse(Doc, moduleParser.Data, "//en:Data");
      moduleParser.Parse(Doc, moduleParser.Enums, "/en:Module/en:Enum");
      moduleParser.Parse(Doc, moduleParser.Events, "/en:Module/en:Event");
      moduleParser.Parse(Doc, moduleParser.Messages, "/en:Module/en:Message");

      return Doc;
    }

    internal void Parse<T>(XmlNode node, Dictionary<string, T> dict, string xPath, string attr = "name", bool skipduplicates = false)
      where T : IParser
    {
      foreach (XmlNode n in node.SelectNodes(xPath, NsMan))
      {
        var name = n.Attributes[attr].InnerText;
        if (skipduplicates && dict.ContainsKey(name)) continue;
        var t = (T)Activator.CreateInstance(typeof(T), name, n);
        dict.Add(name, t);
        t.Parse(this, null);
      }
    }

    private static void validationEventHandler(object sender, ValidationEventArgs e)
    {
      throw new NotImplementedException();
    }
  }
}
