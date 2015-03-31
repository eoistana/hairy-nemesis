using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
{
	
	public partial class Character : IEquatable<Character>
	{
		
		public string Name;

		public Character(string name)
		{
			this.Name = name;
			OnCharacterInit();
		}

		partial void OnCharacterInit();

		public bool Equals(Character obj)
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
