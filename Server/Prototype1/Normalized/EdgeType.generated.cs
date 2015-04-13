using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	[Flags]
	public enum EdgeType
	{
		None = 0x0000,
		BlockingPassage = 0x0001,
		BlockingView = 0x0002,
		BlockingLight = 0x0004,
	}
}
