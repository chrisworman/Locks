using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Locks.iOS.Views
{
	public abstract class View
	{

		public Object Data = null;

		private View ParentView { get; set; }

		private Vector2 _Position;

		public Vector2 Position { 
			get {
				if (ParentView == null) {
					return _Position;
				} else {
					return new Vector2 (_Position.X + ParentView.Position.X + Origin.X, _Position.Y + ParentView.Position.Y + Origin.Y);
				}
			}
			set {
				_Position = value;
			}
		}

		public float RotationRadians { get; set; }

		public Vector2 Origin { get; set; }

		public virtual int Width { get; set; }

		public virtual int Height { get; set; }

		private bool _Visible;

		public virtual bool Visible { 
			get {
				if (ParentView == null) {
					return _Visible;
				} else {
					return ParentView.Visible;
				}
			}
			set {
				if (ParentView == null) {
					_Visible = value;
				}
				// Ignore attempts to set the visibility if the ParentView is set: the visiblity should be set in the Parent
			}
		}

		private Constants.ViewLayer _Layer;

		public Constants.ViewLayer Layer { 
			get {
				//if (ParentView == null) {
					return _Layer;
				//} else {
				//	return ParentView.Layer;
				//}
			}
			set {
				//if (ParentView == null) {
					_Layer = value;
				//}
				// Ignore attempts to set the layer if the ParentView is set: the layer should be set in the Parent
			}
		}

		public Color OverlayColor; // { get; set; }

		public float Scale { get; set; }

		private List<Effects.Effect> Effects;

		protected View (Constants.ViewLayer layer, int width, int height, bool visible) : 
		this(new Vector2(0,0), width, height, layer, visible)
		{
		}

		protected View (Vector2 position, Constants.ViewLayer layer) : 
			this(position, 0, 0, layer)
		{
		}

		protected View (Constants.ViewLayer layer) : 
		this(new Vector2(0,0), 0, 0, layer)
		{
		}

		public View (Vector2 position, int width, int height, Constants.ViewLayer layer) :
		this(position, width, height, layer, true)
		{
		}

		public View (Vector2 position, int width, int height, Constants.ViewLayer layer, bool visible)
		{
			Position = position;
			Width = width;
			Height = height;
			Layer = layer;
			Visible = visible;
			ParentView = null; 
			OverlayColor = Color.White;
			Effects = new List<Effects.Effect> ();
			RotationRadians = 0f;
			//CenterOrigin ();
			Origin = new Vector2 (0, 0);
			Scale = 1.0f;
		}

		/// <summary>
		/// Set the parent view of this view, which will cause this view to use the Visible, Layer, and Position from to the parent.
		/// </summary>
		/// <param name="parentView">Parent view.</param>
		public void SetParent (View parentView)
		{
			ParentView = parentView;
		}

		public virtual bool ConsumeGesture (GestureSample gesture)
		{
			return false; 
		}

		public virtual void Update (GameTime gameTime)
		{
			Effects.ForEach (effect => effect.Update (gameTime, this));
			Effects.RemoveAll (effect => effect.IsComplete ());
		}

		public virtual Rectangle? GetClippingRectangle()
		{
			return null;
		}

		public abstract void Draw (GameTime gameTime, GraphicsDeviceManager graphics);

		public void StartEffect (Effects.Effect newEffect)
		{
			Effects.Add (newEffect);
		}

		protected void DrawTexture (Texture2D texture, Rectangle sourceRectangle)
		{
			if (Visible && texture != null) {
				//Rectangle? sourceRectangle = null;
				float depth = 0f;
				LocksGame.ActiveSpriteBatch.Draw (texture, Position, sourceRectangle, OverlayColor, RotationRadians, Origin, Scale, SpriteEffects.None, depth);
			}
		}

		protected void DrawTextureFullScreen (Texture2D texture)
		{
			if (Visible && texture != null) {
				LocksGame.ActiveSpriteBatch.Draw (texture, new Rectangle (0, 0, LocksGame.ScreenHeight, LocksGame.ScreenWidth), OverlayColor);
			}
		}

		protected void DrawString (string text, SpriteFont font, Color textColor)
		{
			if (Visible && !String.IsNullOrEmpty (text) && font != null) {
				float depth = 0f;
				Color textColorWithOverlay = new Color (textColor.R, textColor.G, textColor.B, OverlayColor.A);
				LocksGame.ActiveSpriteBatch.DrawString(font, text, Position, textColorWithOverlay, RotationRadians, Origin, Scale, SpriteEffects.None, depth);
			}
		}

		public void CenterInScreen ()
		{
			Position = new Vector2 (((float)LocksGame.ScreenHeight - (float)Width) / 2, ((float)LocksGame.ScreenWidth - (float)Height) / 2);
		}

		protected void DrawBoundingBox(GraphicsDeviceManager graphics)
		{
			Texture2D pixel = LocksGame.ActiveScreen.LoadTexture ("OverlayTransparentPixel");
			LocksGame.ActiveSpriteBatch.Draw(pixel, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.DarkGreen);
		}

		public int PixelsWithDensity(int pixels)
		{
			return LocksGame.PixelsWithDensity (pixels);
		}

		public void CenterOrigin()
		{
			Origin = new Vector2 (Width / 2f, Height / 2f);
		}

	}
}

