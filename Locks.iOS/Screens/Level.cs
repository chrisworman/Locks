using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Locks.iOS.Screens
{
	public class Level : Sunfish.Screen
	{
		private Sunfish.Views.Popup PausedPopup { get; set; }

		private Sunfish.Views.Popup SolvedPopup { get; set; }

		private int WorldNumber { get; set; }

		private int LevelNumber { get; set; }

		private Models.Level Model { get; set; }

		private int Moves { get; set; }

		private Sunfish.Views.Label MovesLabel { get; set; }

		private Sunfish.Views.Label LockedCountLabel { get; set; }

		private Dictionary<string, Views.Lock> LockViewsDictionary { get; set; }

		private Views.SettingsPopup SettingsPopup;

		private Views.Stars StarsView { get; set; }

		public Level (Sunfish.SunfishGame currentGame, int worldNumber, int levelNumber) :
		base(currentGame, Color.DarkOliveGreen)
		{
			WorldNumber = worldNumber;
			LevelNumber = levelNumber;
			Model = Rules.Level.ReadLevel (WorldNumber, LevelNumber);
			LockViewsDictionary = new Dictionary<string, Views.Lock> ();
		}

		public override void PopulateScreenViews ()
		{
			CreateAndPopulateTopBar ();
			CreateLevelGrid (TopBar.Height);
			CreatePausedPopup ();
			CreateSolvedPopup ();
			CreateAndShowTutorialPopupIfNecessary ();
		}

		private void CreateAndPopulateTopBar ()
		{

			Sunfish.Views.Sprite pauseButton = new Sunfish.Views.Sprite (LoadTexture ("PauseButton"));
			pauseButton.EnableTapGesture(HandlePauseButtonTapped);

			Sunfish.Views.Sprite settingsButton = new Sunfish.Views.Sprite (LoadTexture ("SettingsButton"));
			settingsButton.EnableTapGesture(HandleSettingsButtonTapped);

			//MovesLabel = new Views.Label ("", LoadFont ("CDub"), Color.Black);
			//UpdateMovesLabel ();

			//LockedCountLabel = new Views.Label ("", LoadFont ("CDub"), Color.Black);
			//UpdateLockCountLabel ();

			//Views.Label levelLabel = new Views.Label ("World " + WorldNumber.ToString () + " Level " + LevelNumber.ToString (), LoadFont ("CDub"), Color.Black);

			SettingsPopup = new Views.SettingsPopup ();
			//ScreenLayers.Add (SettingsPopup);

			CreateTopBar ();
			TopBar.AddChild (pauseButton, PixelsWithDensity (10), PixelsWithDensity (10));
			TopBar.AddChild (settingsButton, PixelsWithDensity (10), PixelsWithDensity (10));
			//TopBar.AddChild (MovesLabel, PixelsWithDensity (150), PixelsWithDensity (30));
			//TopBar.AddChild (LockedCountLabel, PixelsWithDensity (80), PixelsWithDensity (30));
			//TopBar.AddChild (levelLabel, PixelsWithDensity (80), PixelsWithDensity (30));

		}

		private void CreateLevelGrid (int yOffset)
		{
		
			Sunfish.Views.Container lockContainer = CreateLevelGridContainer ();
			int rowCount = Model.LockGrid.RowCount;
			int colCount = Model.LockGrid.ColCount;
			for (int row=0; row<rowCount; row++) {
				for (int col=0; col<colCount; col++) {
					Models.Lock lockModel = Model.LockGrid.Locks [row, col];
					Views.Lock lockView = new Views.Lock (lockModel, row == 0, row == (rowCount - 1), col == 0, col == (colCount - 1));
					lockView.OnLockButtonPush = HandleLockButtonPush;
					lockContainer.AddChild (lockView, (col == 0) ? 0 : PixelsWithDensity (18), PixelsWithDensity (18));
					LockViewsDictionary.Add (lockModel.GetRowColString (), lockView);
				}
			}

			// Center the locks in a play area
			if (Model.LockGrid.RowCount < 3) {
				yOffset += PixelsWithDensity (50);
			}
			Sunfish.Views.Container locksPlayArea = new Sunfish.Views.Container (LocksGame.ScreenHeight, LocksGame.ScreenWidth, new Vector2 (PixelsWithDensity (9), yOffset), Sunfish.Constants.ViewContainerLayout.StackCentered);
			locksPlayArea.AddChild (lockContainer);

			AddChildView (locksPlayArea);

		}

		private void CreatePausedPopup ()
		{

			PausedPopup = AddPopup (LoadTexture("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered);
			Sunfish.Views.Sprite resumeButton = new Sunfish.Views.Sprite (LoadTexture ("ResumeGameButton"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite restartButton = new Sunfish.Views.Sprite (LoadTexture ("RestartGameButton"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Sprite quitButton = new Sunfish.Views.Sprite (LoadTexture ("QuitGameButton"), Sunfish.Constants.ViewLayer.Modal);

			resumeButton.EnableTapGesture(HandleResumeButtonTapped);
			restartButton.EnableTapGesture(HandleRestartButtonTapped);
			quitButton.EnableTapGesture(HandleQuitButtonFromPausedPopupTapped);

			PausedPopup.AddChild (resumeButton, 0, PixelsWithDensity (80));
			PausedPopup.AddChild (restartButton, 0, PixelsWithDensity (30));
			PausedPopup.AddChild (quitButton, 0, PixelsWithDensity (30));

		}

		private void CreateSolvedPopup ()
		{

			StarsView = Views.Stars.Create (0, Sunfish.Constants.ViewLayer.Modal);

			Sunfish.Views.Sprite nextLevelButton = null;
			if (WorldNumber != Core.Constants.WorldCount - 1 || LevelNumber != Core.Constants.WorldLevelCount - 1) {
				nextLevelButton = new Sunfish.Views.Sprite (LoadTexture ("NextLevelButton"), Sunfish.Constants.ViewLayer.Modal);
				nextLevelButton.EnableTapGesture(HandleNextLevelButtonTapped);
			}

			Sunfish.Views.Sprite retryButton = new Sunfish.Views.Sprite (LoadTexture ("RetryButton"), Sunfish.Constants.ViewLayer.Modal);
			retryButton.EnableTapGesture(HandleRetryButtonTapped);

			Sunfish.Views.Sprite quitButton = new Sunfish.Views.Sprite (LoadTexture ("QuitGameButton"), Sunfish.Constants.ViewLayer.Modal);
			quitButton.EnableTapGesture(HandleQuitButtonFromSolvedPopupTapped);

			SolvedPopup = AddPopup (LoadTexture("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered);
			SolvedPopup.OnShown = HandleSolvedPopupShown;
			SolvedPopup.AddChild (StarsView, 0, PixelsWithDensity (60));
			if (nextLevelButton != null) {
				SolvedPopup.AddChild (nextLevelButton, 0, PixelsWithDensity (30));
			}
			SolvedPopup.AddChild (retryButton, 0, PixelsWithDensity (30));
			SolvedPopup.AddChild (quitButton, 0, PixelsWithDensity (30));

		}

		private void CreateAndShowTutorialPopupIfNecessary ()
		{
			if (LevelNumber == 0 && WorldNumber == 0 && LocksGame.GameProgress.GetSolvedLevel (LevelNumber, WorldNumber) == null) {
				Views.TutorialPopup tutorialPopup = new Views.TutorialPopup ();
				AddChildView (tutorialPopup);
				tutorialPopup.Show ();
			}
		}

		private Sunfish.Views.Container CreateLevelGridContainer ()
		{
			int lockWidthWithMargin = Views.Lock.LoadLockBackground ().Width + PixelsWithDensity (18);
			int width = Model.LockGrid.ColCount * lockWidthWithMargin;
			return new Sunfish.Views.Container (width, LocksGame.ScreenWidth, Sunfish.Constants.ViewContainerLayout.FloatLeft);
		}

		private void HandleLockButtonPush (Models.LockButtonPushResult pushResult)
		{

			Moves++;
			UpdateMovesLabel ();

			if (pushResult.LinkedButton != null) {
				Views.Lock lockView = null;
				if (LockViewsDictionary.TryGetValue (pushResult.LinkedButton.ContainingLock.GetRowColString (), out lockView)) {
					lockView.SwitchLockButton (pushResult.LinkedButton.ContainingLockIndex);
					lockView.OnDialRotateComplete = HandleLockDialRotateComplete;
					lockView.RotateDial (pushResult.GetLinkedButtonContainingLockPositionDelta ());
				}
			}

		}

		private void HandleLockDialRotateComplete (Views.Lock lockWhoseDialRotated)
		{

			UpdateLockCountLabel ();
		
			lockWhoseDialRotated.OnDialRotateComplete = null;
			if (Model.LockGrid.IsSolved ()) {
				int stars = Model.LockGrid.GetStars (Moves);
				Models.SolvedLevel solvedLevel = LocksGame.GameProgress.GetSolvedLevel (WorldNumber, LevelNumber);
				StarsView.SetStars (stars);
				if (solvedLevel == null || Moves < solvedLevel.Moves) {
					solvedLevel = new Models.SolvedLevel (WorldNumber, LevelNumber, Moves, stars);
					LocksGame.GameProgress.AddSolvedLevel (solvedLevel);
					Rules.GameProgress.SaveGameProgress (LocksGame.GameProgress);
				}
				LocksGame.ActiveScreen.PlaySoundEffect ("Unlocked");
				SolvedPopup.Show ();

			}

		}

		private void UpdateMovesLabel ()
		{
//			if (Moves == 1) {
//				MovesLabel.Text = Moves.ToString () + " Turn";
//			} else {
//				MovesLabel.Text = Moves.ToString () + " Turns";
//			}
		}

		private void UpdateLockCountLabel ()
		{
			//LockedCountLabel.Text = Model.LockGrid.CountUnlocked ().ToString () + " of " + (Model.LockGrid.ColCount * Model.LockGrid.RowCount).ToString () + " Unlocked";
		}

		private void HandleSolvedPopupShown (Sunfish.Views.Popup popupThatIsNowShown)
		{
			LocksGame.ActiveScreen.PlaySoundEffect ("LevelSuccess");
		}

		private void HandlePauseButtonTapped (Sunfish.Views.View pauseButton)
		{
			PauseGame ();
		}

		private void HandleSettingsButtonTapped (Sunfish.Views.View settingsButton)
		{
			SettingsPopup.Show ();
		}

		private void HandleResumeButtonTapped (Sunfish.Views.View pauseButton)
		{
			ResumeGame ();
		}

		private void HandleRestartButtonTapped (Sunfish.Views.View pauseButton)
		{
			PausedPopup.Hide ();
			RetryLevel ();
		}

		private void HandleQuitButtonFromPausedPopupTapped (Sunfish.Views.View pauseButton)
		{
			PausedPopup.Hide ();
			QuitGame ();
		}

		private void HandleQuitButtonFromSolvedPopupTapped (Sunfish.Views.View pauseButton)
		{
			SolvedPopup.Hide ();
			QuitGame ();
		}

		private void HandleRetryButtonTapped (Sunfish.Views.View retryButton)
		{
			SolvedPopup.Hide ();
			RetryLevel ();
		}

		private void HandleNextLevelButtonTapped (Sunfish.Views.View nextLevelButton)
		{
//			if (LevelNumber == Core.Constants.WorldLevelCount - 1) {
//				CurrentGame.ShowLevelScreen (WorldNumber + 1, 0);
//			} else {
//				CurrentGame.ShowLevelScreen (WorldNumber, LevelNumber + 1);
//			}
			SolvedPopup.Hide ();
		}

		private void PauseGame ()
		{
			PausedPopup.Show ();
		}

		private void ResumeGame ()
		{
			PausedPopup.Hide ();
		}

		private void RetryLevel ()
		{
			//CurrentGame.ShowLevelScreen (WorldNumber, LevelNumber);
		}

		private void QuitGame ()
		{
			//CurrentGame.ShowLevelChooserScreen ();
		}
	}
}

