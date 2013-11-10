using System;
using Sunfish;

namespace Locks.iOS.Views
{
	public class SettingsPopup : Sunfish.Views.Popup
	{
		public SettingsPopup ():
			base(LocksGame.ActiveScreen.LoadTexture ("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered)
		{

			Sunfish.Views.Sprite soundEffectsLabel = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("SoundEffectsLabel"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Switch soundEffectsCheckBox = null;
			if (LocksGame.SoundEffectsOn) {
				soundEffectsCheckBox = Sunfish.Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleSoundEffectsCheckboxTapped);
			} else {
				soundEffectsCheckBox = Sunfish.Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleSoundEffectsCheckboxTapped);
			}
			Sunfish.Views.Container soundEffectsContainer = new Sunfish.Views.Container (soundEffectsLabel.Width + soundEffectsCheckBox.Width, soundEffectsLabel.Height, Sunfish.Constants.ViewLayer.Modal, Sunfish.Constants.ViewContainerLayout.FloatLeft);
			soundEffectsContainer.AddChild (soundEffectsCheckBox);
			soundEffectsContainer.AddChild (soundEffectsLabel);

			Sunfish.Views.Sprite musicLabel = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("MusicLabel"), Sunfish.Constants.ViewLayer.Modal);
			Sunfish.Views.Switch musicCheckBox = null;
			if (LocksGame.MusicOn) {
				musicCheckBox = Sunfish.Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleMusicCheckboxTapped);
			} else {
				musicCheckBox = Sunfish.Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleMusicCheckboxTapped);
			}
			Sunfish.Views.Container musicContainer = new Sunfish.Views.Container (musicLabel.Width + musicCheckBox.Width, musicLabel.Height, Sunfish.Constants.ViewLayer.Modal, Sunfish.Constants.ViewContainerLayout.FloatLeft);
			musicContainer.AddChild (musicCheckBox);
			musicContainer.AddChild (musicLabel);

			Sunfish.Views.Sprite closeButton = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("CloseButton"), Sunfish.Constants.ViewLayer.Modal);
			closeButton.EnableTapGesture (HandleCloseButtonTap);

			AddChild (soundEffectsContainer, 0, PixelsWithDensity(80));
			AddChild (musicContainer, 0, PixelsWithDensity(15));
			AddChild (closeButton, 0, PixelsWithDensity(50));

		}

		private void HandleSoundEffectsCheckboxTapped (Sunfish.Views.View soundEffectsCheckBox)
		{
			LocksGame.SoundEffectsOn = !LocksGame.SoundEffectsOn;
		}

		private void HandleMusicCheckboxTapped (Sunfish.Views.View soundEffectsCheckBox)
		{
			LocksGame.MusicOn = !LocksGame.MusicOn;
		}

		private void HandleCloseButtonTap (Sunfish.Views.View closeButton)
		{
			Hide ();
		}
	}
}

