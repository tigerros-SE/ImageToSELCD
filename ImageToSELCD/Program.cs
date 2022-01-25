using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using TiExtensions.BitmapN;
using TiDithering.ErrorDiffusion;
using System.Text;
using TiExtensions.MathN;

namespace ImageToSELCD {
	public static class Program {
		const double BIT_SPACING = 255.0 / 7.0;

		public static char ToSEChar(this Color c) => ColorToSEChar(c.R, c.G, c.B);
		public static char ColorToSEChar(byte r, byte g, byte b)
			=> (char)(0xe100 +
			((r / BIT_SPACING).Round() << 6) +
			((g / BIT_SPACING).Round() << 3) +
			(b / BIT_SPACING).Round());

		public static string BitmapToSEString(Bitmap bm) {
			var converted = new StringBuilder();
			var pixels = bm.GetPixels2D();

			for (int y = 0; y < bm.Height; y++) {
				for (int x = 0; x < bm.Width; x++) {
					converted.Append(pixels[x, y].ToSEChar());
				}

				converted.Append("\n");
			}

			return converted.ToString();
		}

		public static T Prompt<T>(string prompt, Func<string, T> converter) {
			Console.Write(prompt);
			return converter(Console.ReadLine());
		}

		public static T PromptLine<T>(string prompt, Func<string, T> converter)
			=> Prompt(prompt + "\n", converter);

		[STAThread]
		public static void Main() {
			while (true) {
				Console.OutputEncoding = Encoding.UTF8;

				string path = Prompt("Enter the path of the image that you want to convert: ", s => s);
				int[] size = Prompt("Enter the size of the LCD panel (eg. 2x1 for a Wide LCD Panel): ", s => s.Split('x').Select(ns => int.Parse(ns)).ToArray());
				bool ratioBool = Prompt("Do you wish to keep the aspect ratio of the resized image? [Y/N]: ", s => s.ToUpper() == "Y");
				
				int ratioX = size[0];
				int ratioY = size[1];

				int factor = Prompt("Enter the quantize factor: ", int.Parse);

				Bitmap bm = Bitmap.FromFile(path).Resize(ratioX * 178, ratioY * 178, ratioBool);

				var fs = new FloydSteinberg(bm, factor);
				fs.Diffuse();

				string converted = BitmapToSEString(fs.Bitmap);
				Clipboard.SetText(converted);
				Console.WriteLine("All operations complete.");
			}
		}
	}
}