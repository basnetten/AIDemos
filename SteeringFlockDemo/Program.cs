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
using SteeringBehaviors.Behaviors.Cohesion;
using SteeringBehaviors.Behaviors.Seek;
using SteeringBehaviors.Behaviors.Wander;

namespace SteeringFlockDemo
{
	internal class Program
	{
		private const int NumberOfBoids = 100;

		public static void Main(string[] args)
		{
			var    entities = new List<MovingEntity>();
			double step     = 360d / NumberOfBoids / 2;

			var flock = new CompositeSteeringBehavior(entities);
			flock.Separation.Weight = 4d;
			flock.Allign.Weight     = 2d;
			flock.Cohesion.Weight   = 0.5d;
			flock.Wander.Weight     = 4d;

			for (int i = 0; i < NumberOfBoids; i++)
			{
				var currentAngle = i * step * (Math.PI / 180);
				var movingEntity = new MovingEntity
				{
					Position    = new Vector2(Math.Cos(currentAngle), Math.Sin(currentAngle)) * 75,
					Velocity    = new Vector2(Math.Cos(currentAngle), Math.Sin(currentAngle)) * 10,
					MaxVelocity = 100,
					MaxForce    = 50,
					Mass        = 1,
				};

				entities.Add(movingEntity);
				if (i >= 0)
				{
					movingEntity.SteeringBehavior = flock;
				}
			}

			var form = new Form
			{
				Text       = "Flocking Demo",
				ClientSize = new Size(700, 500)
			};

			Stopwatch stopwatch         = null;
			long      previousUpdate    = 0;
			double    secsPerUpdateTick = 0d;
			int       updateTicksPerSec = 0;

			TrackBar trackBarAllign = new TrackBar
			{
				Minimum       = 0,
				Maximum       = 1000,
				Location      = new Point(500, 10),
				TickFrequency = 100,
				Value         = (int) (flock.Allign.Weight * 100)
			};
			trackBarAllign.ValueChanged += (s, e) => flock.Allign.Weight = trackBarAllign.Value / 100d;
			form.Controls.Add(trackBarAllign);

			TrackBar trackBarCohesion = new TrackBar
			{
				Minimum       = 0,
				Maximum       = 1000,
				Location      = new Point(500, 50),
				TickFrequency = 100,
				Value         = (int) (flock.Cohesion.Weight * 100)
			};
			trackBarCohesion.ValueChanged += (s, e) => flock.Cohesion.Weight = trackBarCohesion.Value / 100d;
			form.Controls.Add(trackBarCohesion);

			TrackBar trackBarSeparation = new TrackBar
			{
				Minimum       = 0,
				Maximum       = 1000,
				Location      = new Point(500, 90),
				TickFrequency = 100,
				Value         = (int) (flock.Separation.Weight * 100)
			};
			trackBarSeparation.ValueChanged += (s, e) => flock.Separation.Weight = trackBarSeparation.Value / 100d;
			form.Controls.Add(trackBarSeparation);

			TrackBar trackBarWander = new TrackBar
			{
				Minimum       = 0,
				Maximum       = 1000,
				Location      = new Point(500, 130),
				TickFrequency = 100,
				Value         = (int) (flock.Wander.Weight * 100)
			};
			trackBarWander.ValueChanged += (s, e) => flock.Wander.Weight = trackBarWander.Value / 100d;
			form.Controls.Add(trackBarWander);

			Label labelAllign = new Label { Location = new Point(620, 10) };
			form.Controls.Add(labelAllign);
			Label labelCohesion = new Label { Location = new Point(620, 50) };
			form.Controls.Add(labelCohesion);
			Label labelSeparation = new Label { Location = new Point(620, 90) };
			form.Controls.Add(labelSeparation);
			Label labelWander = new Label { Location = new Point(620, 130) };
			form.Controls.Add(labelWander);

			form.Paint += (sender, e) =>
			{
				Graphics g = e.Graphics;

				labelAllign.Text     = CompositeSteeringBehavior.AllignCount.ToString();
				labelCohesion.Text   = CompositeSteeringBehavior.CohesionCount.ToString();
				labelSeparation.Text = CompositeSteeringBehavior.SeparationCount.ToString();
				labelWander.Text     = CompositeSteeringBehavior.WanderCount.ToString();

				// TODO Make proper viewport transform.
				g.TranslateTransform(250, 250);

				if (stopwatch == null) stopwatch = Stopwatch.StartNew();

				long   currentUpdate = stopwatch.ElapsedMilliseconds;
				double deltaTimeS    = (currentUpdate - previousUpdate) / 1000d;

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
					e.X  - form.ClientSize.Width  / 2d,
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