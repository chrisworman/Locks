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
		private Sunfish.Views.Sprite GearLeft;
		private Sunfish.Views.Sprite GearRight;
		private int WorldNumber;
		private static float GearRotationRadians = (float) (Math.PI / 4d);

		public delegate void OnLockButtonPushDelegate (Models.LockButtonPushResult pushResult);

		public OnLockButtonPushDelegate OnLockButtonPush;

		public delegate void OnDialRotateCompleteDelegate (Lock lockWhoseDialRotated);

		public OnDialRotateCompleteDelegate OnDialRotateComplete;

		public Lock (Models.Lock lockModel, int worldNumber, bool isFirstRow, bool isLastRow, bool isFirstCol, bool isLastCol) :
		base(LoadLockBackground(worldNumber), new Vector2(0,0), Sunfish.Constants.ViewLayer.Layer2, Sunfish.Constants.ViewContainerLayout.Absolute)
		{

			WorldNumber = worldNumber;
			LockModel = lockModel;

			CreateGearsAndPipes (isFirstRow, isLastRow, isFirstCol, isLastCol);

			Texture2D facePlateTexture = LocksGame.ActiveScreen.LoadTexture ("LockFaceplate_" + (worldNumber + 1).ToString ());
			Sunfish.Views.Sprite faceplate = new Sunfish.Views.Sprite (facePlateTexture, Sunfish.Constants.ViewLayer.Layer3);
			AddChild (faceplate);

			Layout = Sunfish.Constants.ViewContainerLayout.FloatLeft;

			CreateLockIndicatorDial ();
			CreateLockButtons ();

		}

		private void CreateGearsAndPipes (bool isFirstRow, bool isLastRow, bool isFirstCol, bool isLastCol)
		{

			float halfWidth = (float)Width * 0.5f;
			float halfHeight = (float)Height * 0.5f;

			string worldNumberForTexture = (WorldNumber + 1).ToString ();

			// Gears
			Texture2D gearTexture = LocksGame.ActiveScreen.LoadTexture ("Gear_" + worldNumberForTexture);
			Vector2 gearLeftPosition = new Vector2 ((halfWidth - ((float)gearTexture.Width) * 0.5f) - PixelsWithDensity (20), PixelsWithDensity (35));
			Vector2 gearRightPosition = new Vector2 ((halfWidth - ((float)gearTexture.Width) * 0.5f) + PixelsWithDensity (20), PixelsWithDensity (35));
			GearLeft = new Sunfish.Views.Sprite (gearTexture, gearLeftPosition, Sunfish.Constants.ViewLayer.Layer2);
			GearRight = new Sunfish.Views.Sprite (gearTexture, gearRightPosition, Sunfish.Constants.ViewLayer.Layer2);
			AddChild (GearLeft);
			AddChild (GearRight);

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
			dialContainer.AddChild (LockIndicator, 0, PixelsWithDensity (4));
			dialContainer.AddChild (Dial, 0, PixelsWithDensity (5) - PixelsWithDensity(63));
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

		private void CreateLockButtons ()
		{
			LockButtonViewsDictionary = new Dictionary<int, Sunfish.Views.Switch> ();

			string worldNumberForTexture = (WorldNumber + 1).ToString ();

			for (int buttonIndex=0; buttonIndex < LockModel.ButtonCount; buttonIndex++) {
				Models.LockButton lockButtonModel = LockModel.Buttons [buttonIndex];
				Sunfish.Views.Switch lockButtonView = null;
				if (lockButtonModel.IsOn) {
					lockButtonView = Sunfish.Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("RotateCCWButton_" + worldNumberForTexture), LocksGame.ActiveScreen.LoadTexture ("RotateCWButton_" + worldNumberForTexture), Sunfish.Constants.ViewLayer.Layer4, HandleButtonTap);
				} else {
					lockButtonView = Sunfish.Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("RotateCCWButton_" + worldNumberForTexture), LocksGame.ActiveScreen.LoadTexture ("RotateCWButton_" + worldNumberForTexture), Sunfish.Constants.ViewLayer.Layer4, HandleButtonTap);
				}
				lockButtonView.Data = lockButtonModel;
				int marginY = 0;
				if (buttonIndex > 1) {
					marginY = PixelsWithDensity (10);
				}
				AddChild (lockButtonView, PixelsWithDensity (10), marginY);
				LockButtonViewsDictionary.Add (buttonIndex, lockButtonView);
			} 

		}

		public static Sunfish.Views.Sprite CreateDial (int currentPosition)
		{
			Sunfish.Views.Sprite dial = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("LockDial"), Sunfish.Constants.ViewLayer.Layer4);
			if (currentPosition != 0) {
				dial.RotationRadians = PositionsToRadians (currentPosition);
			}
			return dial;
		}

		public static Texture2D LoadLockBackground (int worldNumber)
		{
			return LocksGame.ActiveScreen.LoadTexture ("LockBackground_" + (worldNumber + 1).ToString());
		}

		private void HandleButtonTap (Sunfish.Views.View buttonThatWasTapped)
		{

			// Obtain and update the lock button model
			Models.LockButton lockButton = (Models.LockButton)buttonThatWasTapped.Data;
			int previousPosition = lockButton.ContainingLock.CurrentPosition;
			Models.LockButtonPushResult pushResult = lockButton.Push ();

			// Start the effects in the UI
			RotateDial (lockButton.ContainingLock.CurrentPosition - previousPosition);
			LocksGame.ActiveScreen.PlaySoundEffect ("LockDialTurning");
			PulsateLockButton (buttonThatWasTapped);
			RotateGearsForLockButton (lockButton);

			// Notify the handler of the button push
			if (OnLockButtonPush != null) {
				OnLockButtonPush (pushResult);
			}

		}

		public void SwitchAndPulsateLockButton (int buttonIndex)
		{
			Sunfish.Views.Switch lockButtonView = null;
			if (LockButtonViewsDictionary.TryGetValue (buttonIndex, out lockButtonView)) {

				// Pulsate the lock button
				lockButtonView.Toggle ();
				PulsateLockButton (lockButtonView);
				Models.LockButton lockButton = (Models.LockButton)lockButtonView.Data;
				RotateGearsForLockButton (lockButton);

			}
		}

		private void PulsateLockButton(Sunfish.Views.View lockButtonView)
		{
			lockButtonView.StartEffect (new Sunfish.Views.Effects.Pulsate (4000, 1, Color.White));
		}

		public void RotateDial (int positions)
		{
			float initialRadians = Dial.RotationRadians;
			float finalRadians = Dial.RotationRadians + PositionsToRadians (positions);
			Sunfish.Views.Effects.Rotate rotateEffect = new Sunfish.Views.Effects.Rotate (initialRadians, finalRadians, 300);
			rotateEffect.OnComplete = HandleRotateDialComplete;
			Dial.StartEffect (rotateEffect);
		}

		public void RotateGearsForLockButton (Models.LockButton lockButton)
		{
			if (lockButton.IsOn) {
				GearLeft.StartEffect (new Sunfish.Views.Effects.Rotate (GearLeft.RotationRadians, GearLeft.RotationRadians + GearRotationRadians, 300d));
				GearRight.StartEffect (new Sunfish.Views.Effects.Rotate (GearRight.RotationRadians, GearRight.RotationRadians - GearRotationRadians, 300d));
			} else {
				GearLeft.StartEffect (new Sunfish.Views.Effects.Rotate (GearLeft.RotationRadians, GearLeft.RotationRadians - GearRotationRadians, 300d));
				GearRight.StartEffect (new Sunfish.Views.Effects.Rotate (GearRight.RotationRadians, GearRight.RotationRadians + GearRotationRadians, 300d));
			}
			LocksGame.ActiveScreen.PlaySoundEffect ("GearTurning", 0.6f);
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

