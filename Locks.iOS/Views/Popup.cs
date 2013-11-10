using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Locks.iOS.Views
{
	public class Popup : Container
	{

//		private Vector2 HiddenPosition { get; set; }
//
//		private Vector2 ShowingPosition { get; set; }
//
//		private Overlay Overlay { get; set; }
//
//		private const double TransitionMilliseconds = 500;
//
//		public delegate void OnHiddenDelegate (Popup popupThatIsNowHidden);
//
//		public OnHiddenDelegate OnHidden;
//
//		public delegate void OnShownDelegate (Popup popupThatIsNowShown);
//
//		public OnShownDelegate OnShown;
//
//		private SoundEffectInstance TransitionSoundEffect { get; set; }
//
//		public Popup (Constants.ViewContainerLayout layout) : 
//		base(LocksGame.ActiveScreen.LoadTexture("PopupBackground"), new Vector2(0,0), Constants.ViewLayer.Modal, layout, false)
//		{
//
//			TransitionSoundEffect = LocksGame.ActiveScreen.LoadSoundEffect("PopupTransition");
//			TransitionSoundEffect.Volume = 0.1f;
//
//			// Create an overlay to obscure the screen when this popup is showing
//			Overlay = Overlay.CreateTransparent ();
//			Overlay.Layer = Constants.ViewLayer.ModalOverlay;
//			ChildViews.Add (Overlay);
//
//			// Compute the position coordinates for the various states of this popup
//			float CenterXPosition = (LocksGame.ScreenHeight - Width) / 2.0f;
//			HiddenPosition = new Vector2 (CenterXPosition, -LocksGame.ScreenWidth - 1.0f);
//			ShowingPosition = new Vector2 (CenterXPosition, (LocksGame.ScreenWidth - Height) / 2.0f);
//			Position = HiddenPosition;
//
//		}
//
//		public override bool ConsumeGesture (GestureSample gesture)
//		{
//			if (Visible) {
//				base.ConsumeGesture (gesture);
//				return true; // Popups are modal and hence consume all gestures
//			}
//			return false;
//		}
//
//		public void Show ()
//		{
//			Overlay.Appear ();
//			Effects.TranslateTo translateToShownPosition = new Effects.TranslateTo (Position, ShowingPosition, TransitionMilliseconds);
//			translateToShownPosition.OnComplete = HandleTranslatedtoShownPosition;
//			StartEffect (translateToShownPosition);
//			Visible = true;
//			if (LocksGame.SoundEffectsOn) {
//				TransitionSoundEffect.Play ();
//			}
//		}
//
//		public void Hide ()
//		{
//			Overlay.Disappear ();
//			Effects.TranslateTo translateToHiddenPosition = new Effects.TranslateTo (Position, HiddenPosition, TransitionMilliseconds);
//			translateToHiddenPosition.OnComplete = HandleTranslatedtoHiddenPosition;
//			StartEffect (translateToHiddenPosition);
//			if (LocksGame.SoundEffectsOn) {
//				TransitionSoundEffect.Play ();
//			}
//		}
//
//		public override void RemoveChildren ()
//		{
//			base.RemoveChildren ();
//			Sprite background = new Sprite (LocksGame.ActiveScreen.LoadTexture ("PopupBackground"), Constants.ViewLayer.Modal);
//			AddChild (background, Constants.ViewContainerLayout.Absolute, 0, 0);
//			Overlay = Overlay.CreateTransparent ();
//			Overlay.Layer = Constants.ViewLayer.ModalOverlay;
//			ChildViews.Add (Overlay);
//		}
//
//		public void HandleTranslatedtoHiddenPosition (Effects.Effect effect)
//		{
//			Visible = false;
//			if (OnHidden != null) {
//				OnHidden (this);
//			}
//		}
//
//		public void HandleTranslatedtoShownPosition (Effects.Effect effect)
//		{
//			if (OnShown != null) {
//				OnShown (this);
//			}
//		}
	}
}

