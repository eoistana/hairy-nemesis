using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server.Login
{
	[DataContract]
	public partial class VerifyTokenResponseMessage
	{
		[DataMember] public bool Valid;

		public VerifyTokenResponseMessage(){}
		public VerifyTokenResponseMessage(bool Valid)
		{
			this.Valid = Valid;
		}

	}
}
