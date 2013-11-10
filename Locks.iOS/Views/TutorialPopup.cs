using System;

namespace Locks.iOS.Views
{
	public class TutorialPopup : Sunfish.Views.Popup
	{
		private Sunfish.Views.Container SecondHint;

		public TutorialPopup () :
			base(LocksGame.ActiveScreen.LoadTexture("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered)
		{
			CreateAndAddFirstHint ();
			PrepareSecondHint ();
		}

		private void CreateAndAddFirstHint ()
		{
			Sunfish.Views.Sprite lockIndicator = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockIndicatorUnlocked"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite dial = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockDial"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite dialHint = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("TutorialDialHint"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite nextButton = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("NextButton"), Sunfish.Constants.ViewLayer.Modal);
			nextButton.EnableTapGesture(HandleNextButtonTapped);

			AddChild (lockIndicator, 0, PixelsWithDensity (40));
			AddChild (dial, 0, PixelsWithDensity (10));
			AddChild (dialHint, 0, PixelsWithDensity (10));
			AddChild (nextButton, 0, PixelsWithDensity (20));
		}

		private void PrepareSecondHint ()
		{
			Sunfish.Views.Sprite lockedIndicator = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockIndicatorLocked"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite lockedDial = Lock.CreateDial (-1);
			lockedDial.Layer = Sunfish.Constants.ViewLayer.Modal;
			Sunfish.Views.Container lockedContainer = new Sunfish.Views.Container (lockedDial.Width, 0, Sunfish.Constants.ViewContainerLayout.StackCentered);
			lockedContainer.Layer = Sunfish.Constants.ViewLayer.Modal;
			lockedContainer.ShouldExpandHeight = true;
			lockedContainer.AddChild (lockedIndicator);
			lockedContainer.AddChild (lockedDial, 0, PixelsWithDensity (10));

			Sunfish.Views.Sprite rotateCW = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("RotateCWButton"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite tapIcon = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("TapIcon"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Container rotateContainer = new Sunfish.Views.Container (Math.Max (rotateCW.Width, tapIcon.Width), 0, Sunfish.Constants.ViewContainerLayout.Absolute);
			rotateContainer.Layer = Sunfish.Constants.ViewLayer.Modal;
			rotateContainer.ShouldExpandHeight = true;
			rotateContainer.AddChild (rotateCW);
			rotateContainer.AddChild (tapIcon, rotateCW.Width / 3, rotateCW.Height / 2);

			Sunfish.Views.Sprite unlockedIndicator = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockIndicatorUnlocked"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite unlockedDial = Lock.CreateDial (0);
			unlockedDial.Layer = Sunfish.Constants.ViewLayer.Modal;
			Sunfish.Views.Container unlockedContainer = new Sunfish.Views.Container (unlockedDial.Width, 0, Sunfish.Constants.ViewContainerLayout.StackCentered);
			unlockedContainer.Layer = Sunfish.Constants.ViewLayer.Modal;
			unlockedContainer.ShouldExpandHeight = true;
			unlockedContainer.AddChild (unlockedIndicator);
			unlockedContainer.AddChild (unlockedDial, 0, PixelsWithDensity (10));

			Sunfish.Views.Sprite firstRightArrow = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("RightArrow"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite secondRightArrow = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("RightArrow"), Sunfish.Constants.ViewLayer.Modal);
			int howToRotateContainerWidth = lockedContainer.Width + firstRightArrow.Width + rotateContainer.Width + secondRightArrow.Width + unlockedContainer.Width;
			Sunfish.Views.Container howToRotateContainer = new Sunfish.Views.Container (howToRotateContainerWidth, 0, Sunfish.Constants.ViewContainerLayout.FloatLeft);
			howToRotateContainer.ShouldExpandHeight = true;
			howToRotateContainer.Layer = Sunfish.Constants.ViewLayer.Modal;
			howToRotateContainer.AddChild (lockedContainer);
			howToRotateContainer.AddChild (firstRightArrow, 0, PixelsWithDensity (15));
			howToRotateContainer.AddChild (rotateContainer);
			howToRotateContainer.AddChild (secondRightArrow, 0, PixelsWithDensity (15));
			howToRotateContainer.AddChild (unlockedContainer);

			Sunfish.Views.Sprite rotateHint = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("TutorialRotateHint"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite closeButton = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("CloseButton"), Sunfish.Constants.ViewLayer.Modal);
			closeButton.EnableTapGesture(HandleCloseButtonTapped);

			SecondHint = new Sunfish.Views.Container (Width, Height, Sunfish.Constants.ViewContainerLayout.StackCentered);
			SecondHint.Layer = Sunfish.Constants.ViewLayer.Modal;
			SecondHint.AddChild (howToRotateContainer, 0, PixelsWithDensity (40));
			SecondHint.AddChild (rotateHint, 0, PixelsWithDensity (10));
			SecondHint.AddChild (closeButton, 0, PixelsWithDensity (20));

		}

		private void HandleNextButtonTapped (Sunfish.Views.View closeButton)
		{
			//RemoveChildren ();
			AddChild (SecondHint);
		}

		private void HandleCloseButtonTapped (Sunfish.Views.View closeButton)
		{
			Hide ();
		}
	}
}

