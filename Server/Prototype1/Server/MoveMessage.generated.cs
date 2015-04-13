using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server
{
	[DataContract]
	public partial class MoveMessage
	{
		[DataMember] public double X;
		[DataMember] public double Y;
		[DataMember] public double Speed;

		public MoveMessage(){}
		public MoveMessage(double X, double Y, double Speed)
		{
			this.X = X;
			this.Y = Y;
			this.Speed = Speed;
		}

	}
}
