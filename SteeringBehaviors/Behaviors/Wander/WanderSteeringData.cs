using System.Drawing;
using GameMath.Vectors;
using SteeringBehaviors.Extensions;

namespace SteeringBehaviors.Behaviors.Wander
{
	public class WanderSteeringData : SteeringData
	{
		public Vector2 WanderTarget { get; set; }
		public double WanderRadius { get; set; }
		public double WanderDistance { get; set; }
		public double WanderJitter { get; set; }

		public override void Draw(Graphics g)
		{
			base.Draw(g);

			g.DrawVector(WanderTarget, Entity.Position, Color.Green);
			g.DrawEllipse(Pens.Red,
				new Rectangle((Point) (Entity.Position.Scale(1, -1) + new Vector2(-WanderRadius, -WanderRadius)),
					new Size((int) (WanderRadius * 2), (int) (WanderRadius * 2))));
			
			g.DrawEllipse(Pens.Purple,
				new Rectangle((Point) ((Entity.Position + Entity.Heading * WanderDistance).Scale(1, -1) + new Vector2(-WanderRadius, -WanderRadius)),
					new Size((int) (WanderRadius * 2), (int) (WanderRadius * 2))));
		}
	}
}