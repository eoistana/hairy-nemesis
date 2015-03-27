using System.ServiceModel;

namespace Server.Login
{
	public partial class LoginService : ILoginService
	{
		public LoginToken PostUserListLoginUserMessage(LoginUserMessage message)
		{
			// Singleton
			return UserList.Singleton.RegisterLoginUserMessage(message);
		}

	}
}
