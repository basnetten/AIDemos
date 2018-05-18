using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GameMath.Vectors;
using SteeringBehaviors;

namespace SteeringFlockDemo
{
	internal class Program
	{
		private const int NumberOfBoids = 10;

		public static void Main(string[] args)
		{
			var entities = new List<MovingEntity>();
			for (int i = 0; i < NumberOfBoids; i++)
				entities.Add(new MovingEntity
				{
					// SteeringBehavior = new SeekSteeringBehavior(),
					Position = new Vector2(),
					Velocity = new Vector2(),
					MaxVelocity = 100,
					Mass = 2,
				});

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