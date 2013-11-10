using System;
using Microsoft.Xna.Framework.Graphics;

namespace Locks.iOS.Views
{
	public class Stars : Sunfish.Views.Sprite
	{
		public static Stars Create (int stars, Sunfish.Constants.ViewLayer layer)
		{
			Texture2D starsTexture = LocksGame.ActiveScreen.LoadTexture (GetStarsTextureFileName (stars));
			return new Stars (starsTexture, layer);
		}

		private Stars (Texture2D starsTexture, Sunfish.Constants.ViewLayer layer) :
			base(starsTexture, layer)
		{
		}

		public void SetStars (int stars)
		{
			Texture = LocksGame.ActiveScreen.LoadTexture (GetStarsTextureFileName (stars));
		}

		private static string GetStarsTextureFileName (int stars)
		{
			return String.Format ("Stars{0}", stars.ToString ());
		}
	}
}

