using System;
using System.Collections.Generic;
using GameMath.Vectors;
using SteeringBehaviors.Behaviors.Allign;
using SteeringBehaviors.Behaviors.Cohesion;
using SteeringBehaviors.Behaviors.Seek;
using SteeringBehaviors.Behaviors.Separation;

namespace SteeringBehaviors.Behaviors
{
	public class CompositeSteeringBehavior : ISteeringBehavior
	{
		public CompositeSteeringBehavior(List<MovingEntity> neighbors)
		{
			Neighbors = neighbors;

			Separation = new X(new SeparationSteeringBehavior {Neighbors = Neighbors});
			Cohesion = new X(new CohesionSteeringBehavior {Neighbors = Neighbors});
			Allign = new X(new AllignSteeringBehavior {Neighbors = Neighbors});
			Seek = new X(new SeekSteeringBehavior {Target = new Vector2()});
		}

		public SteeringData DataPrototype { get; } = new SteeringData();

		public SteeringData CalculateData(MovingEntity entity)
		{
			Vector2 total = new Vector2();

			if (Allign.Enabled && total.MagnitudePythagorean() < entity.MaxVelocity)
			{
				Console.WriteLine("Allign");
				total += Allign.Behavior.CalculateData(entity).DeltaVelocity * Allign.Weight;
			}

			if (Separation.Enabled && total.MagnitudePythagorean() < entity.MaxVelocity)
			{
				Console.WriteLine("Separation");
				total += Separation.Behavior.CalculateData(entity).DeltaVelocity * Separation.Weight;
			}

			if (Cohesion.Enabled && total.MagnitudePythagorean() < entity.MaxVelocity)
			{
				Console.WriteLine("Cohesion");
				total += Cohesion.Behavior.CalculateData(entity).DeltaVelocity * Cohesion.Weight;
			}

			if (Seek.Enabled && total.MagnitudePythagorean() < entity.MaxVelocity)
			{
				Console.WriteLine("Seek");
				total += Seek.Behavior.CalculateData(entity).DeltaVelocity * Seek.Weight;
			}

			return new SteeringData
			{
				DeltaVelocity = total,
				Entity = entity,
			};
		}

		public X Allign { get; }
		public X Cohesion { get; }
		public X Separation { get; }
		public X Seek { get; }

		public List<MovingEntity> Neighbors { get; }

		public class X
		{
			public X(ISteeringBehavior behavior)
			{
				Enabled = true;
				Weight = 1d;
				Behavior = behavior;
			}

			public bool Enabled { get; set; }
			public double Weight { get; set; }
			public ISteeringBehavior Behavior { get; set; }
		}
	}
}