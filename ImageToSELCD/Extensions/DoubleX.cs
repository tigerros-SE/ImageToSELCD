namespace ImageToSELCD.Extensions {
	public static class DoubleX {
		/// <summary>
		/// Rounds the double to the nearest integer. 0.5 rounds up.
		/// </summary>
		/// <param name="d">The double to round.</param>
		/// <returns>The nearest integer.</returns>
		public static int Round(this double d) {
			int floored = (int)d;
			double dec = d - floored;

			return dec >= 0.5 ? floored + 1 : floored;
		}
	}
}
