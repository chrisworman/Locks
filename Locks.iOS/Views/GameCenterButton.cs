using System;
using Sunfish;
using Sunfish.Views;
using Sunfish.Views.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Locks.iOS.Views
{
	public class GameCenterButton : Sprite
	{

		public GameCenterButton()
			: this(LocksGame.ActiveScreen.LoadTexture ("GameCenterButton"), Sunfish.Constants.ViewLayer.Layer5)
		{
		}

		private GameCenterButton (Texture2D buttonTexture, Sunfish.Constants.ViewLayer layer) :
		base (buttonTexture, Vector2.Zero, layer)
		{
			GameCenter.OnPlayerStatusChanged = HandleGameCenterPlayerStatusChanged;
			if (GameCenter.IsGamePlayerAuthenticated ()) {
				EnableTapGesture (HandleGameCenterButtonTap);
				Visible = true;
			} else {
				Visible = false;
				DisableTapGestures ();
			}
		}

		private void HandleGameCenterButtonTap (View gameCenterButton) {
			GameCenter.ShowLeaderBoardAndAchievements ();
		}

		private void HandleGameCenterPlayerStatusChanged() {
			if (GameCenter.IsGamePlayerAuthenticated ()) {
				EnableTapGesture (HandleGameCenterButtonTap);
				StartEffect (new Appear (1500d));
			} else {
				Visible = false;
				DisableTapGestures ();
			}
		}

	}
}

