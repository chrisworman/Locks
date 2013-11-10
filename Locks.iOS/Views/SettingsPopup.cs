using System;
using Sunfish;

namespace Locks.iOS.Views
{
	public class SettingsPopup : Sunfish.Views.Popup
	{
		public SettingsPopup ():
			base(LocksGame.ActiveScreen.LoadTexture ("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered)
		{

			Views.Sprite soundEffectsLabel = new Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("SoundEffectsLabel"), Constants.ViewLayer.Modal);
			Views.Switch soundEffectsCheckBox = null;
			if (LocksGame.SoundEffectsOn) {
				soundEffectsCheckBox = Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Constants.ViewLayer.Modal);
			} else {
				soundEffectsCheckBox = Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Constants.ViewLayer.Modal);
			}
			soundEffectsCheckBox.OnTap = HandleSoundEffectsCheckboxTapped;
			Views.Container soundEffectsContainer = new Views.Container (soundEffectsLabel.Width + soundEffectsCheckBox.Width, soundEffectsLabel.Height, Constants.ViewLayer.Modal, Constants.ViewContainerLayout.FloatLeft);
			soundEffectsContainer.AddChild (soundEffectsCheckBox);
			soundEffectsContainer.AddChild (soundEffectsLabel);

			Views.Sprite musicLabel = new Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("MusicLabel"), Constants.ViewLayer.Modal);
			Views.Switch musicCheckBox = null;
			if (LocksGame.MusicOn) {
				musicCheckBox = Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Constants.ViewLayer.Modal);
			} else {
				musicCheckBox = Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Constants.ViewLayer.Modal);
			}
			musicCheckBox.OnTap = HandleMusicCheckboxTapped;
			Views.Container musicContainer = new Views.Container (musicLabel.Width + musicCheckBox.Width, musicLabel.Height, Constants.ViewLayer.Modal, Constants.ViewContainerLayout.FloatLeft);
			musicContainer.AddChild (musicCheckBox);
			musicContainer.AddChild (musicLabel);

			Views.Button closeButton = new Views.Button (LocksGame.ActiveScreen.LoadTexture ("CloseButton"), Constants.ViewLayer.Modal);
			closeButton.OnTap = HandleCloseButtonTap;

			//AddChild (soundEffectsContainer, 0, PixelsWithDensity(80));
			//AddChild (musicContainer, 0, PixelsWithDensity(15));
			//AddChild (closeButton, 0, PixelsWithDensity(50));

		}

		private void HandleSoundEffectsCheckboxTapped(Button soundEffectsCheckBox)
		{
			LocksGame.SoundEffectsOn = !LocksGame.SoundEffectsOn;
		}

		private void HandleMusicCheckboxTapped(Button soundEffectsCheckBox)
		{
			LocksGame.MusicOn = !LocksGame.MusicOn;
		}

		private void HandleCloseButtonTap (Button spriteThatWasTapped)
		{
			Hide ();
		}

	}
}

