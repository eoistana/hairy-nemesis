using System.ServiceModel;

namespace Server.Login
{
	[ServiceContract]
	public partial interface ILoginServerService
	{
		[OperationContract]
		void PostUserListAddUserMessage(AddUserMessage message);

		[OperationContract]
		void PostUserListLogoutUserMessage(LogoutUserMessage message);

		[OperationContract]
		VerifyTokenResponseMessage PostUserListVerifyTokenMessage(VerifyTokenMessage message);

	}
}
