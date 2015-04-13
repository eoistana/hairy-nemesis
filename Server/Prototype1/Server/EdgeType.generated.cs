using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server
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
