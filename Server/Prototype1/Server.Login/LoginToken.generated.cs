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
	public partial class LoginToken : IEquatable<LoginToken>
	{
		
		public string Token;

		public LoginToken(string token)
		{
			this.Token = token;
			OnLoginTokenInit();
		}

		partial void OnLoginTokenInit();

		public bool Equals(LoginToken obj)
		{
		  return base.Equals(obj);
		}

		public override bool Equals(object obj)
		{
		  return base.Equals(obj);
		}

		public override int GetHashCode()
		{
		  return base.GetHashCode();
		}

	}
}
