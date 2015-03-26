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
	/// Base class of the AI system
	/// 
	/// </summary>
	public partial class Ai : Actor
	{

		public Ai(int id, int width, int height, string name) : base(id, width, height, name)
		{
			OnAiInit();
		}

		partial void OnAiInit();

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
