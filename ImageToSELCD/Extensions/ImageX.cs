using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToSELCD.Extensions {
	public static class ImageX {
		/// <summary>
		/// Gets the colors of all pixels in this image.
		/// </summary>
		/// <returns>A 2D <see cref="Color"/> array. The first dimension is the width, and the second is the height.</returns>
		public static Pixel[,] GetPixels2D(this Image image)
			=> new Bitmap(image).GetPixels2D();

		/// <summary>
		/// Gets the colors of all pixels in this image.
		/// </summary>
		/// <returns>A <see cref="Color"/> array that contains all the pixels. Starts from the top left corner, then every row from left to right.</returns>
		public static Pixel[] GetPixels(this Image image)
			=> new Bitmap(image).GetPixels();

		/// <summary>
		/// Resize the image to the specified width and height.
		/// </summary>
		/// <param name="image">The image to resize.</param>
		/// <param name="width">The width to resize to.</param>
		/// <param name="height">The height to resize to.</param>
		/// <returns>The resized image.</returns>
		public static Bitmap Resize(this Image image, int width, int height, bool keepAspectRatio = false)
			=> new Bitmap(image).Resize(width, height, keepAspectRatio);
	}
}
