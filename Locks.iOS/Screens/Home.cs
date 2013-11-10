using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Locks.Core;
using Sunfish;

namespace Locks.iOS.Screens
{
	public class Home : Sunfish.Screen
	{
		private Views.SettingsPopup SettingsPopup;
		private Sunfish.Views.Popup CreditsPopup;
		private Sunfish.Views.Popup PlayPopup { get; set; }

		public Home (LocksGame currentGame) : base(currentGame, Color.BurlyWood)
		{
		}

		public override void PopulateScreenViews ()
		{
			CreateAndPopulateTopBar ();
			CreatePlayButtonAndPopup ();
		}

		private void CreateAndPopulateTopBar ()
		{

			Sunfish.Views.Sprite settingsButton = new Sunfish.Views.Sprite (LoadTexture ("SettingsButton"));
			settingsButton.EnableTapGesture (HandleSettingsButtonTap);
			SettingsPopup = new Views.SettingsPopup ();
			AddChildView (SettingsPopup);

			Sunfish.Views.Sprite creditsButton = new Sunfish.Views.Sprite (LoadTexture ("CreditsButton"));
			creditsButton.EnableDoubleTapGesture (HandleCreditsButtonTap);
			CreateCreditsPopup ();

//			Views.Button backButton = new Views.Button (LoadTexture ("BackButton"));
//			backButton.OnTap = StopParticleSystems;

			CreateTopBar ();
			TopBar.AddChild (settingsButton, PixelsWithDensity (10), PixelsWithDensity (10));
			TopBar.AddChild (creditsButton, PixelsWithDensity (10), PixelsWithDensity (10));
//			TopBar.AddChild (backButton, PixelsWithDensity(10), PixelsWithDensity(10));

		}
		//		private void StopParticleSystems(Views.Button backButton)
		//		{
		//			Smoke.Stop ();
		//		}
		private void CreatePlayButtonAndPopup ()
		{

			Sunfish.Views.Sprite startButton = new Sunfish.Views.Sprite (LoadTexture ("StartButton"));
			startButton.CenterInScreen ();
			startButton.EnableTapGesture (HandleStartButtonTap);
			AddChildView (startButton);

			PlayPopup = AddPopup (LoadTexture ("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered);

			Models.GameProgress[] gameProgress = Rules.GameProgress.LoadGameProgress ();
			for (int slot=0; slot < Core.Constants.SlotsCount; slot++) {
				Models.GameProgress savedGame = gameProgress [slot];
				Views.PlaySlotButton playButton = null;
				if (savedGame == null) {
					playButton = Views.PlaySlotButton.CreateNewGame (slot);
				} else {
					playButton = Views.PlaySlotButton.CreateContinueGame (slot);
				}
				playButton.EnableTapGesture (HandleGameButtonTap);
				PlayPopup.AddChild (playButton, 0, (slot == 0) ? PixelsWithDensity (80) : PixelsWithDensity (30));
			}

		}

		private void CreateCreditsPopup ()
		{

			Sunfish.Views.Sprite closeButton = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("CloseButton"), Sunfish.Constants.ViewLayer.Modal);
			closeButton.EnableTapGesture (HandleCreditsPopupCloseButtonTap);

			CreditsPopup = AddPopup (LoadTexture ("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered);
			CreditsPopup.AddChild (closeButton, 0, PixelsWithDensity (100));

		}

		private void HandleSettingsButtonTap (Sunfish.Views.View settingsButton)
		{
			SettingsPopup.Show ();
		}

		private void HandleCreditsButtonTap (Sunfish.Views.View creditsButton)
		{
			CreditsPopup.Show ();
		}

		private void HandleCreditsPopupCloseButtonTap (Sunfish.Views.View closeButton)
		{
			CreditsPopup.Hide ();
		}

		private void HandleStartButtonTap (Sunfish.Views.View startButton)
		{
			PlayPopup.Show ();
		}

		private void HandleGameButtonTap (Sunfish.Views.View playButton)
		{
			Views.PlaySlotButton playSlotButton = (Views.PlaySlotButton)playButton;
			Models.GameProgress[] gameProgress = Rules.GameProgress.LoadGameProgress ();
			LocksGame.GameProgress = gameProgress [playSlotButton.Slot];
			if (LocksGame.GameProgress == null) { // initialize a new game progress for new games
				LocksGame.GameProgress = new Models.GameProgress (playSlotButton.Slot);
			}
			PlayPopup.Hide ();
			CurrentGame.SetActiveScreen (new Screens.LevelChooser (CurrentGame));
		}
	}
}

