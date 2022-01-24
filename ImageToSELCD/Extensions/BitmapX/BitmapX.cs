using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageToSELCD.Extensions {
	public static partial class BitmapX {
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
