using System;
using System.Drawing;

namespace ImageToSELCD.Dithering {
	/// <summary>
	/// The interface used for the "error-diffusion" dithering technique.
	/// See <see cref="ErrorDiffusionBase"/> for a more implemented version.
	/// </summary>
	public interface IErrorDiffusion {
		/// <summary>
		/// The <see cref="System.Drawing.Bitmap"/> which to operate on.
		/// </summary>
		Bitmap Bitmap { get; }
		/// <summary>
		/// These patterns are used for distributing the quantization errors.
		/// </summary>
		DiffusionPattern[] Patterns { get; }
		/// <summary>
		/// This will be the divisor used with the diffusion rates in the <see cref="Patterns"/> property.
		/// </summary>
		int Divisor { get; }
		/// <summary>
		/// This property affects how many colours are in the image.
		/// For example, a <see cref="QuantizationFactor"/> of <c>2</c> would provide <c>2</c> options for each RGB component.
		/// These options would be <c>0</c> and <c>255</c>.
		/// The total amount of color combinations would therefore be <c>2 * 2 * 2 = 8</c>.
		/// </summary>
		int QuantizationFactor { get; }
		/// <summary>
		/// This method diffuses the pixels in the <see cref="Bitmap"/> and produces a dithered version of the <see cref="Bitmap"/>.
		/// </summary>
		void Diffuse();
	}
}