using System;

namespace ImageToSELCD.Extensions {
	public static class MathX {
		public static T Clamp<T>(this T value, T min, T max) where T : IComparable
			=> value.CompareTo(max) > 0 ? max : value.CompareTo(min) < 0 ? min : value;
	}
}
