using System.Drawing;
using System.Drawing.Drawing2D;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class SteeringData
	{
		public Vector2 DeltaVelocity { get; set; }
		public MovingEntity Entity { get; set; }

		public virtual void Draw(Graphics g)
		{
			DrawDeltaVelocity(g);
		}

		private void DrawDeltaVelocity(Graphics g)
		{
			Pen deltaVelocityPen = new Pen(Color.Aqua);
			deltaVelocityPen.CustomEndCap = new AdjustableArrowCap(4, 4);
			g.DrawLine(deltaVelocityPen,
				(Point) Entity.Position.Scale(1, -1),
				(Point) (Entity.Position + DeltaVelocity).Scale(1, -1));
		}
	}
}