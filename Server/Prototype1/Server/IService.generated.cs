using System.ServiceModel;

namespace Server
{
	public partial interface IService
	{
		[OperationContract]
		string PostMobileEntityMoveMessage(int MobileEntityId, MoveMessage message);

		[OperationContract]
		string PostCellContainerUpdatePositionMessage(int CellContainerId, UpdatePositionMessage message);

		[OperationContract]
		string PostMobileEntityTurnMessage(int MobileEntityId, TurnMessage message);

		[OperationContract]
		string PostUserListLoginUserMessage(int UserListId, LoginUserMessage message);

	}
}
