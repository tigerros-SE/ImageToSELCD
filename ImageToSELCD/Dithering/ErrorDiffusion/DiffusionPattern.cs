namespace ImageToSELCD.Dithering {
	/// <summary>
	/// This pattern is used for distributing the quantization error to a pixel.
	/// </summary>
	public sealed class DiffusionPattern {
		/// <summary>
		/// The amount of error (out of the <see cref="IErrorDiffusion.Divisor"/>) that is applied.
		/// </summary>
		public int Rate { get; }
		/// <summary>
		/// The x axis offset of the targeted pixel from the current pixel.
		/// </summary>
		public int XOffset { get; }
		/// <summary>
		/// The y axis offset of the targeted pixel from the current pixel.
		/// </summary>
		public int YOffset { get; }

		public DiffusionPattern(int rate, int xOffset, int yOffset) {
			Rate = rate;
			XOffset = xOffset;
			YOffset = yOffset;
		}
	}
}