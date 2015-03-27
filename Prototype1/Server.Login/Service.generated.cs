using System.ServiceModel;

namespace Server.Login
{
	public partial class Service : IService
	{
		public LoginToken PostUserListLoginUserMessage(LoginUserMessage message)
		{
			// Singleton
			return UserList.Singleton.RegisterLoginUserMessage(message);
		}

	}
}
