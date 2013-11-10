using System;
using Microsoft.Xna.Framework;

namespace Locks.iOS.Views.Effects
{
	public class Scale : Effect
	{

		private float DeltaScale { get; set; }

		private float InitialScale { get; set; }

		private float FinalScale { get; set; }

		public Scale (float initialScale, float finalScale, double lengthInMilliseconds) :
			base(lengthInMilliseconds)
		{
			InitialScale = initialScale;
			FinalScale = finalScale;
			DeltaScale = finalScale - initialScale;
		}

		protected override void UpdateEffect (GameTime gameTime, View view) 
		{
			float amountComplete = 0f;
			if (IsComplete (out amountComplete)) {
				view.Scale = FinalScale;
			} else {
				view.Scale = InitialScale + (DeltaScale * amountComplete);
			}
		}

	}
}

