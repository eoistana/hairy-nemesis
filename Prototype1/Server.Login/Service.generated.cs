using System.ServiceModel;

namespace Server
{
	public partial class Service : IService
	{
		public string PostUserListLoginUserMessage(int UserListId, LoginUserMessage message)
		{
			// Find UserList with id "UserListId"
			// Check if valid request
			// Dispatch message
			return null;
		}

	}
}
