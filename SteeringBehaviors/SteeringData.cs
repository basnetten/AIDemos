using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	public class SteeringData
	{
		public SteeringData()
		{
			LegendItems = new List<LegendItem>
				{new LegendItem(Color.Aqua, "Delta Velocity")};
		}

		public Vector2 DeltaVelocity { get; set; }
		public MovingEntity Entity { get; set; }

		public List<LegendItem> LegendItems { get; }

		public virtual void Draw(Graphics g)
		{
			DrawDeltaVelocity(g);

			DrawLegend(g);
		}

		protected void DrawVector(Graphics g, Color color, Vector2 origin, Vector2 vector)
		{
			Pen pen = new Pen(color);
			pen.CustomEndCap = new AdjustableArrowCap(4, 4);

			g.DrawLine(pen, (Point) origin.Scale(1, -1), (Point) (origin + vector).Scale(1, -1));
		}

		private void DrawDeltaVelocity(Graphics g) => DrawVector(g, Color.Aqua, Entity.Position, DeltaVelocity);

		private void DrawLegend(Graphics g)
		{
			Font font = new Font("arial", 8);
			int yOff = 250 - 10;
			const int xOff = 10 + -250;
			foreach (var legendItem in LegendItems)
			{
				Size textSize = TextRenderer.MeasureText(legendItem.Text, font);
				DrawVector(g, legendItem.VectorColor, new Vector2(xOff, yOff), new Vector2(20, 0));

				g.DrawString(legendItem.Text,
					font,
					Brushes.Black,
					xOff + 20 + 5,
					-yOff - textSize.Height / 2);
				yOff -= textSize.Height + 5;
			}
		}
	}
}