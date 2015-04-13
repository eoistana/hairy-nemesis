using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  /// <summary>
  /// Parses a reference to a Field element
  /// </summary>
  public class FieldRefParser : Parser
  {
    public FieldRefParser(string name, XmlNode node)
      : base(name, node)
    {
    }

  }
}
