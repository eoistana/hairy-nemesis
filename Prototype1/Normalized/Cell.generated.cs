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
	/// Base class of everything in the engine      
	/// 
	/// </summary>
	public partial class Cell
	{
		/// <summary>
		/// The main ID of the object
		/// </summary>
		public int Id;
		
		public double Scale;
		
		public CellType CellType;
		public List<Entity> Entities;

		public Cell(int id)
		{
			this.Id = id;
			this.Scale = 1;
			OnCellInit();
		}

		partial void OnCellInit();

		#region List access

		protected readonly object EntitiesSyncRoot = new object();
		public void AddEntities(Entity enity)
		{
			lock (this.EntitiesSyncRoot)
			{
				if (this.Entities == null) this.Entities = new List<Entity>();
				this.Entities.Add(enity);
			}
		}

		internal void RemoveEntities(Entity enity)
		{
			lock (this.EntitiesSyncRoot)
			{
				this.Entities.Remove(enity);
				if (!this.Entities.Any()) this.Entities = null;
			}
		}
		#endregion

		#region Events

		public virtual void Tick(TickEventParameters e)
		{
			var p = new TickEventParameters(e);
			if(p.Continue)
			{
				Entity[] EntitiesCopy = null;
				lock(EntitiesSyncRoot)
				{
					if(Entities != null) 
						EntitiesCopy = Entities.ToArray();
				}
				if (EntitiesCopy != null) Parallel.ForEach(EntitiesCopy, x => { if (x != null)x.Tick(p); });
			}
		}


		public virtual void Collide(CollideEventParameters e)
		{
			var p = new CollideEventParameters(e);
			if(p.Continue)
			{
				Entity[] EntitiesCopy = null;
				lock(EntitiesSyncRoot)
				{
					if(Entities != null) 
						EntitiesCopy = Entities.ToArray();
				}
				if (EntitiesCopy != null) Parallel.ForEach(EntitiesCopy, x => { if (x != null)x.Collide(p); });
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
