using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a Required element
  /// </summary>
  class RequiredParser : Parser, IDeclaration
  {
    public string Ref;

    public RequiredParser(string @ref, XmlNode node)
      : base(@ref, node)
    {
      Ref = @ref;
    }

    private bool validated;
    internal void ValidateReference(Dictionary<string, ModulesParser.FieldParser> Fields, Dictionary<string, ModulesParser.ListParser> Lists, Dictionary<string, ModulesParser.ArrayParser> Arrays)
    {
     
    }

    public string GetDeclaration()
    {
      throw new NotImplementedException();
    }

    public bool ImplementsEvent(string eventName)
    {
      throw new NotImplementedException();
    }

    public string GetTypeName()
    {
      throw new NotImplementedException();
    }

    public string GetName()
    {
      throw new NotImplementedException();
    }
  }
}
