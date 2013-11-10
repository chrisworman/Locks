using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Locks.iOS.Views.Effects {

	public abstract class Effect {

//		protected double LengthInMilliseconds;
//
//		protected double ElapsedTimeInMilliseconds = 0d;
//
//		public delegate void OnCompleteDelegate (Effect effectThatIsComplete);
//
//		public Effect AfterEffect { get; set; }
//
//		public OnCompleteDelegate OnComplete;
//
//		public Effect(double lengthInMilliseconds) {
//			LengthInMilliseconds = lengthInMilliseconds;
//		}
//
//		public Effect(double lengthInMilliseconds, SoundEffect sound) {
//			LengthInMilliseconds = lengthInMilliseconds;
//			if (sound != null) {
//				sound.CreateInstance ().Play ();
//			}
//		}
//
//		public void Update(GameTime gameTime, View view) {
//			ElapsedTimeInMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;
//			UpdateEffect (gameTime, view);
//			if (IsComplete()) {
//				if (AfterEffect != null) {
//					view.StartEffect (AfterEffect);
//				}
//				if (OnComplete != null) {
//					OnComplete (this);
//				}
//			}
//		}
//
//		protected abstract void UpdateEffect(GameTime gameTime, View view);
//
//		/// <summary>
//		/// Is this effect complete?
//		/// </summary>
//		/// <returns><c>true</c> if this instance is complete; otherwise, <c>false</c>.</returns>
//		public bool IsComplete() { 
//			float amountComplete;
//			return IsComplete (out amountComplete);
//		}
//
//		/// <summary>
//		/// Is this effect complete?  Also returns the amount that this effect is complete as an output parameter.
//		/// </summary>
//		/// <returns><c>true</c> if and only if this instance is complete.</returns>
//		/// <param name="amountComplete">The amount that this effect is complete.</param>
//		public bool IsComplete(out float amountComplete) {
//			amountComplete = GetAmountComplete ();
//			return amountComplete >= 1f;
//		}
//
//		/// <summary>
//		/// Get the amount that this effect is complete between 0 and 1.
//		/// </summary>
//		/// <returns>The amount complete.</returns>
//		public float GetAmountComplete() {
//			double amountComplete = ElapsedTimeInMilliseconds / LengthInMilliseconds;
//			if (amountComplete >= 1d) {
//				return 1f;
//			} else {
//				return (float) amountComplete;
//			}
//		}

	}
}

