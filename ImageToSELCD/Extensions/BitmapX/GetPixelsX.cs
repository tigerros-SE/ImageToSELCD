using System;
using System.Drawing;

namespace ImageToSELCD.Extensions {
	public static partial class BitmapX {
		/// <summary>
		/// Gets all the pixels in this bitmap.
		/// </summary>
		/// <returns>A 2D <see cref="Color"/> array. The first dimension is the x axis, and the second is the y axis.</returns>
		public static Color[,] GetPixels2D(this Bitmap bm) {
			var pixels = new Color[bm.Width, bm.Height];

			for (int x = 0; x < bm.Width; x++) {
				for (int y = 0; y < bm.Height; y++) {
					pixels[x, y] = bm.GetPixel(x, y);
				}
			}

			return pixels;
		}

		/// <summary>
		/// Gets all the pixels in this bitmap.
		/// </summary>
		/// <returns>A <see cref="Color"/> array that contains all the pixels. Starts from the top left corner, then every row from left to right.</returns>
		public static Color[] GetPixels(this Bitmap bm) {
			var pixels = new Color[bm.Width * bm.Height];

			for (int i = 0; i < pixels.Length; i++) {
				int y = i / bm.Width;
				int x = i - (y * bm.Width);

				pixels[i] = bm.GetPixel(x, y);
			}
			
			return pixels;
		}
	}
}
