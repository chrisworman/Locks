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
			: this(LocksGame.ActiveScreen.LoadTexture ("GameCenterIcon"), Sunfish.Constants.ViewLayer.Layer5)
		{
		}

		private GameCenterButton (Texture2D buttonTexture, Sunfish.Constants.ViewLayer layer) :
		base (buttonTexture, Vector2.Zero, layer)
		{
			EnableTapGesture (HandleGameCenterButtonTap);
			Visible = true;
			PositionInTopRight ();

		}

		public void PositionInTopRight() {
			ViewPositioner.ScreenTopRight (this, PixelsWithDensity (10), PixelsWithDensity (10));
		}

		private void HandleGameCenterButtonTap (View gameCenterButton) {
			GameCenter.ShowLeaderBoardAndAchievements ();
		}

	}
}

