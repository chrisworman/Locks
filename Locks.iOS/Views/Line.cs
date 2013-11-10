using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Locks.iOS.Views
{
	public class Line : View
	{

		public Vector2 Begin {
			get {
				return Position;
			}
			set {
				Position = value;
			}
		}

		public Vector2 End { get; set; }

		public int LineWidth { get; set; }

		private Texture2D WhitePixel;

		public override int Width {
			get {
				return (int) Math.Abs (Begin.X - End.X);
			}
			set {
			}
		}

		public override int Height {
			get {
				return (int) Math.Abs (Begin.Y - End.Y);
			}
			set {
			}
		}

		public Line (Vector2 begin, Vector2 end, int lineWidth, Color overlayColor) :
			base (Constants.ViewLayer.Layer1)
		{
			Begin = begin;
			End = end;
			LineWidth = lineWidth;
			OverlayColor = overlayColor;
			WhitePixel = LocksGame.ActiveScreen.LoadTexture ("WhitePixel");
		}

		public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics)
		{	
			Rectangle r = new Rectangle((int)Begin.X, (int)Begin.Y, (int)(End - Begin).Length()+LineWidth, LineWidth);
			Vector2 v = Vector2.Normalize(Begin - End);
			float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
			if (Begin.Y > End.Y) angle = MathHelper.TwoPi - angle;
			LocksGame.ActiveSpriteBatch.Draw(WhitePixel, r, null, OverlayColor, angle, Vector2.Zero, SpriteEffects.None, 0);
		}

	}
}

