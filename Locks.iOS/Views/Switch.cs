using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Locks.iOS.Views {

	public class Switch : Button {

		private Texture2D OnTexture { get; set; }

		private Texture2D OffTexture { get; set; }

		public bool IsOn { get; private set; }

		public static Switch CreateOn (Texture2D onTexture, Texture2D offTexture, Constants.ViewLayer layer) {
			return new Switch (onTexture, onTexture, offTexture, true, new Vector2 (0, 0), layer);
		}

		public static Switch CreateOff (Texture2D onTexture, Texture2D offTexture, Constants.ViewLayer layer) {
			return new Switch (offTexture, onTexture, offTexture, false, new Vector2 (0, 0), layer);
		}

		protected Switch (Texture2D currentTexture, Texture2D onTexture, Texture2D offTexture, bool isOn, Vector2 texturePosition, Constants.ViewLayer layer) :
		base(currentTexture, texturePosition, layer) {
			OnTexture = onTexture;
			OffTexture = offTexture;
			IsOn = isOn;
		}

		public bool Toggle() {
			IsOn = !IsOn;
			this.Texture = IsOn ? OnTexture : OffTexture;
			return IsOn;
		}

		public override bool ConsumeGesture(GestureSample gesture) {
			bool wasTapped = WasTapped (gesture);
			if (wasTapped) {
				Toggle ();
				if (OnTap != null) {
					OnTap (this);
				}
			}
			return wasTapped;
		}

	}
}

