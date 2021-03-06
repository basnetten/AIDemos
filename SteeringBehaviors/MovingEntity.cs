﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameMath.Matrices;
using GameMath.Vectors;
using SteeringBehaviors.Behaviors;
using SteeringBehaviors.Extensions;

namespace SteeringBehaviors
{
	public class MovingEntity
	{
		private static LegendItem        VelocityLegendItem { get; } = new LegendItem(Color.Black, "Velocity");
		private        ISteeringBehavior _steeringBehavior;

		public Vector2 Position { get; set; }

		// Y axis flipped for rendering.
		public Vector2 RenderPosition => Position.Scale(1, -1);
		public double  MaxVelocity    { get; set; }
		public double  MaxForce       { get; set; }
		public Vector2 Velocity       { get; set; }

		public double Mass { get; set; }

		public Vector2 Heading => Velocity.Normalize();

		public ISteeringBehavior SteeringBehavior
		{
			get => _steeringBehavior;
			set
			{
				_steeringBehavior?.DataPrototype.RemoveLegendItems(Legend);
				_steeringBehavior = value;
				_steeringBehavior?.DataPrototype.AddLegendItems(Legend);
			}
		}

		public SteeringData LastSteeringData { get; set; }

		public List<Vector2> Lines { get; }

		public List<LegendItem> Legend { get; }

		public MovingEntity()
		{
			Lines = new List<Vector2>();

			// Initlialize as triangle.
			Lines.Add(new Vector2(10,  0));
			Lines.Add(new Vector2(-5, -5));
			Lines.Add(new Vector2(-5, 5));
			Lines.Add(new Vector2(10,  0));

			Legend = new List<LegendItem>();
			AddLegendItems(Legend);
		}

		public void Update(double deltaTimeS)
		{
			var calculateData = SteeringBehavior?.CalculateData(this, deltaTimeS);
			LastSteeringData = calculateData;
			Vector2 a = (calculateData?.DeltaVelocity ?? new Vector2()).Truncate(MaxForce);
			a        /= Mass;
			Velocity += a * deltaTimeS;
			Velocity =  Velocity.Truncate(MaxVelocity);
			Position += Velocity * deltaTimeS;

			if (Position.X <= -250 || 250 <= Position.X) Position = Position.Scale(-1, 1);
			if (Position.Y <= -250 || 250 <= Position.Y) Position = Position.Scale(1,  -1);
		}

		public void Draw(Graphics g)
		{
			// Allow the steering to draw its debug info.
			// LastSteeringData?.Draw(g);

			// Calculate the heading of the entity.
			double th = Math.Atan2(0, 1) - Math.Atan2(Heading.Y, Heading.X);

			// Rotate and transform all the points.
			Matrix3     rotMat      = MatrixBuilder.RotationMatrix(th);
			List<Point> transformed = new List<Point>();
			foreach (var vector2 in Lines)
			{
				Vector3 vector3 = new Vector3(vector2, 1d);
				Vector2 rotated = new Vector2(vector3 * rotMat);
				rotated = rotated + RenderPosition;
				transformed.Add((Point) rotated);
			}

			// Draw the velocity vector.
			// Velocity.Draw(g, Position, Color.Black);

			// Draw the lines of the entity.
			g.DrawLines(Pens.Black, transformed.ToArray());

			// Draw the legend.
			// DrawLegend(g);
		}

		public void AddLegendItems(List<LegendItem>    legend) => legend.Add(VelocityLegendItem);
		public void RemoveLegendItems(List<LegendItem> legend) => legend.Remove(VelocityLegendItem);

		private void DrawLegend(Graphics g)
		{
			Font      font = new Font("arial", 8);
			int       yOff = 250 - 10;
			const int xOff = 10 + -250;
			foreach (var legendItem in Legend)
			{
				Size textSize = TextRenderer.MeasureText(legendItem.Text, font);
				g.DrawVector(new Vector2(20, 0), new Vector2(xOff, yOff), legendItem.VectorColor);

				g.DrawString(legendItem.Text,
					font,
					Brushes.Black,
					xOff + 20 + 5,
					-yOff     - textSize.Height / 2);
				yOff -= textSize.Height + 5;
			}
		}
	}
}