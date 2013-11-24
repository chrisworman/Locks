using System;
using Sunfish;

namespace Locks.iOS.Views
{
	public class SettingsPopup : Sunfish.Views.Popup
	{

		Sunfish.Views.Switch MusicCheckBox;
		Sunfish.Views.Switch SoundEffectsCheckBox;

		public SettingsPopup ():
			base(LocksGame.ActiveScreen.LoadTexture ("PopupBackground"), Sunfish.Constants.ViewContainerLayout.StackCentered)
		{

			Sunfish.Views.Sprite soundEffectsLabel = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("SoundEffectsLabel"), Sunfish.Constants.ViewLayer.Modal);
			if (LocksGame.SoundEffectsOn) {
				SoundEffectsCheckBox = Sunfish.Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleSoundEffectsCheckboxTapped);
			} else {
				SoundEffectsCheckBox = Sunfish.Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleSoundEffectsCheckboxTapped);
			}
			Sunfish.Views.Container soundEffectsContainer = new Sunfish.Views.Container (soundEffectsLabel.Width + SoundEffectsCheckBox.Width, soundEffectsLabel.Height, Sunfish.Constants.ViewLayer.Modal, Sunfish.Constants.ViewContainerLayout.FloatLeft);
			soundEffectsContainer.AddChild (SoundEffectsCheckBox);
			soundEffectsContainer.AddChild (soundEffectsLabel);

			Sunfish.Views.Sprite musicLabel = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("MusicLabel"), Sunfish.Constants.ViewLayer.Modal);
			if (LocksGame.MusicOn) {
				MusicCheckBox = Sunfish.Views.Switch.CreateOn (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleMusicCheckboxTapped);
			} else {
				MusicCheckBox = Sunfish.Views.Switch.CreateOff (LocksGame.ActiveScreen.LoadTexture ("CheckBoxOn"), LocksGame.ActiveScreen.LoadTexture ("CheckBoxOff"), Sunfish.Constants.ViewLayer.Modal, HandleMusicCheckboxTapped);
			}
			Sunfish.Views.Container musicContainer = new Sunfish.Views.Container (musicLabel.Width + MusicCheckBox.Width, musicLabel.Height, Sunfish.Constants.ViewLayer.Modal, Sunfish.Constants.ViewContainerLayout.FloatLeft);
			musicContainer.AddChild (MusicCheckBox);
			musicContainer.AddChild (musicLabel);

			Sunfish.Views.Sprite closeButton = new Sunfish.Views.Sprite (LocksGame.ActiveScreen.LoadTexture ("CloseButton"), Sunfish.Constants.ViewLayer.Modal);
			closeButton.EnableTapGesture (HandleCloseButtonTap);

			AddChild (soundEffectsContainer, 0, PixelsWithDensity(80));
			AddChild (musicContainer, 0, PixelsWithDensity(15));
			AddChild (closeButton, 0, PixelsWithDensity(50));

		}

		public override void Show ()
		{
			MusicCheckBox.SetIsOn (LocksGame.MusicOn, false);
			SoundEffectsCheckBox.SetIsOn (LocksGame.SoundEffectsOn, false);
			base.Show ();
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

