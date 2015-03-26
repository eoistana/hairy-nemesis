using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	/// <summary>
	/// 
	/// Singleton class for the list of users
	/// 
	/// </summary>
	public partial class UserList
	{
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

		#region Events

		public virtual void Tick(TickEventParameters e)
		{
			var p = new TickEventParameters(e);
			if(p.Continue)
			{
			}
		}


		public virtual void Collide(CollideEventParameters e)
		{
			var p = new CollideEventParameters(e);
			if(p.Continue)
			{
			}
		}


		public virtual void PositionChanged(PositionChangedEventParameters e)
		{
			var p = new PositionChangedEventParameters(e);
			if(p.Continue)
			{
			}
		}

		#endregion

	}
}
