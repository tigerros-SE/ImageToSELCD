using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToSELCD.Extensions {
	public static class ColorX {
		const double BIT_SPACING = 255.0 / 7.0;

		public static char ToSEChar(this Color color) => ColorToSEChar(color.R, color.G, color.B);
		public static char ColorToSEChar(byte r, byte g, byte b)
			=> (char)(0xe100 + ((int)Math.Round(r / BIT_SPACING) << 6) + ((int)Math.Round(g / BIT_SPACING) << 3) + (int)Math.Round(b / BIT_SPACING));

		public static Color Quantize(this Color color, int factor) {
			int round(double n) {
				int floored = (int)n;
				double dec = n - floored;

				return dec >= 0.5 ? floored + 1 : floored;
			}
			
			return Color.FromArgb(
				round((factor - 1) * color.R / 255) * 255 / factor,
				round((factor - 1) * color.G / 255) * 255 / factor,
				round((factor - 1) * color.B / 255) * 255 / factor
			);
		}
	}
}
