using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Locks.iOS.Screens
{
	public abstract class Screen
	{

//		public LocksGame CurrentGame { get; set; }
//
//		public Color BackgroundColor { get; set; }
//
//		public static Color DefaultBackgroundColor = Color.Black;
//
//		public Views.ViewLayers ScreenLayers { get; set; }
//
//		public ContentManager ScreenContent { get; set; }
//
//		public Views.Container TopBar { get; private set; }
//
//		public delegate void OnTransitionedOutDelegate (Screen screenThatTransitionedOut);
//
//		public OnTransitionedOutDelegate OnTransitionedOut;
//
//		private Rectangle SavedScissorRectangle { get; set; }
//
//		private RasterizerState SavedRasterizedState { get; set; }
//
//		protected Screen (LocksGame currentGame) :
//		this(currentGame, DefaultBackgroundColor)
//		{
//		}
//
//		protected Screen (LocksGame currentGame, Color backgroundColor)
//		{
//			ScreenContent = currentGame.CreateContentManager ();
//			ScreenLayers = new Views.ViewLayers ();
//			CurrentGame = currentGame;
//			BackgroundColor = backgroundColor;
//		}
//
//		public abstract void PopulateScreenViews ();
//
//		public void Update (GameTime gameTime)
//		{
//			ScreenLayers.Update (gameTime);
//		}
//
//		public void Draw (GameTime gameTime, GraphicsDeviceManager graphics)
//		{
//			graphics.GraphicsDevice.Clear (BackgroundColor);
//
//			LocksGame.ActiveSpriteBatch.Begin ();
//			ScreenLayers.Draw (gameTime, graphics);
//			LocksGame.ActiveSpriteBatch.End ();
//
////			BeginClipping (new Rectangle (0, 0, 1000, 600));
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("Depth1"), new Vector2 (130, 130), null, null, null, 0f, null, null, SpriteEffects.None, 1f);
////			EndClipping ();
////			BeginClipping (new Rectangle (0, 0, 1000, 600));
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("DepthPoint5"), new Vector2 (90, 90), null, null, null, 0f, null, null, SpriteEffects.None, 0.5f);
////			EndClipping ();
////			BeginClipping (new Rectangle (0, 0, 1000, 600));
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("Depth0"), new Vector2 (50, 50), null, null, null, 0f, null, null, SpriteEffects.None, 0f);
////			EndClipping ();
//
////			BeginClipping (new Rectangle (100, 100, 100, 100));
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("PopupBackground"), new Vector2(50, 50), Color.White);
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("RetryButton"), new Vector2(90, 110), Color.White);
////			EndClipping ();
////
////			BeginClipping (new Rectangle (500, 100, 100, 100));
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("PopupBackground"), new Vector2(400, 50), Color.White);
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("RetryButton"), new Vector2(490, 110), Color.White);
////			EndClipping ();
//
////			RasterizerState savedRasterizedState = LocksGame.ActiveSpriteBatch.GraphicsDevice.RasterizerState;
////			Rectangle savedScissorRectangle = LocksGame.ActiveSpriteBatch.GraphicsDevice.ScissorRectangle;
////			RasterizerState clipState = new RasterizerState() { ScissorTestEnable = true };
////			LocksGame.ActiveSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, clipState);
////			LocksGame.ActiveSpriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle(100,100,100,100);
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("PopupBackground"), new Vector2(50, 50), Color.White);
////			LocksGame.ActiveSpriteBatch.GraphicsDevice.ScissorRectangle = savedScissorRectangle;
////			LocksGame.ActiveSpriteBatch.GraphicsDevice.RasterizerState = savedRasterizedState;
////			LocksGame.ActiveSpriteBatch.Draw (LoadTexture ("PopupBackground"), new Vector2(500, 50), Color.White);
////			LocksGame.ActiveSpriteBatch.End ();
//		}
//
////		public void BeginClipping(Rectangle clippingRectangle)
////		{
////			//SavedScissorRectangle = LocksGame.ActiveSpriteBatch.GraphicsDevice.ScissorRectangle;
////			//SavedRasterizedState = LocksGame.ActiveSpriteBatch.GraphicsDevice.RasterizerState;
////			RasterizerState clippingState = new RasterizerState() { ScissorTestEnable = true };
////			LocksGame.ActiveSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, clippingState);
////			LocksGame.ActiveSpriteBatch.GraphicsDevice.ScissorRectangle = clippingRectangle;
////		}
////
////		public void EndClipping()
////		{
////			LocksGame.ActiveSpriteBatch.End ();
////		}
//
////		public void RestoreDefaultGraphicsDeviceState()
////		{
////			if (SavedRasterizedState != null) {
////				LocksGame.ActiveSpriteBatch.GraphicsDevice.ScissorRectangle = SavedScissorRectangle;
////				LocksGame.ActiveSpriteBatch.GraphicsDevice.RasterizerState = SavedRasterizedState;
////			}
////		}
//
////		private void DrawTestLine()
////		{
////			Vector2 begin = new Vector2 (10, 10);
////			Vector2 end = new Vector2 (500, 500);
////			int width = 3;
////			Color color = Color.DarkCyan;
////			Texture2D pixel = LoadTexture ("WhitePixel");
////			Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length()+width, width);
////			Vector2 v = Vector2.Normalize(begin - end);
////			float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
////			if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
////			LocksGame.ActiveSpriteBatch.Draw(pixel, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
////		}
//
//		public Views.Popup AddPopup (Constants.ViewContainerLayout childPosition)
//		{
//			Views.Popup newPopup = new Locks.iOS.Views.Popup (childPosition);
//			ScreenLayers.Add (newPopup);
//			return newPopup;
//		}
//
//		public void CreateTopBar ()
//		{
//			if (TopBar == null) {
//				Texture2D topBarBackground = LoadTexture ("TopBarBackground");
//				TopBar = new Views.Container (topBarBackground, Constants.ViewContainerLayout.FloatLeft);
//				ScreenLayers.Add (TopBar);
//			}
//		}
//
//		public Texture2D LoadTexture (string imageFileNameWithoutExtension)
//		{
//			if (LocksGame.IsiPad ()) {
//				return ScreenContent.Load<Texture2D> (Constants.IPadImageContentFolder + imageFileNameWithoutExtension);
//			} else {
//				return ScreenContent.Load<Texture2D> (Constants.IPadRetinaImageContentFolder + imageFileNameWithoutExtension);
//			}
//		}
//
//		public SoundEffectInstance LoadSoundEffect (string audioFileNameWithoutExtension)
//		{
//			return ScreenContent.Load<SoundEffect> (Constants.AudioContentFolder + audioFileNameWithoutExtension).CreateInstance ();
//		}
//
//		public void PlaySoundEffect (string audioFileNameWithoutExtension, float volume = 1f)
//		{
//			if (LocksGame.SoundEffectsOn) {
//				SoundEffectInstance soundEffectInstance = LoadSoundEffect (audioFileNameWithoutExtension);
//				soundEffectInstance.Volume = volume;
//				soundEffectInstance.Play ();
//			}
//		}
//
//		public SpriteFont LoadFont (string fontFileNameWithoutExtension)
//		{
//			return ScreenContent.Load<SpriteFont>(Constants.FontContentFolder + fontFileNameWithoutExtension);
//		}
//
//		public void TransitionIn ()
//		{
//			Views.Overlay overlay = Views.Overlay.CreateOpaque (this);
//			ScreenLayers.Add (overlay);
//			overlay.Disappear ();
//		}
//
//		public void TransitionOut ()
//		{
//			Views.Overlay overlay = Views.Overlay.CreateOpaque (this);
//			overlay.OnAppear = this.HandleTransitionedOut; 
//			ScreenLayers.Add (overlay);
//			overlay.Appear ();
//		}
//
//		private void HandleTransitionedOut ()
//		{
//			if (OnTransitionedOut != null) {
//				OnTransitionedOut (this);
//			}
//		}
//
//		public void UnloadContent ()
//		{
//			ScreenContent.Unload ();
//			ScreenContent.Dispose ();
//		}
//
//		public int PixelsWithDensity(int pixels)
//		{
//			return LocksGame.PixelsWithDensity (pixels);
//		}

	}
}

