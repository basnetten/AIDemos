using System.Collections.Generic;
using GameMath.Vectors;
using SteeringBehaviors.Behaviors.Seek;

namespace SteeringBehaviors.Behaviors.Cohesion
{
	public class CohesionSteeringBehavior : ISteeringBehavior
	{
		public SteeringData DataPrototype { get; } = new CohesionSteeringData();

		public SteeringData CalculateData(MovingEntity entity)
		{
			var centerOfMass = new Vector2();

			int handledNeighborCount = 0;

			foreach (MovingEntity neighbor in Neighbors)
			{
				if (neighbor == entity) continue;
				centerOfMass += neighbor.Position;
				handledNeighborCount++;
			}

			if (handledNeighborCount > 0)
			{
				centerOfMass /= handledNeighborCount;
			}

			CohesionSteeringData data = (CohesionSteeringData) new SeekSteeringBehavior {Target = centerOfMass}
				.CalculateData(entity, new CohesionSteeringData());

			data.CenterOfMass = centerOfMass;
			
			return data;
		}

		public List<MovingEntity> Neighbors { get; set; }
	}
}