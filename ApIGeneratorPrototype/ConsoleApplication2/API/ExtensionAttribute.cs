using System;

namespace API
{
  /// <summary>
  /// Marks a class as an extension to an API class
  /// </summary>
  public class ExtensionAttribute : Attribute
  {
    public string Name;
    public string Register;
    public string Mod;

    public ExtensionAttribute(string mod, string name, string register)
    {
      Mod = mod;
      Name = name;
      Register = register;
    }
  }
}