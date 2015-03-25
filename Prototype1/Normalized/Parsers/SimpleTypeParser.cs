using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Normalized.Parsers
{
  public class SimpleTypeParser : Parser
  {
    public SimpleTypeParser(string name, XmlNode node)
      : base(name, node)
    {
    }
  }
}
