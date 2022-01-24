using System;
using System.Drawing;

namespace ImageToSELCD.Extensions {
	public static class ColorX {
		const double BIT_SPACING = 255.0 / 7.0;
		
		public static char ToSEChar(this Color c) => ColorToSEChar(c.R, c.G, c.B);
		public static char ColorToSEChar(byte r, byte g, byte b)
			=> (char)(0xe100 +
			((int)Math.Round(r / BIT_SPACING) << 6) +
			((int)Math.Round(g / BIT_SPACING) << 3) +
			(int)Math.Round(b / BIT_SPACING));

		public static Color Clamp(this Color c, Color min, Color max)
			=> Color.FromArgb(c.R.Clamp(min.R, max.R), c.G.Clamp(min.G, max.G), c.B.Clamp(min.B, max.B));

		public static Color Clamp(this Color c, byte min, byte max)
			=> Color.FromArgb(c.R.Clamp(min, max), c.G.Clamp(min, max), c.B.Clamp(min, max));

		public static Color FromArgbClamped(int r, int g, int b)
			=> Color.FromArgb(r.Clamp(0, 255), g.Clamp(0, 255), b.Clamp(0, 255));

		public static Color Add(this Color a, Color b)
			=> FromArgbClamped(a.R + b.R, a.G + b.G, a.B + b.B);

		public static Color Add(this Color a, byte b)
			=> FromArgbClamped(a.R + b, a.G + b, a.B + b);

		public static Color Sub(this Color a, Color b)
			=> FromArgbClamped(a.R - b.R, a.G - b.G, a.B - b.B);

		public static Color Sub(this Color a, byte b)
			=> FromArgbClamped(a.R - b, a.G - b, a.B - b);

		public static Color Mul(this Color a, Color b)
			=> FromArgbClamped(a.R * b.R, a.G * b.G, a.B * b.B);

		public static Color Mul(this Color a, byte b)
			=> FromArgbClamped(a.R * b, a.G * b, a.B * b);

		public static Color Mul(this Color a, double b)
			=> FromArgbClamped((a.R * b).Round(), (a.G * b).Round(), (a.B * b).Round());

		public static Color Div(this Color a, Color b)
			=> FromArgbClamped(a.R / b.R, a.G / b.G, a.B / b.B);

		public static Color Div(this Color a, byte b)
			=> FromArgbClamped(a.R / b, a.G / b, a.B / b);

		public static Color Quantize(this Color c, int factor)
			=> FromArgbClamped(
				DoubleX.Round(factor * c.R / 255) * (255 / factor),
				DoubleX.Round(factor * c.G / 255) * (255 / factor),
				DoubleX.Round(factor * c.B / 255) * (255 / factor)
			);
	}
}
