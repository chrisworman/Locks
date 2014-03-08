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

		public Home (SunfishGame currentGame) : 
			base(currentGame, Color.DarkGray, "HomeScreenBackground")
		{
		}

		public override void PopulateScreenViews ()
		{
			CreateAndPopulateTopBar ();
			CreateStartButtonAndPopup ();
			Views.BannerAd.Show ();
		}

		private void CreateAndPopulateTopBar ()
		{

			Sunfish.Views.Sprite settingsButton = new Sunfish.Views.Sprite (LoadTexture ("SettingsButton"));
			settingsButton.EnableTapGesture (HandleSettingsButtonTap);
			SettingsPopup = new Views.SettingsPopup ();
			AddChildView (SettingsPopup);

			Sunfish.Views.Sprite creditsButton = new Sunfish.Views.Sprite (LoadTexture ("CreditsButton"));
			creditsButton.EnableTapGesture (HandleCreditsButtonTap);
			CreateCreditsPopup ();

			CreateTopBar ();
			TopBar.AddChild (settingsButton, PixelsWithDensity (10), PixelsWithDensity (10));
			TopBar.AddChild (creditsButton, PixelsWithDensity (10), PixelsWithDensity (10));

		}

		private void CreateStartButtonAndPopup ()
		{

			Sunfish.Views.Sprite startButtonGlow = new Sunfish.Views.Sprite (LoadTexture ("StartButtonGlow"), Sunfish.Constants.ViewLayer.Layer2);
			startButtonGlow.CenterInScreen ();
			startButtonGlow.OverlayColor = Color.TransparentBlack;
			startButtonGlow.StartEffect(new Sunfish.Views.Effects.InAndOut(10000d, 100, Color.White));
			AddChildView (startButtonGlow);

			Sunfish.Views.Sprite startButton = new Sunfish.Views.Sprite (LoadTexture ("StartButton"), Sunfish.Constants.ViewLayer.Layer3);
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

			// New game?
			if (LocksGame.GameProgress == null) { 
				LocksGame.GameProgress = new Models.GameProgress (playSlotButton.Slot);
			}

			PlayPopup.Hide ();
			CurrentGame.SetActiveScreen (new Screens.LevelChooser (CurrentGame));

		}
	}
}

