using System.Drawing;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public interface ISteeringBehavior
	{
		SteeringData CalculateData(MovingEntity entity);
	}
}