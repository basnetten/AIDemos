using System.Drawing;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public interface ISteeringBehavior
	{
		SteeringData DataPrototype { get; }
		
		SteeringData CalculateData(MovingEntity entity);
	}
}