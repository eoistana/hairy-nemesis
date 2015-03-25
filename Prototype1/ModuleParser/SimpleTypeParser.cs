using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ModulesParser
{
  public class SimpleTypeParser : Parser, IDeclaration
  {
    public string CsType;

    public SimpleTypeParser(string name, XmlNode node)
      : base(name, node)
    {
    }

    public override void Parse(ModuleParser moduleParser, DataParser data)
    {
      CsType = Node.Attributes["cstype"].InnerText;
    }

    public string GetDeclaration()
    {
      return CsType;
    }


    public bool ImplementsEvent(string eventName)
    {
      return false;
    }


    public string GetTypeName()
    {
      return CsType;
    }


    public string GetName()
    {
      return Name;
    }
  }
}
