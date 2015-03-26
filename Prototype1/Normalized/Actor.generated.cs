using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	/// <summary>
	/// 
	/// Base class for objects that can post commands
	/// 
	/// </summary>
	public partial class Actor : MobileEntity
	{

		public Actor(int id, int width, int height, string name) : base(id, width, height, name)
		{
			OnActorInit();
		}

		partial void OnActorInit();

		#region Events

		public override void Tick(TickEventParameters e)
		{
			base.Tick(e);
			var p = new TickEventParameters(e);
			if(p.Continue)
			{
			}
		}


		public override void Collide(CollideEventParameters e)
		{
			base.Collide(e);
			var p = new CollideEventParameters(e);
			if(p.Continue)
			{
			}
		}


		public override void PositionChanged(PositionChangedEventParameters e)
		{
			base.PositionChanged(e);
			var p = new PositionChangedEventParameters(e);
			if(p.Continue)
			{
			}
		}

		#endregion

	}
}
