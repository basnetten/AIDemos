namespace SteeringBehaviors.Behaviors
{
	public interface ISteeringBehavior
	{
		SteeringData DataPrototype { get; }
		
		SteeringData CalculateData(MovingEntity entity);
	}
}