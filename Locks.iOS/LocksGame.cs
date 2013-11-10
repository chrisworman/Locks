using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Sunfish;

namespace Locks.iOS
{
	/// <summary>
	/// The Locks game.
	/// </summary>
	public class LocksGame : SunfishGame
	{

		protected override Screen GetHomeScreen ()
		{
			return new Screens.Home (this);
		}

		protected override DisplayOrientation GetDisplayOrientation ()
		{
			return DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
		}

		#region Fields
//		public static GraphicsDeviceManager Graphics { get; set; }
//
//		public static Screens.Screen ActiveScreen { get; set; }
//
//		public static SpriteBatch ActiveSpriteBatch { get; set; }
//
		public static Models.GameProgress GameProgress { get; set; }
//
//		public static int ScreenWidth;
//		public static int ScreenHeight;
//		private Screens.Screen NextScreen;
//		public static bool SoundEffectsOn = true;
//		private static bool _MusicOn = false;
//
//		public static bool MusicOn {
//		
//			get {
//				return _MusicOn;
//			}
//
//			set {
//				_MusicOn = value;
//				if (_MusicOn && (GameSong.State == SoundState.Paused || GameSong.State == SoundState.Stopped)) {
//					GameSong.Resume ();
//				} else if (!_MusicOn && GameSong.State == SoundState.Playing) {
//					GameSong.Pause ();
//				}
//			}
//
//		}

		private static SoundEffectInstance GameSong = null;
		#endregion
		#region Initialization
		public LocksGame () : base()
		{
//			Graphics = new GraphicsDeviceManager (this);
//			Content.RootDirectory = Constants.ContentFolder;
//			Graphics.IsFullScreen = true;
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
//		protected override void Initialize ()
//		{
//
//			base.Initialize ();
//
//			ScreenWidth = Graphics.GraphicsDevice.Viewport.Height;
//			ScreenHeight = Graphics.GraphicsDevice.Viewport.Width;
//
//			TouchPanel.EnabledGestures = GestureType.Tap;
//			ActiveSpriteBatch = new SpriteBatch (Graphics.GraphicsDevice);
//			ShowHomeScreen ();
//
//		}

//		public ContentManager CreateContentManager ()
//		{
//			ContentManager newManager = new ContentManager (Content.ServiceProvider);
//			newManager.RootDirectory = Constants.ContentFolder;
//			return newManager;
//		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent ()
		{

			GameSong = Content.Load<SoundEffect> (Constants.AudioContentFolder + "LevelSong").CreateInstance ();
			GameSong.Volume = 0.3f;
			GameSong.IsLooped = true;
			MusicOn = true;
		
		}

		//		private void TryLoadingFont(string fontPath) {
		//			SpriteFont testFont = null;
		//			try {
		//				testFont = Content.Load<SpriteFont>(fontPath);
		//			} catch (Exception ex) {
		//				Console.WriteLine (string.Format ("{0} failed: {1}", fontPath, ex.Message));
		//			}
		//			if (testFont != null) {
		//				Console.WriteLine (string.Format ("{0} SUCCEEDED!", fontPath));
		//			}
		//		}

		#endregion

//		public static int PixelsWithDensity(int pixels)
//		{
//			if (IsiPad()) {
//				return pixels;
//			} else {
//				return pixels * 2;
//			}
//		}
//
//		public static bool IsiPad()
//		{
//			return ScreenWidth == 768;
//		}
//
//		public static bool IsiPadRetina()
//		{
//			return !IsiPad();
//		}

		#region Screen Navigation
//		public void ShowHomeScreen ()
//		{
//			SetActiveScreen (new Screens.Home(this));
//		}
//
//		public void ShowLevelChooserScreen ()
//		{
//			SetActiveScreen (new Screens.LevelChooser (this));
//		}
//
//		public void ShowLevelScreen (int worldNumber, int levelNumber)
//		{
//			SetActiveScreen (new Screens.Level (this, worldNumber, levelNumber));
//		}

//		private void SetActiveScreen (Screens.Screen newScreen)
//		{
//			NextScreen = newScreen;
//			if (ActiveScreen == null) {
//				PopulateAndTransitionInNextScreen ();
//			} else {
//				ActiveScreen.OnTransitionedOut = HandleActiveScreenTransitionedOut;
//				ActiveScreen.TransitionOut ();
//			}
//		}
//
//		private void HandleActiveScreenTransitionedOut (Screens.Screen screen)
//		{
//			ActiveScreen.UnloadContent ();
//			PopulateAndTransitionInNextScreen ();
//		}
//
//		private void PopulateAndTransitionInNextScreen ()
//		{
//			ActiveScreen = NextScreen;
//			NextScreen.PopulateScreenViews ();
//			NextScreen.TransitionIn ();
//		}
		#endregion
		#region Update and Draw
//		/// <summary>
//		/// Allows the game to run logic such as updating the world,
//		/// checking for collisions, gathering input, and playing audio.
//		/// </summary>
//		/// <param name="gameTime">Provides a snapshot of timing values.</param>
//		protected override void Update (GameTime gameTime)
//		{
//			ActiveScreen.Update (gameTime);
//			base.Update (gameTime);
//		}
//
//		/// <summary>
//		/// This is called when the game should draw itself. 
//		/// </summary>
//		/// <param name="gameTime">Provides a snapshot of timing values.</param>
//		protected override void Draw (GameTime gameTime)
//		{
//			// Draw the current screen
//			if (ActiveScreen != null) {
//				ActiveScreen.Draw (gameTime, Graphics);
//			} else {
//				Graphics.GraphicsDevice.Clear (Color.Black);
//			}
//			base.Draw (gameTime);
//		}
		#endregion
	}
}
