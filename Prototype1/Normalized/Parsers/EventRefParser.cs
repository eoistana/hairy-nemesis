using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Normalized.Parsers
{
  public class EventRefParser : Parser
  {
    public EventRefParser(string name, XmlNode node)
      : base(name, node)
    {
    }

  }
}
