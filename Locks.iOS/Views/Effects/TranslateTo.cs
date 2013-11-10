using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Locks.iOS.Views.Effects {

	public class TranslateTo : Effect {

		private Vector2 InitialPosition;

		private Vector2 FinalPosition;

		private Vector2 DeltaPosition;

		public TranslateTo (Vector2 initialPosition, Vector2 finalPosition, double lengthInMilliseconds, SoundEffect sound):
			base(lengthInMilliseconds, sound){
			InitialPosition = initialPosition;
			FinalPosition = finalPosition;
			DeltaPosition = Vector2.Subtract (finalPosition, initialPosition); // Compute this once now for better performance in UpdateEffect
		}

		public TranslateTo (Vector2 initialPosition, Vector2 finalPosition, double lengthInMilliseconds):
			base(lengthInMilliseconds){
			InitialPosition = initialPosition;
			FinalPosition = finalPosition;
			DeltaPosition = Vector2.Subtract (finalPosition, initialPosition); // Compute this once now for better performance in UpdateEffect
		}

		protected override void UpdateEffect (GameTime gameTime, View view) {
			float amountComplete;
			if (IsComplete (out amountComplete)) {
				view.Position = FinalPosition;
			} else {
				Vector2 offsetPosition = Vector2.Multiply (DeltaPosition, new Vector2 (amountComplete));
				view.Position = Vector2.Add (InitialPosition, offsetPosition);
			}
		}

	}
}

