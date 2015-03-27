using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
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

	}
}
