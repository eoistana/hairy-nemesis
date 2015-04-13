using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	
	public partial class Position
	{
		
		public double X;
		
		public double Y;
		
		public double Rotation;
		
		public CellContainer Parent;

		public Position(double x, double y, double rotation)
		{
			this.X = x;
			this.Y = y;
			this.Rotation = rotation;
			OnPositionInit();
		}

		partial void OnPositionInit();

		#region Events

		public virtual void Tick(TickEventParameters e)
		{
			var p = new TickEventParameters(e);
			if(p.Continue)
			{
			}
		}


		public virtual void Collide(CollideEventParameters e)
		{
			var p = new CollideEventParameters(e);
			if(p.Continue)
			{
			}
		}


		public virtual void PositionChanged(PositionChangedEventParameters e)
		{
			var p = new PositionChangedEventParameters(e);
			if(p.Continue)
			{
			}
		}

		#endregion


	}
}
