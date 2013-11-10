using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Locks.iOS.Views
{
	public class AnimatedSprite : Sprite
	{

		protected Rectangle FrameRectangle { get; set; }

		protected int FrameCount { get; set; }

		protected double MillisecondsPerFrame { get; set; }

		protected double ElapsedTimeMilliseconds { get; set; }

		protected int CurrentFrame { get; set; }

		public AnimatedSprite (Texture2D texture, Rectangle frameRectangle, int frameCount, double millisecondsPerFrame) :
			this(texture, new Vector2(0,0), Constants.ViewLayer.Layer1, frameRectangle, frameCount, millisecondsPerFrame) {
		}

		public AnimatedSprite (Texture2D texture, Vector2 position, Rectangle frameRectangle, int frameCount, double millisecondsPerFrame) :
		this(texture, position, Constants.ViewLayer.Layer1, frameRectangle, frameCount, millisecondsPerFrame) {
		}

		public AnimatedSprite (Texture2D texture, Constants.ViewLayer layer, Rectangle frameRectangle, int frameCount, double millisecondsPerFrame) :
		this(texture, new Vector2(0,0), layer, frameRectangle, frameCount, millisecondsPerFrame) {
		}

		public AnimatedSprite (Texture2D texture, Vector2 position, Constants.ViewLayer layer, Rectangle frameRectangle, int frameCount, double millisecondsPerFrame) :
			base(texture, position, layer) {
			FrameRectangle = frameRectangle;
			Width = FrameRectangle.Width;
			Height = FrameRectangle.Height;
			FrameCount = frameCount;
			MillisecondsPerFrame = millisecondsPerFrame;
			ElapsedTimeMilliseconds = 0d;
			CurrentFrame = 0;
			SourceRectangle = FrameRectangle;
		}

		public override void Update (GameTime gameTime)
		{

			base.Update (gameTime);
			ElapsedTimeMilliseconds += gameTime.ElapsedGameTime.TotalMilliseconds;

			if (ElapsedTimeMilliseconds > MillisecondsPerFrame) {
				CurrentFrame++;
				CurrentFrame = CurrentFrame % FrameCount;
				ElapsedTimeMilliseconds -= MillisecondsPerFrame;
				SourceRectangle = new Rectangle (FrameRectangle.Width * CurrentFrame, FrameRectangle.Y, FrameRectangle.Width, FrameRectangle.Height);
			}

		}

	}
}

