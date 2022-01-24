using System;
using System.Drawing;

namespace ImageToSELCD.Dithering {
	/// <summary>
	/// A Floyd-Steinberg implementation of the <see cref="ErrorDiffusionBase"/>.
	/// </summary>
	public sealed class FloydSteinberg : ErrorDiffusionBase {
		public override DiffusionPattern[] Patterns { get; } = {
			new DiffusionPattern(7, 1, 0),
			new DiffusionPattern(5, 0, 1),
			new DiffusionPattern(3, -1, 1),
			new DiffusionPattern(1, 1, 1)
		};

		public override int Divisor { get; } = 16;

		public FloydSteinberg(Bitmap bitmap, int quantizationFactor = 0) : base(bitmap, quantizationFactor) { }
	}
}