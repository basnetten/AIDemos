using System;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class MovingEntity
	{
		public Vector2 Position { get; set; }
		public double MaxVelocity { get; set; }
		public Vector2 Velocity { get; set; }

		public ISteeringBehavior SteeringBehavior { get; set; }

		public void Update(double deltaTimeS)
		{
			Vector2 a = SteeringBehavior?.GetDesiredVelocity(this) ?? new Vector2();
			a /= 2;
			Velocity += a * deltaTimeS;
			Position += Velocity * deltaTimeS;
			Console.WriteLine(Velocity.MagnitudePythagorean());
		}
	}
}