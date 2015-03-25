
namespace Server
{
  public partial class Service : IService
  {
    public string GetData(string value)
    {
      return string.Format("You entered: {0}", value);
    }

    public string PostMessage(string owner, string message)
    {
      return null;
    }
  }
}
