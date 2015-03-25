using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class KeyParser : Parser
  {
    public string Ref;

    public FieldParser FieldRef;
    public ListParser ListRef;
    public ArrayParser ArrayRef;
    public EnumRefParser EnumRef;

    public KeyParser(string @ref, XmlNode node) : base(@ref, node)
    {
      Ref = @ref;
    }


    internal void ValidateReference(Dictionary<string, FieldParser> fields, Dictionary<string, ListParser> lists, Dictionary<string, ArrayParser> arrays, Dictionary<string, EnumRefParser> enums)
    {
      if (fields.ContainsKey(Ref)) FieldRef = fields[Ref];
      if (lists.ContainsKey(Ref)) ListRef = lists[Ref];
      if (arrays.ContainsKey(Ref)) ArrayRef = arrays[Ref];
      if (enums.ContainsKey(Ref)) EnumRef = enums[Ref];

      if (FieldRef == null && ListRef == null && ArrayRef == null && EnumRef == null) throw new ArgumentException("Key refers to nonexistant field", Ref);
    }
  }
}
