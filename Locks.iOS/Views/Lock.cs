using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Locks.iOS.Views
{
	public class Lock : Sunfish.Views.Container
	{
		public Models.Lock LockModel;
		public Sunfish.Views.Sprite Dial;
		private Dictionary<int, Sunfish.Views.Switch> LockButtonViewsDictionary;
		private Sunfish.Views.Sprite LockIndicator;
		private Texture2D LockIndicatorLockedTexture;
		private Texture2D LockIndicatorUnlockedTexture;
		private Sunfish.Views.Sprite GearMedium;

		public delegate void OnLockButtonPushDelegate (Models.LockButtonPushResult pushResult);

		public OnLockButtonPushDelegate OnLockButtonPush;

		public delegate void OnDialRotateCompleteDelegate (Lock lockWhoseDialRotated);

		public OnDialRotateCompleteDelegate OnDialRotateComplete;

		public Lock (Models.Lock lockModel, bool isFirstRow, bool isLastRow, bool isFirstCol, bool isLastCol) :
			base(LoadLockBackground(), new Vector2(0,0), Sunfish.Constants.ViewLayer.Layer2, Sunfish.Constants.ViewContainerLayout.Absolute)
		{

			LockModel = lockModel;

			CreateGearsAndPipes (isFirstRow, isLastRow, isFirstCol, isLastCol);

			Sunfish.Views.Sprite faceplate = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockFaceplate"), Sunfish.Constants.ViewLayer.Layer3);
			AddChild (faceplate);

			Layout = Sunfish.Constants.ViewContainerLayout.FloatLeft;

			CreateLockIndicatorDial ();
			CreateLocksButtons ();

		}

		private void CreateGearsAndPipes (bool isFirstRow, bool isLastRow, bool isFirstCol, bool isLastCol)
		{

			float halfWidth = (float)Width * 0.5f;
			float halfHeight = (float)Height * 0.5f;


			// Gears
			Texture2D gearMediumTexture = LocksGame.ActiveScreen.LoadTexture ("GearMedium");
			Vector2 gearMediumPosition = new Vector2 ((halfWidth - ((float)gearMediumTexture.Width) * 0.5f) - PixelsWithDensity (20), PixelsWithDensity (20));
			GearMedium = new Sunfish.Views.Sprite (gearMediumTexture, gearMediumPosition, Sunfish.Constants.ViewLayer.Layer2);
			GearMedium.CenterOrigin ();
			AddChild (GearMedium);

			// Horizontal Pipes
			Texture2D pipeHorizontal = LocksGame.ActiveScreen.LoadTexture ("PipeHorizontal1");
			if (isLastCol) {
				Vector2 pipePosition = new Vector2 (-pipeHorizontal.Width, halfHeight);
				AddChild (new Sunfish.Views.Sprite (pipeHorizontal, pipePosition, Sunfish.Constants.ViewLayer.Layer1));
			} else {
				Vector2 pipePosition = new Vector2 (Width, halfHeight);
				AddChild (new Sunfish.Views.Sprite (pipeHorizontal, pipePosition, Sunfish.Constants.ViewLayer.Layer1));
			}

			// Vertical Pipes
			Texture2D pipeVertical = LocksGame.ActiveScreen.LoadTexture ("PipeVertical1");
			if (isLastRow) {
				Vector2 pipePosition = new Vector2 (halfWidth, 0);
				AddChild (new Sunfish.Views.Sprite (pipeVertical, pipePosition, Sunfish.Constants.ViewLayer.Layer1));
			} else {
				Vector2 pipePosition = new Vector2 (halfWidth, Height);
				AddChild (new Sunfish.Views.Sprite (pipeVertical, pipePosition, Sunfish.Constants.ViewLayer.Layer1));
			}

		}

		private void CreateLockIndicatorDial ()
		{
			LockIndicator = CreateLockIndicator (LockModel.IsUnlocked ());
			Dial = CreateDial (LockModel.CurrentPosition);

			Sunfish.Views.Container dialContainer = new Sunfish.Views.Container (Width, 0, Sunfish.Constants.ViewLayer.Layer4, Sunfish.Constants.ViewContainerLayout.StackCentered);
			dialContainer.ShouldExpandHeight = true;
			dialContainer.AddChild (LockIndicator, 0, PixelsWithDensity (5));
			dialContainer.AddChild (Dial, 0, PixelsWithDensity (5));
			AddChild (dialContainer);

		}

		private Sunfish.Views.Sprite CreateLockIndicator (bool isUnlocked)
		{
			LockIndicatorLockedTexture = LocksGame.ActiveScreen.LoadTexture ("LockIndicatorLocked");
			LockIndicatorUnlockedTexture = LocksGame.ActiveScreen.LoadTexture ("LockIndicatorUnlocked");
			if (isUnlocked) {
				return new Sunfish.Views.Sprite (LockIndicatorUnlockedTexture, Sunfish.Constants.ViewLayer.Layer4);
			} else {
				return new Sunfish.Views.Sprite (LockIndicatorLockedTexture, Sunfish.Constants.ViewLayer.Layer4);
			}
		}

		private void CreateLocksButtons ()
		{
			LockButtonViewsDictionary = new Dictionary<int, Sunfish.Views.Switch> ();

			for (int buttonIndex=0; buttonIndex < LockModel.ButtonCount; buttonIndex++) {
				Models.LockButton lockButtonModel = LockModel.Buttons [buttonIndex];
				Sunfish.Views.Switch lockButtonView = null;
				if (lockButtonModel.IsOn) {
					lockButtonView = Sunfish.Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("RotateCCWButton"), LocksGame.ActiveScreen.LoadTexture ("RotateCWButton"), Sunfish.Constants.ViewLayer.Layer4);
				} else {
					lockButtonView = Sunfish.Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("RotateCCWButton"), LocksGame.ActiveScreen.LoadTexture ("RotateCWButton"), Sunfish.Constants.ViewLayer.Layer4);
				}
				lockButtonView.Data = lockButtonModel;
				lockButtonView.EnableTapGesture(HandleButtonTap);
				AddChild (lockButtonView, PixelsWithDensity (10), PixelsWithDensity (10));
				LockButtonViewsDictionary.Add (buttonIndex, lockButtonView);
			} 

		}

		public static Sunfish.Views.Sprite CreateDial (int currentPosition)
		{
			Sunfish.Views.Sprite dial = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockDial"), Sunfish.Constants.ViewLayer.Layer4);
			dial.CenterOrigin ();
			if (currentPosition != 0) {
				dial.RotationRadians = PositionsToRadians (currentPosition);
			}
			return dial;
		}

		public static Texture2D LoadLockBackground ()
		{
			return LocksGame.ActiveScreen.LoadTexture ("LockBackground");
		}

		private void HandleButtonTap (Sunfish.Views.View buttonThatWasTapped)
		{
			Models.LockButton lockButton = (Models.LockButton)buttonThatWasTapped.Data;
			int previousPosition = lockButton.ContainingLock.CurrentPosition;
			Models.LockButtonPushResult pushResult = lockButton.Push ();
			RotateDial (lockButton.ContainingLock.CurrentPosition - previousPosition);
			RotateGears ();
			if (OnLockButtonPush != null) {
				OnLockButtonPush (pushResult);
			}
			LocksGame.ActiveScreen.PlaySoundEffect ("LockButtonPushed");
			//LocksGame.ActiveScreen.PlaySoundEffect ("GearMedium", 0.7f);

		}

		public void SwitchLockButton (int buttonIndex)
		{
			Sunfish.Views.Switch lockButtonView = null;
			if (LockButtonViewsDictionary.TryGetValue (buttonIndex, out lockButtonView)) {

				// Pulsate the lock button
				lockButtonView.Toggle ();
				lockButtonView.StartEffect (new Sunfish.Views.Effects.Pulsate (400, 4, Color.White));

				RotateGears ();

			}
		}

		public void RotateDial (int positions)
		{
			float initialRadians = Dial.RotationRadians;
			float finalRadians = Dial.RotationRadians + PositionsToRadians (positions);
			Sunfish.Views.Effects.Rotate rotateEffect = new Sunfish.Views.Effects.Rotate (initialRadians, finalRadians, 300);
			rotateEffect.OnComplete = HandleRotateDialComplete;
			Dial.StartEffect (rotateEffect);
		}

		public void RotateGears ()
		{
			GearMedium.StartEffect (new Sunfish.Views.Effects.Rotate (GearMedium.RotationRadians, GearMedium.RotationRadians + (float)Math.PI, 1000d));

		}

		public static float PositionsToRadians (int positions)
		{
			return (((float)Math.PI * 2f) / Locks.Core.Constants.LockPositions) * positions;
		}

		private void HandleRotateDialComplete (Sunfish.Views.Effects.Effect effectThatIsComplete)
		{
			if (LockModel.IsLocked ()) {
				LockIndicator.Texture = LockIndicatorLockedTexture;
			} else {
				LockIndicator.Texture = LockIndicatorUnlockedTexture;
			}
			if (OnDialRotateComplete != null) {
				OnDialRotateComplete (this);
			}
		}
	}
}

