using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
{
	/// <summary>
	/// 
	/// Singleton class for the list of users
	/// 
	/// </summary>
	public partial class UserList : IEquatable<UserList>
	{
		private static UserList singleton = null;
		public static UserList Singleton
		{
		  get
		  {
		    if (singleton == null) singleton = new UserList();
		    return singleton;
		  }
		}

		public List<User> Users;
		public List<TokenUsage> Tokens;

		public UserList()
		{
			OnUserListInit();
		}

		partial void OnUserListInit();

		public bool Equals(UserList obj)
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

		#region List access

		protected readonly object UsersSyncRoot = new object();
		public void AddUsers(User user)
		{
			lock (this.UsersSyncRoot)
			{
				if (this.Users == null) this.Users = new List<User>();
				this.Users.Add(user);
			}
		}

		internal void RemoveUsers(User user)
		{
			lock (this.UsersSyncRoot)
			{
				this.Users.Remove(user);
				if (!this.Users.Any()) this.Users = null;
			}
		}

		public User GetUsers(int i)
		{
			lock (this.UsersSyncRoot)
			{
				return this.Users[i];
			}
		}

		public IEnumerable<User> SelectUsers()
		{
			User[] list;
			lock (this.UsersSyncRoot)
			{
				list = this.Users.ToArray();
			}
			foreach(var l in list) yield return l;
		}

		protected readonly object TokensSyncRoot = new object();
		public void AddTokens(TokenUsage tokenUsage)
		{
			lock (this.TokensSyncRoot)
			{
				if (this.Tokens == null) this.Tokens = new List<TokenUsage>();
				this.Tokens.Add(tokenUsage);
			}
		}

		internal void RemoveTokens(TokenUsage tokenUsage)
		{
			lock (this.TokensSyncRoot)
			{
				this.Tokens.Remove(tokenUsage);
				if (!this.Tokens.Any()) this.Tokens = null;
			}
		}

		public TokenUsage GetTokens(int i)
		{
			lock (this.TokensSyncRoot)
			{
				return this.Tokens[i];
			}
		}

		public IEnumerable<TokenUsage> SelectTokens()
		{
			TokenUsage[] list;
			lock (this.TokensSyncRoot)
			{
				list = this.Tokens.ToArray();
			}
			foreach(var l in list) yield return l;
		}
		#endregion

		#region Messages

		partial void OnProcessAddUserMessage(AddUserMessage message);
		public void RegisterAddUserMessage(AddUserMessage message)
		{
			OnProcessAddUserMessage(message);
		}
		private class LoginUserMessageReturnValue
		{
			public LoginUserResponseMessage ReturnValue = null;
		}
		partial void OnProcessLoginUserMessage(LoginUserMessage message, LoginUserMessageReturnValue returnValue);
		public LoginUserResponseMessage RegisterLoginUserMessage(LoginUserMessage message)
		{
			var returnValue = new LoginUserMessageReturnValue();
			OnProcessLoginUserMessage(message, returnValue);
			return returnValue.ReturnValue;
		}
		partial void OnProcessLogoutUserMessage(LogoutUserMessage message);
		public void RegisterLogoutUserMessage(LogoutUserMessage message)
		{
			OnProcessLogoutUserMessage(message);
		}
		private class VerifyTokenMessageReturnValue
		{
			public VerifyTokenResponseMessage ReturnValue = null;
		}
		partial void OnProcessVerifyTokenMessage(VerifyTokenMessage message, VerifyTokenMessageReturnValue returnValue);
		public VerifyTokenResponseMessage RegisterVerifyTokenMessage(VerifyTokenMessage message)
		{
			var returnValue = new VerifyTokenMessageReturnValue();
			OnProcessVerifyTokenMessage(message, returnValue);
			return returnValue.ReturnValue;
		}
		#endregion
	}
}
