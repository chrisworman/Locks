using System;
using Microsoft.Xna.Framework;

namespace Locks.iOS.Views.Effects
{
	public class Pulsate : Effect
	{

		private Color PulseColor { get; set; }

		private int Pulses { get; set; }

		private int PulseNumber { get; set; }

		private double MillisecondsPerPulse { get; set; }

		private double PulseEllapsedTimeMilliseconds { get; set; }

		public Pulsate (double lengthInMillisecondsPerPulse, int pulses, Color pulseColor) :
			base (lengthInMillisecondsPerPulse * pulses)
		{
			Pulses = pulses;
			PulseColor = pulseColor;
			MillisecondsPerPulse = LengthInMilliseconds / Pulses;
			PulseNumber = 1;
			PulseEllapsedTimeMilliseconds = 0d;
		}

		protected override void UpdateEffect(GameTime gameTime, View view)
		{
			if (IsComplete ()) {
				view.OverlayColor = Color.White;
			} else {

				// Calculate how long this pulse has been running for
				PulseEllapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;

				// Has the current pulse ended? If so, increment to the next pulse and reset the pulse start time
				if (PulseEllapsedTimeMilliseconds >= MillisecondsPerPulse) {
					PulseNumber++;
					PulseEllapsedTimeMilliseconds = PulseEllapsedTimeMilliseconds - MillisecondsPerPulse;
				}

				// Calculate how much of the current pulse is complete (between 0 and 1)
				double pulseAmountComplete = PulseEllapsedTimeMilliseconds / MillisecondsPerPulse;

				// Interpolate using the SIN function for smooth pulsing
				float pulseColorAlpha = 1.0f - (float)Math.Sin (Math.PI * pulseAmountComplete);

				// New code: avoids creating a new color every update
				view.OverlayColor.R = PulseColor.R;
				view.OverlayColor.G = PulseColor.G;
				view.OverlayColor.B = PulseColor.B;
				view.OverlayColor.A = (byte) (Byte.MaxValue * pulseColorAlpha);

				// Old code: creates a new color every update
				//view.OverlayColor = new Color (PulseColor, pulseColorAlpha);

			}
		}

	}
}

