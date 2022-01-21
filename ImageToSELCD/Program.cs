using System;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ImageToSELCD.Extensions;
using ImageToSELCD.Dithering;

namespace ImageToSELCD {
	public static class Program {
		[STAThread]
		public static void Main() {
			while (true) {
				Console.Write("Enter the path of the image that you want to convert: ");
				string path = Console.ReadLine();
				Console.Write("Enter the size of the LCD panel (eg. 2x1 for a Wide LCD Panel): ");
				int[] size = Console.ReadLine().Split('x').Select(n => int.Parse(n)).ToArray();
				Console.Write("Do you wish to keep the aspect ratio of the resized image? [Y/N]: ");
				bool ratioBool = Console.ReadLine().ToUpper() == "Y";
				int ratioX = size[0];
				int ratioY = size[1];

				Image image = Image.FromFile(path);
				Bitmap bm = new Bitmap(image).Resize(ratioX * 178, ratioY * 178, ratioBool);
				var pixels = bm.GetPixels();//newBm.SetPixels(pixel => { pixel.Color = pixel.Color.Quantize(3); return pixel; });
				string converted = "";

				for (int i = 0; i < pixels.Length; i++) {
					converted += pixels[i].Color.ToSEChar();
				}

				converted = Regex.Replace(converted, $".{{{bm.Width}}}", "$0\n");

				Clipboard.SetText(converted);
				Console.WriteLine("The text has been copied to your clipboard.");
			}
		}
	}
}