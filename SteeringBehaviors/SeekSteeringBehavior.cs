using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class SeekSteeringBehavior : ISteeringBehavior
	{
		public Vector2 Target { get; set; }

		public SeekSteeringBehavior()
		{
			Target = new Vector2();
		}

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