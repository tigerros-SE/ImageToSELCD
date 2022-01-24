using System;
using System.Drawing;
using ImageToSELCD.Extensions;

namespace ImageToSELCD.Dithering {
	/// <summary>
	/// Implements the <see cref="IErrorDiffusion.Diffuse"/> method and provides some constructors.
	/// </summary>
	public abstract class ErrorDiffusionBase : IErrorDiffusion {
		public abstract DiffusionPattern[] Patterns { get; }
		public abstract int Divisor { get; }
		public int QuantizationFactor { get; }
		public Bitmap Bitmap { get; }

		public ErrorDiffusionBase(Bitmap bitmap, int quantizationFactor = 0) {
			Bitmap = bitmap;
			QuantizationFactor = quantizationFactor;
		}

		public void Diffuse() {
			var pixels = Bitmap.GetPixels2D();

			for (int x = 0; x < Bitmap.Width; x++) {
				for (int y = 0; y < Bitmap.Height; y++) {
					var oldPix = pixels[x, y];
					var newPix = pixels[x, y].Quantize(QuantizationFactor);

					pixels[x, y] = newPix;

					var error = oldPix.Sub(newPix);

					foreach (var v in Patterns) {
						if (Bitmap.Width > x + v.XOffset
							&& x + v.XOffset >= 0
							&& Bitmap.Height > y + v.YOffset
							&& y + v.YOffset >= 0) {
							//Console.WriteLine(
							//	$"Old [{x} + {v.XOffset}, {y} + {v.YOffset}]: {pixels[x + v.XOffset, y + v.YOffset]} | " +
							//	$"\n\tNew: {pixels[x + v.XOffset, y + v.YOffset]} + ({error} * ({v.Rate} / {RateSum})) = {pixels[x + v.XOffset, y + v.YOffset].Add(error.Mul((double)v.Rate / RateSum))}");
							pixels[x + v.XOffset, y + v.YOffset] = pixels[x + v.XOffset, y + v.YOffset].Add(error.Mul((double)v.Rate / Divisor));
							Bitmap.SetPixel(x + v.XOffset, y + v.YOffset, pixels[x + v.XOffset, y + v.YOffset]);
						}
					}
				}
			}
		}
	}
}