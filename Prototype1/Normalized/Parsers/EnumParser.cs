using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Normalized.Parsers
{
  public class EnumParser : Parser
  {
    public EnumParser(string name, System.Xml.XmlNode node): base(name, node)
    {
    }

  }
}
