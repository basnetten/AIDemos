using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GameMath.Vectors;
using SteeringBehaviors;
using SteeringBehaviors.Behaviors.Seek;

namespace SteeringSeekDemo
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			SeekSteeringBehavior seekSteeringBehavior = new SeekSteeringBehavior();
			MovingEntity movingEntity = new MovingEntity
			{
				SteeringBehavior = seekSteeringBehavior,
				Position = new Vector2(),
				Velocity = new Vector2(),
				MaxVelocity = 100,
				Mass = 2,
			};

			Form form = new Form();
			form.Text = "Seek Demo";
			form.ClientSize = new Size(500, 500);

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
					movingEntity.Update(deltaTimeS);

					previousUpdate = currentUpdate;
				}

				/**
				 * Draw calls.
				 */
				movingEntity.Draw(g);

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

				if (e.Button == MouseButtons.Left)
				{
					seekSteeringBehavior.Target = clickPos;
				}
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