using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GameMath.Vectors;
using SteeringBehaviors;
using SteeringBehaviors.Behaviors;
using SteeringBehaviors.Behaviors.Allign;
using SteeringBehaviors.Behaviors.Seek;
using SteeringBehaviors.Behaviors.Wander;

namespace SteeringFlockDemo
{
	internal class Program
	{
		private const int NumberOfBoids = 1;

		public static void Main(string[] args)
		{
			var entities = new List<MovingEntity>();
			double step = 360d / NumberOfBoids / 2;
			var flock = new WanderSteeringBehavior
			{
				WanderTarget = new Vector2(-1,0),
				WanderDistance = 10,
				WanderJitter = 1,
				WanderRadius = 50,
			};
			
//			var flock = new CompositeSteeringBehavior(entities);
//			flock.Separation.Weight = 2d;
//			flock.Allign.Weight = 1d;
//			flock.Cohesion.Weight = 1d;
//			((SeekSteeringBehavior) flock.Seek.Behavior).Target = new Vector2(200, 100);

			for (int i = 0; i < NumberOfBoids; i++)
			{
				var currentAngle = i * step * (Math.PI / 180);
				var movingEntity = new MovingEntity
				{
					Position = new Vector2(Math.Cos(currentAngle), Math.Sin(currentAngle)) * 75,
					Velocity = new Vector2(Math.Cos(currentAngle), Math.Sin(currentAngle)) * 10,
					MaxVelocity = 100,
					Mass = 2,
				};
				
				entities.Add(movingEntity);
				if (i >= 0)
				{
					movingEntity.SteeringBehavior = flock;
				}
			}

			var form = new Form
			{
				Text = "Flocking Demo",
				ClientSize = new Size(500, 500)
			};

			Stopwatch stopwatch = null;
			long previousUpdate = 0;
			double secsPerUpdateTick = 0d;
			int updateTicksPerSec = 0;

			form.Paint += (sender, e) =>
			{
				Graphics g = e.Graphics;

				// TODO Make proper viewport transform.
				g.TranslateTransform(250, 250);

				if (stopwatch == null) stopwatch = Stopwatch.StartNew();

				long currentUpdate = stopwatch.ElapsedMilliseconds;
				double deltaTimeS = (currentUpdate - previousUpdate) / 1000d;

				if (deltaTimeS > secsPerUpdateTick)
				{
					foreach (MovingEntity entity in entities)
						entity.Update(deltaTimeS);

					previousUpdate = currentUpdate;
				}

				/**
				 * Draw calls.
				 */
				foreach (MovingEntity entity in entities)
					entity.Draw(g);

				form.Invalidate();
			};
			form.KeyDown += (s, e) =>
			{
				Console.WriteLine(e.KeyCode);
				if (e.KeyCode == Keys.Escape)
				{
					Environment.Exit(0);
				}
			};
			form.MouseClick += (s, e) =>
			{
				Vector2 clickPos = new Vector2(
					e.X - form.ClientSize.Width / 2d,
					-e.Y + form.ClientSize.Height / 2d);
				Console.WriteLine($"Click! {clickPos}");
			};

			EnableDoubleBuffer(form);
			Application.Run(form);
		}

		public static void EnableDoubleBuffer(Control c)
		{
			PropertyInfo aProp =
				typeof(Control).GetProperty(
					"DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
			aProp.SetValue(c, true, null);
		}
	}
}