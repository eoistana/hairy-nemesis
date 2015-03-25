using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	public partial class CollideEventParameters
	{
		public bool Continue;
		public Entity Entity;
		public double X;
		public double Y;

		public CollideEventParameters(Entity entity, double x, double y)
		{
			this.Entity = entity;
			this.X = x;
			this.Y = y;
		}

		public CollideEventParameters(CollideEventParameters e)
		{
			Continue = e.Continue;
			Entity = e.Entity;
			X = e.X;
			Y = e.Y;
		}

	}
}
