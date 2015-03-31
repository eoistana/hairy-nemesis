using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
{
	/// <summary>
	/// 
	/// Holds the currently valid tokens
	/// 
	/// </summary>
	public partial class TokenUsage
	{
		
		public LoginToken Token;
		
		public DateTime LastAccessed;
		
		public User User;

		public TokenUsage(LoginToken token)
		{
			this.Token = token;
			OnTokenUsageInit();
		}

		partial void OnTokenUsageInit();

	}
}
