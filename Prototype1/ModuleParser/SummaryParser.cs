using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModulesParser
{
  public class SummaryParser
  {
    public string Summary;

    public SummaryParser(XmlNode node)
    {
      Summary = node.InnerText;
    }

    internal static SummaryParser Parse(ModuleParser moduleParser, DataParser dataParser)
    {
      foreach (XmlNode n in dataParser.Node.SelectNodes("./en:Summary", moduleParser.NsMan))
      {
        return new SummaryParser(n);
      }
      return null;
    }
  }
}
