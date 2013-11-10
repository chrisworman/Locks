using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Locks.iOS.Views
{
	public class Overlay : Sprite {

		public delegate void OnAppearDelegate ();

		public OnAppearDelegate OnAppear;

		public delegate void OnDisappearDelegate ();

		public OnDisappearDelegate OnDisappear;

		public static Overlay CreateOpaque(Screens.Screen screen) {
			return new Overlay (LocksGame.ActiveScreen.LoadTexture ("OverlayOpaquePixel"));
		}

		public static Overlay CreateTransparent() {
			return new Overlay (LocksGame.ActiveScreen.LoadTexture ("OverlayTransparentPixel"));
		}

		private Overlay (Texture2D pixel) :
		base(pixel, new Vector2(0,0), Constants.ViewLayer.Modal) {
		}

		public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics){
			DrawTextureFullScreen (Texture);
		}

		public void Appear() {
			Effects.Appear appearEffect = new Effects.Appear (500);
			appearEffect.OnComplete = HandleAppearEffectOnComplete;
			StartEffect (appearEffect);
		}

		private void HandleAppearEffectOnComplete(Effects.Effect effect) {
			if (OnAppear != null) {
				OnAppear ();
			}
		}

		public void Disappear() {
			Effects.Disappear disappearEffect = new Effects.Disappear (500);
			disappearEffect.OnComplete = HandleDisappearEffectOnComplete;
			StartEffect (disappearEffect);
		}

		private void HandleDisappearEffectOnComplete(Effects.Effect effect) {
			if (OnDisappear != null) {
				OnDisappear ();
			}
		}

	}
}

