using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	
	public partial class MovementType
	{
		public List<TypesList> Types;

		public MovementType()
		{
			OnMovementTypeInit();
		}

		partial void OnMovementTypeInit();

		#region List access

		protected readonly object TypesSyncRoot = new object();
		public void AddTypes(TypesList type)
		{
			lock (this.TypesSyncRoot)
			{
				if (this.Types == null) this.Types = new List<TypesList>();
				this.Types.Add(type);
			}
		}

		internal void RemoveTypes(TypesList type)
		{
			lock (this.TypesSyncRoot)
			{
				this.Types.Remove(type);
				if (!this.Types.Any()) this.Types = null;
			}
		}
		#endregion

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
