using System;

namespace Locks.iOS.Views
{
	public class TutorialPopup : Sunfish.Views.Popup
	{

		Sunfish.Views.Sprite TutorialContent = null;

		Sunfish.Views.Sprite CloseButton = null;

		public TutorialPopup () :
		base(LocksGame.ActiveScreen.LoadTexture("TutorialPopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered)
		{

			Visible = false;

			TransitionAudioFilename = "PopupTransition";
			TransitionAudioVolume = 0.8f;

			TutorialContent = new Sunfish.Views.Sprite(LocksGame.ActiveScreen.LoadTexture("TutorialContent"), Sunfish.Constants.ViewLayer.Modal);
			AddChild (TutorialContent, 0, PixelsWithDensity (150));

			CloseButton = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("CloseButton"), Sunfish.Constants.ViewLayer.Modal);
			CloseButton.EnableTapGesture(HandleCloseButtonTapped);
			AddChild (CloseButton, 0, PixelsWithDensity (50));

		}

		public override void Show ()
		{
			//TutorialContent.StartEffect (new Sunfish.Views.Effects.Appear (1500d));
			//CloseButton.StartEffect (new Sunfish.Views.Effects.Appear (1500d));
			base.Show ();
		}

		public override void Hide()
		{
			//TutorialContent.StartEffect (new Sunfish.Views.Effects.Disappear (1500d));
			//CloseButton.StartEffect (new Sunfish.Views.Effects.Disappear (1500d));
			base.Hide ();
		}

		private void HandleCloseButtonTapped (Sunfish.Views.View closeButton)
		{
			Hide ();
		}
	}
}

