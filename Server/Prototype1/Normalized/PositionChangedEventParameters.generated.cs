using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	
	public partial class PositionChangedEventParameters
	{
		public bool Continue;

		public PositionChangedEventParameters()
		{
		}

		public PositionChangedEventParameters(PositionChangedEventParameters e)
		{
			Continue = e.Continue;
		}

	}
}
