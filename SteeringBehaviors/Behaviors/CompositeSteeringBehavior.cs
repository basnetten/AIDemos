using System.Collections.Generic;
using GameMath.Vectors;
using SteeringBehaviors.Behaviors.Allign;
using SteeringBehaviors.Behaviors.Cohesion;
using SteeringBehaviors.Behaviors.Separation;
using SteeringBehaviors.Behaviors.Wander;

namespace SteeringBehaviors.Behaviors
{
	public class CompositeSteeringBehavior : ISteeringBehavior
	{
		
		public static int AllignCount     = 0;
		public static int CohesionCount   = 0;
		public static int SeparationCount = 0;
		public static int WanderCount     = 0;

		
		public CompositeSteeringBehavior(List<MovingEntity> neighbors)
		{
			Neighbors = neighbors;

			Separation = new X(new SeparationSteeringBehavior {Neighbors = Neighbors});
			Cohesion   = new X(new CohesionSteeringBehavior {Neighbors   = Neighbors});
			Allign     = new X(new AllignSteeringBehavior {Neighbors     = Neighbors});
			Wander = new X(new WanderSteeringBehavior
			{
				WanderTarget = new Vector2(),
				WanderDistance = 50,
				WanderJitter   = 2000,
				WanderRadius   = 25,
			});
		}

		public SteeringData DataPrototype { get; } = new SteeringData();

		public SteeringData CalculateData(MovingEntity entity, double deltaTimeS)
		{
			Vector2 total = new Vector2();

			if (Allign.Enabled && total.MagnitudePythagorean() < entity.MaxForce)
			{
				AllignCount++;
//				Console.WriteLine("Allign");
				total += Allign.Behavior.CalculateData(entity, deltaTimeS).DeltaVelocity * Allign.Weight;
			}

			if (Separation.Enabled && total.MagnitudePythagorean() < entity.MaxForce)
			{
				SeparationCount++;
//				Console.WriteLine("Separation");
				total += Separation.Behavior.CalculateData(entity, deltaTimeS).DeltaVelocity * Separation.Weight;
			}

			if (Cohesion.Enabled && total.MagnitudePythagorean() < entity.MaxForce)
			{
				CohesionCount++;
//				Console.WriteLine("Cohesion");
				total += Cohesion.Behavior.CalculateData(entity, deltaTimeS).DeltaVelocity * Cohesion.Weight;
			}

			if (Wander.Enabled && total.MagnitudePythagorean() < entity.MaxForce)
			{
				WanderCount++;
//				Console.WriteLine("Seek");
				total += Wander.Behavior.CalculateData(entity, deltaTimeS).DeltaVelocity * Wander.Weight;
			}

			return new SteeringData
			{
				DeltaVelocity = total,
				Entity        = entity,
			};
		}

		public X Allign     { get; }
		public X Cohesion   { get; }
		public X Separation { get; }
		public X Wander     { get; }

		public List<MovingEntity> Neighbors { get; }

		public class X
		{
			public X(ISteeringBehavior behavior)
			{
				Enabled  = true;
				Weight   = 1d;
				Behavior = behavior;
			}

			public bool              Enabled  { get; set; }
			public double            Weight   { get; set; }
			public ISteeringBehavior Behavior { get; set; }
		}
	}
}