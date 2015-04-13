using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server
{
	[DataContract]
	public partial class UpdatePositionMessage
	{
		[DataMember] public int EntityId;
		[DataMember] public double X;
		[DataMember] public double Y;

		public UpdatePositionMessage(){}
		public UpdatePositionMessage(int EntityId, double X, double Y)
		{
			this.EntityId = EntityId;
			this.X = X;
			this.Y = Y;
		}

	}
}
