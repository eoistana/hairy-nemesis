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
	/// The game loop
	/// 
	/// </summary>
	public partial class GameLoop
	{
		
		public long time;
		
		public bool quit;
		public List<Map> Maps;

		public GameLoop()
		{
			this.time = 0;
			this.quit = true;
			OnGameLoopInit();
		}

		partial void OnGameLoopInit();

		#region List access

		protected readonly object MapsSyncRoot = new object();
		public void AddMaps(Map map)
		{
			lock (this.MapsSyncRoot)
			{
				if (this.Maps == null) this.Maps = new List<Map>();
				this.Maps.Add(map);
			}
		}

		internal void RemoveMaps(Map map)
		{
			lock (this.MapsSyncRoot)
			{
				this.Maps.Remove(map);
				if (!this.Maps.Any()) this.Maps = null;
			}
		}

		public Map GetMaps(int i)
		{
			lock (this.MapsSyncRoot)
			{
				return this.Maps[i];
			}
		}

		public IEnumerable<Map> SelectMaps()
		{
			Map[] list;
			lock (this.MapsSyncRoot)
			{
				list = this.Maps.ToArray();
			}
			foreach(var l in list) yield return l;
		}
		#endregion

		#region Events

		public virtual void Tick(TickEventParameters e)
		{
			var p = new TickEventParameters(e);
			if(p.Continue)
			{
				Map[] MapsCopy = null;
				lock(MapsSyncRoot)
				{
					if(Maps != null) 
						MapsCopy = Maps.ToArray();
				}
				if (MapsCopy != null) Parallel.ForEach(MapsCopy, x => { if (x != null)x.Tick(p); });
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
