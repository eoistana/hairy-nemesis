using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{

	public partial class Entity : CellContainer
	{
		
		public string Name;

		public Entity(int id, int width, int height, string name) : base(id, width, height)
		{
			this.Name = name;
			OnEntityInit();
		}

		partial void OnEntityInit();

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
			OnCollide(p);
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

		partial void OnCollide(CollideEventParameters e);
		#endregion

	}
}
