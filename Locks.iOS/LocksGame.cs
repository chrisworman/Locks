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

		public static Models.GameProgress GameProgress { get; set; }

		protected override Screen GetHomeScreen ()
		{
			return new Screens.Home (this);
		}

		protected override DisplayOrientation GetDisplayOrientation ()
		{
			return DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
		}

		protected override void Initialize ()
		{
			base.Initialize ();
			SetAndStartGameSong ("LevelSong", 0.4f);
		}

		public static Sunfish.Views.Font GetTopBarFont()
		{
			if (IsiPad()) {
				//return new Sunfish.Views.Font ("AmericanTypewriter24");
				return new Sunfish.Views.Font ("Rave24");
			} else {
				//return new Sunfish.Views.Font ("AmericanTypewriter48");
				return new Sunfish.Views.Font ("Rave48");
			}
		}

	}
}
