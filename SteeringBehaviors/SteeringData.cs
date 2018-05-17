using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GameMath.Vectors;

namespace SteeringBehaviors
{
	/// <summary>
	/// A class to hold data that steering behaviors calculate. It contains the functionality to render the debug
	/// information as well.
	/// </summary>
	public class SteeringData
	{
		/// <summary>
		/// The LegendItem for the DeltaVelocityVector.
		/// </summary>
		private static LegendItem DeltaVelocityLegendItem { get; } = new LegendItem(Color.Aqua, "Delta Velocity");

		/// <summary>
		/// The change in velocity requested.
		/// </summary>
		public Vector2 DeltaVelocity { get; set; }
		
		/// <summary>
		/// The entity that this SteeringData was calculated for.
		/// </summary>
		public MovingEntity Entity { get; set; }

		/// <summary>
		/// Draws the debug information of this SteeringData object.
		/// </summary>
		public virtual void Draw(Graphics g)
		{
			DeltaVelocity.DrawVector(g, Color.Aqua, Entity.Position);
		}

		/// <summary>
		/// Add information about the debug drawing of this Steering data to the legend.
		/// </summary>
		public virtual void AddLegendItems(List<LegendItem> legend) => legend.Add(DeltaVelocityLegendItem);
		
		/// <summary>
		/// Remove information about the debug drawing of this Steering data from the legend.
		/// </summary>
		public virtual void RemoveLegendItems(List<LegendItem> legend) => legend.Remove(DeltaVelocityLegendItem);
	}
}