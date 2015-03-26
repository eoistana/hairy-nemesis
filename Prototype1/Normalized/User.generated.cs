using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	
	public partial class User
	{
		/// <summary>
		/// The username
		/// </summary>
		public string Name;
		/// <summary>
		/// The password. Stored in plain text for now.
		/// </summary>
		public string Password;
		public List<Character> Characters;

		public User(string name, string password)
		{
			this.Name = name;
			this.Password = password;
			OnUserInit();
		}

		partial void OnUserInit();

		#region List access

		protected readonly object CharactersSyncRoot = new object();
		public void AddCharacters(Character character)
		{
			lock (this.CharactersSyncRoot)
			{
				if (this.Characters == null) this.Characters = new List<Character>();
				this.Characters.Add(character);
			}
		}

		internal void RemoveCharacters(Character character)
		{
			lock (this.CharactersSyncRoot)
			{
				this.Characters.Remove(character);
				if (!this.Characters.Any()) this.Characters = null;
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
