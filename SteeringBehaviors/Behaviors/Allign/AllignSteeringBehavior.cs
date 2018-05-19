using System.Collections.Generic;
using GameMath.Vectors;

namespace SteeringBehaviors.Behaviors.Allign
{
	public class AllignSteeringBehavior : ISteeringBehavior
	{
		public SteeringData DataPrototype { get; } = new AllignSteeringData();

		public SteeringData CalculateData(MovingEntity entity)
		{
			var averageHeading = new Vector2();
			int handledNeighborCount = 0;

			foreach (MovingEntity neighbor in Neighbors)
			{
				if (neighbor == entity) continue;
				averageHeading += neighbor.Heading;
				handledNeighborCount++;
			}

			if (handledNeighborCount > 0)
			{
				averageHeading /= handledNeighborCount;
				averageHeading -= entity.Heading;
			}

			return new AllignSteeringData
			{
				DeltaVelocity = averageHeading,
				Entity = entity,
				AverageHeading = averageHeading
			};
		}

		public List<MovingEntity> Neighbors { get; set; }
	}
}