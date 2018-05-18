using System.Drawing;
using System.Drawing.Drawing2D;
using GameMath.Vectors;

namespace SteeringBehaviors.Extensions
{
	public static class GraphicsExtensions
	{
		public static void DrawVector(this Graphics g, Vector2 vector, Vector2 origin, Color color)
		{
			Pen pen = new Pen(color) {CustomEndCap = new AdjustableArrowCap(4, 4)};

			g.DrawLine(pen, (Point) origin.Scale(1, -1), (Point) (origin + vector).Scale(1, -1));
		}
	}
}