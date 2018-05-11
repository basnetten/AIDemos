using System;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class SeekSteeringBehavior : ISteeringBehavior
	{
		public Vector2 Target { get; set; }

		public Vector2 GetDesiredVelocity(MovingEntity entity)
		{
			return GetDesiredVelocity(entity, Target);
		}

		public static Vector2 GetDesiredVelocity(MovingEntity entity, Vector2 target)
		{
			Vector2 targetOffset = target - entity.Position;
			Vector2 targetDirection = targetOffset.Normalize();
			Vector2 desiredVelocity = targetDirection * entity.MaxVelocity;
			Vector2 deltaVelocity = desiredVelocity - entity.Velocity;
			
			return deltaVelocity;
		}
	}
}