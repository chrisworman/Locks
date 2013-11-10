using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Sunfish;

namespace Locks.iOS.Views
{
	public class PlaySlotButton : Sunfish.Views.Sprite
	{
		public int Slot { get; private set; }

		public static PlaySlotButton CreateNewGame (int slot)
		{
			return new PlaySlotButton (LocksGame.ActiveScreen.LoadTexture ("NewGameButton"), slot, true);
		}

		public static PlaySlotButton CreateContinueGame (int slot)
		{
			return new PlaySlotButton (LocksGame.ActiveScreen.LoadTexture ("ContinueGameButton"), slot, false);
		}

		private PlaySlotButton (Texture2D buttonTexture, int slot, bool isNewGame) :
		base (buttonTexture, new Vector2(0,0), Sunfish.Constants.ViewLayer.Modal)
		{
			Slot = slot;
		}
	}
}

