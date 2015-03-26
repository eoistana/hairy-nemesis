using System.ServiceModel;

namespace Server
{
	public partial interface IService
	{
		[OperationContract]
		string PostUserListLoginUserMessage(int UserListId, LoginUserMessage message);

	}
}
