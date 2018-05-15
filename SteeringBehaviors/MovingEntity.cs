using System;
using System.Collections.Generic;
using System.Drawing;
using GameMath.Matrices;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class MovingEntity
	{
		public Vector2 Position { get; set; }
		// Y axis flipped for rendering.
		public Vector2 RenderPosition => Position.Scale(1, -1);
		public double MaxVelocity { get; set; }
		public Vector2 Velocity { get; set; }

		public double Mass { get; set; }
		
		public Vector2 Heading => Velocity.Normalize();

		public ISteeringBehavior SteeringBehavior { get; set; }
		public SteeringData LastSteeringData { get; set; }
		
		public List<Vector2> Lines { get; }

		public MovingEntity()
		{
			Lines = new List<Vector2>();

			// Initlialize as triangle.
			Lines.Add(new Vector2(50, 0));
			Lines.Add(new Vector2(-25, -25));
			Lines.Add(new Vector2(-25, 25));
			Lines.Add(new Vector2(50, 0));
		}

		public void Update(double deltaTimeS)
		{
			var calculateData = SteeringBehavior?.CalculateData(this);
			LastSteeringData = calculateData;
			Vector2 a = calculateData?.DeltaVelocity ?? new Vector2();
			a /= Mass;
			Velocity += a * deltaTimeS;
			Position += Velocity * deltaTimeS;
		}

		public void Draw(Graphics g)
		{
			// Allow the steering to draw its debug info.
			LastSteeringData?.Draw(g);
			
			// Calculate the heading of the entity.
			double th = Math.Atan2(0, 1) - Math.Atan2(Heading.Y, Heading.X);

			// Rotate and transform all the points.
			Matrix3 rotMat = MatrixBuilder.RotationMatrix(th);
			List<Point> transformed = new List<Point>();
			foreach (var vector2 in Lines)
			{
				Vector3 vector3 = new Vector3(vector2, 1d);
				Vector2 rotated = new Vector2(vector3 * rotMat);
				rotated = rotated + RenderPosition;
				transformed.Add((Point) rotated);
			}

			// Draw the velocity vector.
			Velocity.Draw(g, Position, Color.Black);

			// Draw the lines of the entity.
			g.DrawLines(Pens.Black, transformed.ToArray());
		}
	}
}