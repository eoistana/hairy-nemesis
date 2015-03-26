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
	/// Base class of everything that can contain other things
	/// For instance a map, a chest or an actor
	/// 
	/// </summary>
	public partial class CellContainer : Cell
	{
		
		public int Width;
		
		public int Height;
		
		public Shape Shape;
		
		public Position Position;
		/// <summary>
		/// 2-dimensional array of cells. Can be used as inventory.
		/// </summary>
		public Cell[,] Cells;

		public CellContainer(int id, int width, int height) : base(id)
		{
			this.Width = width;
			this.Height = height;
			OnCellContainerInit();
		}

		partial void OnCellContainerInit();

		#region Events

		public override void Tick(TickEventParameters e)
		{
			base.Tick(e);
			var p = new TickEventParameters(e);
			OnTick(p);
			if(p.Continue)
			{
				if (Cells != null)
					Parallel.For(0, Cells.GetLength(0), 
						x1 => Parallel.For(0, Cells.GetLength(1), 
							y => { var a = Cells[x1, y]; if(a != null) a.Tick(p); }));
			}
		}


		public override void Collide(CollideEventParameters e)
		{
			base.Collide(e);
			var p = new CollideEventParameters(e);
			if(p.Continue)
			{
				if (Cells != null)
					Parallel.For(0, Cells.GetLength(0), 
						x1 => Parallel.For(0, Cells.GetLength(1), 
							y => { var a = Cells[x1, y]; if(a != null) a.Collide(p); }));
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

		partial void OnTick(TickEventParameters e);
		#endregion

		#region Messages

		partial void OnProcessUpdatePositionMessage(UpdatePositionMessage message);
		public void RegisterUpdatePositionMessage(UpdatePositionMessage message)
		{
			OnProcessUpdatePositionMessage(message);
		}
		#endregion
	}
}
