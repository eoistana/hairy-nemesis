using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	
	public partial class ShapeDefinition
	{
		
		public double Height;
		public List<Edge> Edges;

		public ShapeDefinition()
		{
			OnShapeDefinitionInit();
		}

		partial void OnShapeDefinitionInit();

		#region List access

		protected readonly object EdgesSyncRoot = new object();
		public void AddEdges(Edge edge)
		{
			lock (this.EdgesSyncRoot)
			{
				if (this.Edges == null) this.Edges = new List<Edge>();
				this.Edges.Add(edge);
			}
		}

		internal void RemoveEdges(Edge edge)
		{
			lock (this.EdgesSyncRoot)
			{
				this.Edges.Remove(edge);
				if (!this.Edges.Any()) this.Edges = null;
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
