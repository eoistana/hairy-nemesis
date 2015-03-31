using System.ServiceModel;

namespace Server.Login
{
	[ServiceContract]
	public partial interface ILoginClientService
	{
		[OperationContract]
		LoginUserResponseMessage PostUserListLoginUserMessage(LoginUserMessage message);

	}
}
