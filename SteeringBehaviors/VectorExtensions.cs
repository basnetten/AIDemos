using System.Drawing;
using System.Drawing.Drawing2D;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public static class VectorExtensions
	{
		public static void DrawVector(this Vector2 vector, Graphics g, Color color, Vector2 origin)
		{
			Pen pen = new Pen(color);
			pen.CustomEndCap = new AdjustableArrowCap(4, 4);

			g.DrawLine(pen, (Point) origin.Scale(1, -1), (Point) (origin + vector).Scale(1, -1));
		}
	}
}