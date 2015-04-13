using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server.Login
{
	[DataContract]
	public partial class AddUserMessage
	{
		[DataMember] public string Name;
		[DataMember] public string Password;

		public AddUserMessage(){}
		public AddUserMessage(string Name, string Password)
		{
			this.Name = Name;
			this.Password = Password;
		}

	}
}
