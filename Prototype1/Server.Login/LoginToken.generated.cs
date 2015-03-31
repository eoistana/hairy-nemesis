using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
{
	/// <summary>
	/// 
	/// Session token
	/// 
	/// </summary>
	public partial class LoginToken
	{
		
		public string Token;

		public LoginToken(string token)
		{
			this.Token = token;
			OnLoginTokenInit();
		}

		partial void OnLoginTokenInit();

	}
}
