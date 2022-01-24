using System;
using System.Drawing;

namespace ImageToSELCD.Extensions {
	public static partial class BitmapX {
		/// <summary>
		/// Gets all the pixels in this bitmap and applies the <paramref name="selector"/> function on them.
		/// </summary>
		/// <returns>A 2D <see cref="Color"/> array. The first dimension is the width, and the second is the height.</returns>
		public static Color[,] SetPixels2D(this Bitmap bm, Func<Color, Color> selector) {
			var pixels = new Color[bm.Width, bm.Height];

			for (int x = 0; x < bm.Width; x++) {
				for (int y = 0; y < bm.Height; y++) {
					pixels[x, y] = selector(bm.GetPixel(x, y));
				}
			}

			return pixels;
		}

		/// <summary>
		/// Changes the pixels of the bitmap.
		/// </summary>
		/// <returns>A bitmap with the changed pixels.</returns>
		public static Bitmap SetPixels2D(this Bitmap bm, Color[,] newPixels) {
			var newBm = new Bitmap(bm);

			for (int x = 0; x < bm.Width; x++) {
				for (int y = 0; y < bm.Height; y++) {
					newBm.SetPixel(x, y, newPixels[x, y]);
				}
			}

			return newBm;
		}

		/// <summary>
		/// Gets all the pixels in this bitmap and applies the <paramref name="selector"/> function on them.
		/// </summary>
		/// <returns>A <see cref="Color"/> array that contains all the pixels. Starts from the top left corner, then every row from left to right.</returns>
		public static Color[] SetPixels(this Bitmap bm, Func<Color, Color> selector) {
			var pixels = new Color[bm.Width * bm.Height];

			for (int i = 0; i < pixels.Length; i++) {
				int y = i / bm.Width;
				int x = i - (y * bm.Width);

				pixels[i] = selector(bm.GetPixel(x, y));
			}

			return pixels;
		}

		/// <summary>
		/// Changes the pixels of the bitmap.
		/// </summary>
		/// <returns>A bitmap with the changed pixels.</returns>
		public static Bitmap SetPixels(this Bitmap bm, Color[] newPixels) {
			var newBm = new Bitmap(bm);

			for (int i = 0; i < bm.Width * bm.Height; i++) {
				int y = i / bm.Width;
				int x = i - (y * bm.Width);

				newBm.SetPixel(x, y, newPixels[i]);
			}

			return newBm;
		}
	}
}
