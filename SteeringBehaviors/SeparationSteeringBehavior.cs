using System.Collections.Generic;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class SeparationSteeringBehavior : ISteeringBehavior
	{
		public SeparationSteeringBehavior()
		{
			Neighbors = new List<MovingEntity>();
		}

		public SteeringData DataPrototype { get; } = new SeparationSteeringData();

		public SteeringData CalculateData(MovingEntity entity)
		{
			var force = new Vector2();
			var position = entity.Position;
			var forces = new List<Vector2>();

			foreach (MovingEntity neighbor in Neighbors)
			{
				if (neighbor == entity) continue;

				Vector2 toNeighbor = (position - neighbor.Position);
				double distToNeighbor = toNeighbor.MagnitudePythagorean();
				
				if (distToNeighbor > 100) continue;
				// Prevent accidental /0 errors.
				if (distToNeighbor == 0) continue;
				Vector2 neighborForce = toNeighbor.Normalize() / distToNeighbor;
				forces.Add(neighborForce);
				force += neighborForce;
			}

			force *= 10;

			return new SeparationSteeringData
			{
				DeltaVelocity = force,
				Entity = entity,
				// Separation Specific.
				NeighborForces = forces,
			};
		}

		public List<MovingEntity> Neighbors { get; set; }
	}
}