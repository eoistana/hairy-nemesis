using System.ServiceModel;

namespace Server
{
  [ServiceContract]
  public partial interface IService
  {
    [OperationContract]
    string GetData(string value);

    [OperationContract]
    string PostMessage(string owner, string message);
  }


}
