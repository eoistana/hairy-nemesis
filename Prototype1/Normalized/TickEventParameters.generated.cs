using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	/// <summary>
	/// Main game loop event
	/// </summary>
	public partial class TickEventParameters
	{
		public bool Continue;
		public long Time;

		public TickEventParameters(long time)
		{
			this.Time = time;
		}

		public TickEventParameters(TickEventParameters e)
		{
			Continue = e.Continue;
			Time = e.Time;
		}

	}
}
