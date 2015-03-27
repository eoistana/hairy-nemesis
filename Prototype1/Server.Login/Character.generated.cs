using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Login
{
	
	public partial class Character
	{
		
		public string Name;

		public Character(string name)
		{
			this.Name = name;
			OnCharacterInit();
		}

		partial void OnCharacterInit();

	}
}
