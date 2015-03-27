using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	public partial class UpdatePositionMessage
	{
		public Entity Entity;
		public double X;
		public double Y;

		public UpdatePositionMessage(Entity Entity, double X, double Y)
		{
			this.Entity = Entity;
			this.X = X;
			this.Y = Y;
		}

	}
}
