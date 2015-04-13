using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
{
	
	public partial class User : IEquatable<User>
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

		public bool Equals(User obj)
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

		public Character GetCharacters(int i)
		{
			lock (this.CharactersSyncRoot)
			{
				return this.Characters[i];
			}
		}

		public IEnumerable<Character> SelectCharacters()
		{
			Character[] list;
			lock (this.CharactersSyncRoot)
			{
				list = this.Characters.ToArray();
			}
			foreach(var l in list) yield return l;
		}
		#endregion

	}
}
