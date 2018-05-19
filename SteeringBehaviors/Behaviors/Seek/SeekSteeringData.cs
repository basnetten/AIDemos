using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using GameMath.Vectors;
using SteeringBehaviors.Extensions;

namespace SteeringBehaviors.Behaviors.Seek
{
	/// <summary>
	/// A class to hold the data from the seek steering behavior. It also contains all the debug drawing code for the
	/// steering behavior.
	/// </summary>
	public class SeekSteeringData : SteeringData
	{
		private static LegendItem DesiredVelocityLegendItem { get; } = new LegendItem(Color.Green, "Desired Velocity");

		public Vector2 Target { get; set; }
		public Vector2 TargetOffset { get; set; }
		public Vector2 TargetDirection { get; set; }
		public Vector2 DesiredVelocity { get; set; }

		public override void Draw(Graphics g)
		{
			base.Draw(g);

			DrawTarget(g, Target.Scale(1, -1));
			DrawTargetOffset(g);
			DrawDesiredVelocity(g);
		}

		public override void AddLegendItems(List<LegendItem> legend)
		{
			base.AddLegendItems(legend);
			legend.Add(DesiredVelocityLegendItem);
		}

		public override void RemoveLegendItems(List<LegendItem> legend)
		{
			base.RemoveLegendItems(legend);
			legend.Remove(DesiredVelocityLegendItem);
		}

		private void DrawDesiredVelocity(Graphics g) => g.DrawVector(DesiredVelocity, Entity.Position, Color.Green);

		private void DrawTargetOffset(Graphics g)
		{
			Pen targetOffsetPen = new Pen(Color.Black);
			targetOffsetPen.DashStyle = DashStyle.Dash;
			g.DrawLine(targetOffsetPen,
				(Point) Entity.Position.Scale(1, -1),
				(Point) (Entity.Position + TargetOffset).Scale(1, -1));
		}

		private static void DrawTarget(Graphics g, Vector2 position)
		{
			int circleRadius = 5;
			Vector2 circleOrigin = position + new Vector2(-5, -5);
			g.DrawEllipse(
				Pens.Red,
				new Rectangle((Point) circleOrigin, new Size(circleRadius * 2, circleRadius * 2)));

			int lineHalfLength = 8;
			Vector2 lineVerticalOrigin = position + new Vector2(0, -lineHalfLength);
			Vector2 lineHorizontalOrigin = position + new Vector2(-lineHalfLength, 0);
			g.DrawLine(Pens.Black,
				(Point) lineVerticalOrigin,
				(Point) (lineVerticalOrigin + new Vector2(0, lineHalfLength * 2)));
			g.DrawLine(Pens.Black,
				(Point) lineHorizontalOrigin,
				(Point) (lineHorizontalOrigin + new Vector2(lineHalfLength * 2, 0)));
		}
	}
}