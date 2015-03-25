using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class DataRefParser : Parser
  {
    public string Ref;
    public DataParser DataType;

    public DataRefParser(string @ref, XmlNode node):base(@ref,node)
    {
      Ref = @ref;
    }

    public override void ValidateReferences(Dictionary<string, SimpleTypeParser> simpleTypes, Dictionary<string, DataParser> data, Dictionary<string, EnumParser> enums, Dictionary<string, EventParser> events)
    {
      if (data.ContainsKey(Ref)) DataType = data[Ref];

      if (DataType == null) throw new ArgumentException("Referenced data element does not exist", Ref);
    }
  }
}
