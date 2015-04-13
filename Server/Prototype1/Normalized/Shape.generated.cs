using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Normalized;
using Normalized.Maths;

namespace Modules.Engine
{
	
	public partial class Shape
	{
		
		public Position position;
		
		public bool edgesRecalculated;
		
		public bool boundingCircleRecalculated;
		
		public double boundingCircleRadiusSquared;
		
		public bool linesRecalculated;
		
		public ShapeDefinition Definition;
		public List<Edge> edges;
		public List<Line> lines;

		public Shape(ShapeDefinition definition)
		{
			this.Definition = definition;
			OnShapeInit();
		}

		partial void OnShapeInit();

		#region List access

		protected readonly object edgesSyncRoot = new object();
		public void Addedges(Edge edge)
		{
			lock (this.edgesSyncRoot)
			{
				if (this.edges == null) this.edges = new List<Edge>();
				this.edges.Add(edge);
			}
		}

		internal void Removeedges(Edge edge)
		{
			lock (this.edgesSyncRoot)
			{
				this.edges.Remove(edge);
				if (!this.edges.Any()) this.edges = null;
			}
		}

		public Edge Getedges(int i)
		{
			lock (this.edgesSyncRoot)
			{
				return this.edges[i];
			}
		}

		public IEnumerable<Edge> Selectedges()
		{
			Edge[] list;
			lock (this.edgesSyncRoot)
			{
				list = this.edges.ToArray();
			}
			foreach(var l in list) yield return l;
		}

		protected readonly object linesSyncRoot = new object();
		public void Addlines(Line line)
		{
			lock (this.linesSyncRoot)
			{
				if (this.lines == null) this.lines = new List<Line>();
				this.lines.Add(line);
			}
		}

		internal void Removelines(Line line)
		{
			lock (this.linesSyncRoot)
			{
				this.lines.Remove(line);
				if (!this.lines.Any()) this.lines = null;
			}
		}

		public Line Getlines(int i)
		{
			lock (this.linesSyncRoot)
			{
				return this.lines[i];
			}
		}

		public IEnumerable<Line> Selectlines()
		{
			Line[] list;
			lock (this.linesSyncRoot)
			{
				list = this.lines.ToArray();
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
