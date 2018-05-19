using GameMath.Vectors;

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
			return CalculateData(entity, new SeekSteeringData());
		}

		public SteeringData CalculateData(MovingEntity entity, SeekSteeringData data)
		{
			Vector2 targetOffset = Target - entity.Position;
			Vector2 targetDirection = targetOffset.Normalize();
			Vector2 desiredVelocity = targetDirection * entity.MaxVelocity;
			Vector2 deltaVelocity = desiredVelocity - entity.Velocity;
			
			data.DeltaVelocity = deltaVelocity;
			data.Entity = entity;
			data.Target = Target;
			data.TargetOffset = targetOffset;
			data.TargetDirection = targetDirection;
			data.DesiredVelocity = desiredVelocity;
			
			return data;
		}
	}
}