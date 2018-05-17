using System;
using System.Drawing;

namespace SteeringBehaviors
{
	/// <summary>
	/// POD class for legend items in the HUD.
	/// </summary>
	public class LegendItem
	{
		/// <summary>
		/// Initialize a legend item with the specified color and info text.
		/// </summary>
		public LegendItem(Color vectorColor, string text)
		{
			VectorColor = vectorColor;
			Text = text;
		}

		/// <summary>
		/// The color that this legend item will be drawn in.
		/// </summary>
		public Color VectorColor { get; set; }
		
		/// <summary>
		/// The informational text to be displayed next to the legend item.
		/// </summary>
		public string Text { get; set; }
	}
}