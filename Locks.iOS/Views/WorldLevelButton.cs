using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;

namespace Locks.iOS.Views
{
	public class WorldLevelButton : Sunfish.Views.Container
	{
		public int WorldNumber { get; private set; }

		public int LevelNumber { get; private set; }

		public static WorldLevelButton CreateSolved (int worldNumber, int levelNumber, int stars)
		{
			Texture2D buttonTexture = LocksGame.ActiveScreen.LoadTexture ("WorldLevelSolved");
			Sunfish.Views.Sprite buttonSprite = new Sunfish.Views.Sprite (buttonTexture, new Vector2 (0, 0), Sunfish.Constants.ViewLayer.Layer1);
			WorldLevelButton button = new WorldLevelButton (buttonSprite, Stars.Create (stars, Sunfish.Constants.ViewLayer.Layer1), worldNumber, levelNumber);
			return button;
		}

		public static WorldLevelButton CreateFirstUnsolved (int worldNumber, int levelNumber)
		{
			Texture2D buttonTexture = LocksGame.ActiveScreen.LoadTexture ("WorldLevelFirstUnsolved");
			Sunfish.Views.Sprite buttonSprite = new Sunfish.Views.Sprite (buttonTexture, new Vector2 (0, 0), Sunfish.Constants.ViewLayer.Layer1);
			WorldLevelButton button = new WorldLevelButton (buttonSprite, Stars.Create (0, Sunfish.Constants.ViewLayer.Layer1), worldNumber, levelNumber);
			return button;
		}

		public static WorldLevelButton CreateUnsolved (int worldNumber, int levelNumber)
		{
			Texture2D buttonTexture = LocksGame.ActiveScreen.LoadTexture ("WorldLevelUnsolved");
			Sunfish.Views.Sprite unsolvedSprite = new Sunfish.Views.Sprite (buttonTexture, new Vector2 (0, 0), Sunfish.Constants.ViewLayer.Layer1);
			return new WorldLevelButton (unsolvedSprite, Stars.Create (0, Sunfish.Constants.ViewLayer.Layer1), worldNumber, levelNumber);
		}

		private WorldLevelButton (Sunfish.Views.Sprite buttonSprite, Stars stars, int worldNumber, int levelNumber) :
		base(Math.Max(buttonSprite.Width, stars.Width), buttonSprite.Height + stars.Height, new Vector2(0,0), Sunfish.Constants.ViewLayer.Layer1, Sunfish.Constants.ViewContainerLayout.StackCentered)
		{
			WorldNumber = worldNumber;
			LevelNumber = levelNumber;
			AddChild (buttonSprite);
			AddChild (stars);
		}

	}
}

