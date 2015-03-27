using System.ServiceModel;

namespace Server.Login
{
	public partial interface ILoginService
	{
		[OperationContract]
		LoginToken PostUserListLoginUserMessage(LoginUserMessage message);

	}
}
