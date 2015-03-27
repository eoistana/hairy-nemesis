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
	public partial class UserList
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

		public UserList()
		{
			OnUserListInit();
		}

		partial void OnUserListInit();

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
		#endregion

		#region Messages

		public class LoginUserMessageReturnValue
		{
			public LoginToken ReturnValue;
		}
		partial void OnProcessLoginUserMessage(LoginUserMessage message, LoginUserMessageReturnValue returnValue);
		public LoginToken RegisterLoginUserMessage(LoginUserMessage message)
		{
			var returnValue = new LoginUserMessageReturnValue();
			OnProcessLoginUserMessage(message, returnValue);
			return returnValue.ReturnValue;
		}
		#endregion
	}
}
