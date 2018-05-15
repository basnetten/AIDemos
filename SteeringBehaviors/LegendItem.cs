using System;
using System.Drawing;

namespace SteeringBehaviors
{
	public class LegendItem
	{
		public LegendItem(Color vectorColor, string text)
		{
			VectorColor = vectorColor;
			Text = text;
		}

		public Color VectorColor { get; set; }
		public string Text { get; set; }
	}
}