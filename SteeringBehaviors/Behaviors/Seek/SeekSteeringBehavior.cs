﻿using GameMath.Vectors;

namespace SteeringBehaviors.Behaviors.Seek
{
	public class SeekSteeringBehavior : ISteeringBehavior
	{
		public Vector2 Target { get; set; }

		public SeekSteeringBehavior()
		{
			Target = new Vector2();
		}

		public SteeringData DataPrototype => new SeekSteeringData();

		public SteeringData CalculateData(MovingEntity entity)
		{
			Vector2 targetOffset = Target - entity.Position;
			Vector2 targetDirection = targetOffset.Normalize();
			Vector2 desiredVelocity = targetDirection * entity.MaxVelocity;
			Vector2 deltaVelocity = desiredVelocity - entity.Velocity;
			
			return new SeekSteeringData
			{
				DeltaVelocity = deltaVelocity,
				Entity = entity,
				// Seek Specific.
				Target = Target,
				TargetOffset = targetOffset,
				TargetDirection = targetDirection,
				DesiredVelocity = desiredVelocity
			};
		}
	}
}