using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server.Login
{
	[DataContract]
	public partial class LogoutUserMessage
	{
		[DataMember] public LoginToken Token;

		public LogoutUserMessage(){}
		public LogoutUserMessage(LoginToken Token)
		{
			this.Token = Token;
		}

	}
}
