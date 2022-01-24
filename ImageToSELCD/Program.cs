using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ImageToSELCD.Extensions;
using ImageToSELCD.Dithering;
using System.Text;

namespace ImageToSELCD {
	public static class Program {
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

				Image image = Image.FromFile(path);
				Bitmap bm = new Bitmap(image).Resize(ratioX * 178, ratioY * 178, ratioBool);

				var fs = new FloydSteinberg(bm, factor);
				fs.Diffuse();

				string converted = BitmapToSEString(fs.Bitmap);
				Clipboard.SetText(converted);
				Console.WriteLine("All operations complete.");
			}
		}
	}
}