using System.ServiceModel;

namespace Server.Login
{
	public partial class LoginServerService : ILoginServerService
	{
		public void PostUserListAddUserMessage(AddUserMessage message)
		{
			// Singleton
			UserList.Singleton.RegisterAddUserMessage(message);
		}

		public void PostUserListLogoutUserMessage(LogoutUserMessage message)
		{
			// Singleton
			UserList.Singleton.RegisterLogoutUserMessage(message);
		}

		public VerifyTokenResponseMessage PostUserListVerifyTokenMessage(VerifyTokenMessage message)
		{
			// Singleton
			return UserList.Singleton.RegisterVerifyTokenMessage(message);
		}

	}
}
