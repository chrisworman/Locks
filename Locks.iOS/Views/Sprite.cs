using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Locks.iOS.Views {

	/// <summary>
	/// A texture-based sprite.
	/// </summary>
	public class Sprite : View {

		public Texture2D Texture { get; set; }

		protected Rectangle SourceRectangle { get; set; }

		public Sprite (Texture2D texture) :
			this(texture, new Vector2(0,0), Constants.ViewLayer.Layer1) {
		}

		public Sprite (Texture2D texture, Vector2 position) :
			this(texture, position, Constants.ViewLayer.Layer1) {
		}

		public Sprite (Texture2D texture, Constants.ViewLayer layer) :
		this(texture, new Vector2(0,0), layer) {
		}

		public Sprite (Texture2D texture, Vector2 position, Constants.ViewLayer layer) :
		base(position, texture.Width, texture.Height, layer) {
			Texture = texture;
			SourceRectangle = new Rectangle(0, 0, Width, Height);
		}

		public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics){
			DrawTexture (Texture, SourceRectangle);
			//DrawBoundingBox (graphics);
		}
	
	}
}

