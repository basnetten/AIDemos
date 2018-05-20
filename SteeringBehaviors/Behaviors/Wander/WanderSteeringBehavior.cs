using System;
using GameMath.Vectors;

namespace SteeringBehaviors.Behaviors.Wander
{
	public class WanderSteeringBehavior : ISteeringBehavior
	{
		public WanderSteeringBehavior()
		{
			_random = new Random();
		}
		
		private Random _random;
		public SteeringData DataPrototype { get; } = new WanderSteeringData();

		public SteeringData CalculateData(MovingEntity entity)
		{
			Console.WriteLine(RandomClamped());
			
			// Randomize.
			WanderTarget += new Vector2(RandomClamped() * WanderJitter, RandomClamped() * WanderJitter);
			WanderTarget = WanderTarget.Normalize();

			WanderTarget *= WanderRadius;

			Vector2 targetLocal = WanderTarget + entity.Heading * WanderDistance;
//			targetLocal -= entity.Position;
			return new WanderSteeringData
			{
				DeltaVelocity = targetLocal,
				Entity = entity,

				WanderTarget = WanderTarget,
				WanderRadius = WanderRadius,
				WanderDistance = WanderDistance,
				WanderJitter = WanderJitter
			};
		}

		public Vector2 WanderTarget { get; set; }

		public double WanderRadius { get; set; }
		public double WanderDistance { get; set; }
		public double WanderJitter { get; set; }

		private double RandomClamped()
		{
			return -1 + _random.NextDouble() * 2d;
		}
	}
}