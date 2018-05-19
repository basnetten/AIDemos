using System.Collections.Generic;
using System.Drawing;
using GameMath.Vectors;
using SteeringBehaviors.Extensions;

namespace SteeringBehaviors.Behaviors.Allign
{
	public class AllignSteeringData : SteeringData
	{
		private static LegendItem AverageHeadingLegendItem { get; } = new LegendItem(Color.Brown, "Average Heading");
		
		public Vector2 AverageHeading { get; set; }
		
		public override void Draw(Graphics g)
		{
			base.Draw(g);

			g.DrawVector(AverageHeading * 10, Entity.Position, Color.Brown);
		}

		public override void AddLegendItems(List<LegendItem> legend)
		{
			base.AddLegendItems(legend);
			legend.Add(AverageHeadingLegendItem);
		}

		public override void RemoveLegendItems(List<LegendItem> legend)
		{
			base.RemoveLegendItems(legend);
			legend.Remove(AverageHeadingLegendItem);
		}
	}
}