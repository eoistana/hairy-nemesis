using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server
{
	[DataContract]
	public partial class LoginUserMessage
	{
		[DataMember] public string Name;
		[DataMember] public string Password;

		public LoginUserMessage(){}
		public LoginUserMessage(string Name, string Password)
		{
			this.Name = Name;
			this.Password = Password;
		}

	}
}
