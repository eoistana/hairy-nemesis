using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	public partial class MoveMessage
	{
		public double X;
		public double Y;
		public double Speed;

		public MoveMessage(double X, double Y, double Speed)
		{
			this.X = X;
			this.Y = Y;
			this.Speed = Speed;
		}

	}
}
