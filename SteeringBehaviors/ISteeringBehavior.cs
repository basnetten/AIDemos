using GameMath.Vectors;

namespace SteeringBehaviors
{
	public interface ISteeringBehavior
	{
		Vector2 GetDesiredVelocity(MovingEntity entity);
	}
}