using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Locks.iOS.Views {

	/// <summary>
	/// A sprite that can be tapped by the user.
	/// </summary>
	public class Button : Sprite {

//		public bool Enabled { get; set; }
//
//		public delegate void OnTapDelegate (Button spriteThatWasTapped);
//
//		public OnTapDelegate OnTap;
//
//		public Button (Texture2D texture) :
//			this(texture, new Vector2(0,0)) {
//		}
//
//		public Button (Texture2D texture, Constants.ViewLayer layer) :
//			this(texture, new Vector2(0,0), layer) {
//		}
//
//		public Button (Texture2D texture, Vector2 texturePosition) :
//		this(texture, texturePosition, Constants.ViewLayer.Layer1) {
//		}
//
//		public Button (Texture2D texture, Vector2 texturePosition, Constants.ViewLayer layer) :
//			base(texture, texturePosition, layer) {
//			Enabled = true;
//		}
//
//		/// <summary>
//		/// If this sprite is Visible, Enabled, and the gesture is a tap within this sprite, then consume the gesture and call the OnTap delegate method.
//		/// </summary>
//		/// <returns>true</returns>
//		/// <c>false</c>
//		/// <param name="gesture">Gesture.</param>
//		public override bool ConsumeGesture(GestureSample gesture) {
//			bool wasTapped = WasTapped (gesture);
//			if (wasTapped && OnTap != null) {
//				OnTap (this);
//			}
//			return wasTapped;
//		}
//
//		protected bool WasTapped(GestureSample gesture) {
//			if (Visible && Enabled && gesture.GestureType == GestureType.Tap) {
//				Rectangle bounds = new Rectangle (Convert.ToInt32 (Position.X), Convert.ToInt32 (Position.Y), Texture.Width, Texture.Height);
//				return bounds.Contains (gesture.Position);
//			}
//			return false;
//		}

	}
}

