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
	/// Base class of any entity that can move
	/// 
	/// </summary>
	public partial class MobileEntity : Entity
	{
		
		public double Heading;
		
		public double Speed;
		
		public MovementType MovementType;

		public MobileEntity(int id, int width, int height, string name) : base(id, width, height, name)
		{
			this.Heading = Math.PI;
			OnMobileEntityInit();
		}

		partial void OnMobileEntityInit();

		#region Events

		public override void Tick(TickEventParameters e)
		{
			base.Tick(e);
			var p = new TickEventParameters(e);
			MoveMessage TickMoveMessage;
			if(MoveMessages.TryPop(out TickMoveMessage))
				OnProcessTickMoveMessage(TickMoveMessage);
			TurnMessage TickTurnMessage;
			if(TurnMessages.TryPop(out TickTurnMessage))
				OnProcessTickTurnMessage(TickTurnMessage);
			OnTick(p);
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

		partial void OnTick(TickEventParameters e);
		partial void OnProcessTickMoveMessage(MoveMessage TickMoveMessage);
		partial void OnProcessTickTurnMessage(TurnMessage TickTurnMessage);
		#endregion

		#region Messages

		protected ConcurrentQueue<TurnMessage> TurnMessages = new ConcurrentQueue<TurnMessage>();
		public void RegisterTurnMessage(TurnMessage message)
		{
			TurnMessages.Push(message);
		}
		protected ConcurrentQueue<MoveMessage> MoveMessages = new ConcurrentQueue<MoveMessage>();
		public void RegisterMoveMessage(MoveMessage message)
		{
			MoveMessages.Push(message);
		}
		#endregion
	}
}
