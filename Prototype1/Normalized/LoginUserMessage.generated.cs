using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	public partial class LoginUserMessage
	{
		public string Name;
		public string Password;

		public LoginUserMessage(string Name, string Password)
		{
			this.Name = Name;
			this.Password = Password;
		}

	}
}
