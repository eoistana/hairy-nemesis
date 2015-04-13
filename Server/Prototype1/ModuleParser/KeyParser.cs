using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Key element
  /// </summary>
  public class KeyParser : Parser, IDeclaration
  {
    public string Ref;

    public FieldParser FieldRef;
    public ListParser ListRef;
    public ArrayParser ArrayRef;

    public IDeclaration RefDeclaration;

    public KeyParser(string @ref, XmlNode node) : base(@ref, node)
    {
      Ref = @ref;
    }


    private bool validated;
    internal void ValidateReference(Dictionary<string, FieldParser> fields, Dictionary<string, ListParser> lists, Dictionary<string, ArrayParser> arrays)
    {
      if (validated) return;
      validated = true;
      if (fields.ContainsKey(Ref)) RefDeclaration = FieldRef = fields[Ref];
      if (lists.ContainsKey(Ref)) RefDeclaration = ListRef = lists[Ref];
      if (arrays.ContainsKey(Ref)) RefDeclaration = ArrayRef = arrays[Ref];

      if (FieldRef == null && ListRef == null && ArrayRef == null) throw new ArgumentException("Key refers to nonexistant field", Ref);
    }

    public string GetDeclaration()
    {
      return GetTypeName() + " " + GetName();
    }

    public bool ImplementsEvent(string eventName)
    {
      return false;
    }


    public string GetTypeName()
    {
      return RefDeclaration.GetTypeName();
    }


    public string GetName()
    {
      return Ref.ToLower();
    }
  }
}
