using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToSELCD.Extensions {
	public class Pixel {
		public Color Color { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public Pixel(Color color = default, int x = default, int y = default) {
			Color = color;
			X = x;
			Y = y;
		}
	}

	public static class BitmapX {
		/// <summary>
		/// Gets all the pixels in this bitmap.
		/// </summary>
		/// <returns>A 2D <see cref="Pixel"/> array. The first dimension is the width, and the second is the height.</returns>
		public static Pixel[,] GetPixels2D(this Bitmap bm) {
			var pixels = new Pixel[bm.Width, bm.Height];

			for (int x = 0; x < pixels.GetLength(0); x++) {
				for (int y = 0; y < pixels.GetLength(1); y++) {
					pixels[x, y] = new Pixel(bm.GetPixel(x, y), x, y);
				}
			}

			return pixels;
		}

		/// <summary>
		/// Gets all the pixels in this bitmap.
		/// </summary>
		/// <returns>A <see cref="Pixel"/> array that contains all the pixels. Starts from the top left corner, then every row from left to right.</returns>
		public static Pixel[] GetPixels(this Bitmap bm) {
			var pixels = new Pixel[bm.Width * bm.Height];

			for (int i = 0; i < pixels.Length; i++) {
				int y = i / bm.Width;
				int x = i - (y * bm.Width);

				pixels[i] = new Pixel(bm.GetPixel(x, y), x, y);
			}

			return pixels;
		}

		/// <summary>
		/// Gets all the pixels in this bitmap and applies the <paramref name="selector"/> function on them.
		/// </summary>
		/// <returns>A 2D <see cref="Pixel"/> array. The first dimension is the width, and the second is the height.</returns>
		public static Pixel[,] SetPixels2D(this Bitmap bm, Func<Pixel, Pixel> selector) {
			var pixels = new Pixel[bm.Width, bm.Height];

			for (int x = 0; x < pixels.GetLength(0); x++) {
				for (int y = 0; y < pixels.GetLength(1); y++) {
					pixels[x, y] = selector(new Pixel(bm.GetPixel(x, y), x, y));
				}
			}

			return pixels;
		}

		/// <summary>
		/// Gets all the pixels in this bitmap and applies the <paramref name="selector"/> function on them.
		/// </summary>
		/// <returns>A <see cref="Pixel"/> array that contains all the pixels. Starts from the top left corner, then every row from left to right.</returns>
		public static Pixel[] SetPixels(this Bitmap bm, Func<Pixel, Pixel> selector) {
			var pixels = new Pixel[bm.Width * bm.Height];

			for (int i = 0; i < pixels.Length; i++) {
				int y = i / bm.Width;
				int x = i - (y * bm.Width);

				pixels[i] = selector(new Pixel(bm.GetPixel(x, y), x, y));
			}

			return pixels;
		}

		/// <summary>
		/// Resize the bitmap to the specified width and height.
		/// </summary>
		/// <param name="bm">The <see cref="Bitmap"/> to resize.</param>
		/// <param name="width">The width to resize to.</param>
		/// <param name="height">The height to resize to.</param>
		/// <returns>The resized <see cref="Bitmap"/>.</returns>
		public static Bitmap Resize(this Bitmap bm, int width, int height, bool keepAspectRatio = false) {
			double ratioX = (double)width / bm.Width;
			double ratioY = (double)height / bm.Height;
			double ratio = ratioX < ratioY ? ratioX : ratioY;
			int newWidth = Convert.ToInt32(bm.Width * ratio);
			int newHeight = Convert.ToInt32(bm.Height * ratio);

			var destRect = new Rectangle(0, 0, keepAspectRatio ? newWidth : width, keepAspectRatio ? newHeight : height);
			var destImage = new Bitmap(keepAspectRatio ? newWidth : width, keepAspectRatio ? newHeight : height);

			destImage.SetResolution(bm.HorizontalResolution, bm.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage)) {
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighSpeed;
				graphics.InterpolationMode = InterpolationMode.Low;
				graphics.SmoothingMode = SmoothingMode.HighSpeed;
				graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

				using (var wrapMode = new ImageAttributes()) {
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(bm, destRect, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			return destImage;
		}
	}
}
