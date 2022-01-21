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
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Util;
using MediaToolkit.Options;

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
				//C: \Users\glaci\Downloads\bigkrtkusdiscordicon.png

				//var directoryPath = Path.ChangeExtension(path, null);
				//Directory.CreateDirectory(directoryPath);

				//using (var engine = new Engine()) {
				//	var outputFile = new MediaFile();
				//	var options = new ConversionOptions();
				//	var video = new MediaFile(path);

				//	engine.GetMetadata(video);

				//	Console.WriteLine(video.Metadata.VideoData.Fps);

				//	int milliseconds = video.Metadata.Duration.Minutes * 6000 + video.Metadata.Duration.Seconds * 1000 + video.Metadata.Duration.Milliseconds;

				//	for (int i = 0; i <= milliseconds; i += (int)video.Metadata.VideoData.Fps * 3) {
				//		options.Seek = TimeSpan.FromMilliseconds(i);
				//		outputFile.Filename = $@"{directoryPath}\frame_{i / (video.Metadata.VideoData.Fps * 3)}.jpeg";
				//		engine.GetThumbnail(video, outputFile, options);
				//		Console.WriteLine($"Frame #{i / (video.Metadata.VideoData.Fps * 3)}/{(int)(milliseconds / (video.Metadata.VideoData.Fps * 3))}");
				//	}
				//}

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