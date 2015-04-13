using System.ServiceModel;

namespace Server.Login
{
	public partial class LoginClientService : ILoginClientService
	{
		public LoginUserResponseMessage PostUserListLoginUserMessage(LoginUserMessage message)
		{
			// Singleton
			return UserList.Singleton.RegisterLoginUserMessage(message);
		}

	}
}
