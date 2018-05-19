using System.Drawing;
using GameMath.Vectors;
using SteeringBehaviors.Behaviors.Seek;

namespace SteeringBehaviors.Behaviors.Cohesion
{
	public class CohesionSteeringData : SeekSteeringData
	{	
		public Vector2 CenterOfMass { get; set; }

		public override void Draw(Graphics g)
		{
			base.Draw(g);

			g.FillEllipse(Brushes.Red, new Rectangle((Point) (CenterOfMass + new Vector2(-6, -6)), new Size(11, 10)));
		}
	}
}