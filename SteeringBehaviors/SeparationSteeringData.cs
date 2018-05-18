using System.Collections.Generic;
using System.Drawing;
using GameMath.Vectors;
using SteeringBehaviors.Extensions;

namespace SteeringBehaviors
{
	public class SeparationSteeringData : SteeringData
	{
		private static LegendItem NeighborForcesLegendItem { get; } = new LegendItem(Color.Green, "Neighbor Forces");

		public List<Vector2> NeighborForces { get; set; }

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			foreach (Vector2 force in NeighborForces)
				g.DrawVector(force, Entity.Position, Color.Green);
		}

		public override void AddLegendItems(List<LegendItem> legend)
		{
			base.AddLegendItems(legend);
			legend.Add(NeighborForcesLegendItem);
		}

		public override void RemoveLegendItems(List<LegendItem> legend)
		{
			base.RemoveLegendItems(legend);
			legend.Remove(NeighborForcesLegendItem);
		}
	}
}