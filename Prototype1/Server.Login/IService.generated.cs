using System.ServiceModel;

namespace Server.Login
{
	public partial interface IService
	{
		[OperationContract]
		LoginToken PostUserListLoginUserMessage(LoginUserMessage message);

	}
}
