using System.ServiceModel;

namespace Server
{
	public partial class Service : IService
	{
		public string PostMobileEntityMoveMessage(int MobileEntityId, MoveMessage message)
		{
			// Find MobileEntity with id "MobileEntityId"
			// Check if valid request
			// Dispatch message
			return null;
		}

		public string PostCellContainerUpdatePositionMessage(int CellContainerId, UpdatePositionMessage message)
		{
			// Find CellContainer with id "CellContainerId"
			// Check if valid request
			// Dispatch message
			return null;
		}

		public string PostMobileEntityTurnMessage(int MobileEntityId, TurnMessage message)
		{
			// Find MobileEntity with id "MobileEntityId"
			// Check if valid request
			// Dispatch message
			return null;
		}

	}
}
