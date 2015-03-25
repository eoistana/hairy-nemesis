using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class EnumRefParser : Parser
  {
    public string Ref;
    public EnumParser EnumType;
    public EnumRefParser(string @ref, XmlNode node) : base(@ref,node)
    {
      Ref = @ref;
    }


    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      if (!enums.ContainsKey(Ref)) throw new ArgumentException("Unknown enum type", Ref);

      EnumType = enums[Ref];
    }
  }
}
