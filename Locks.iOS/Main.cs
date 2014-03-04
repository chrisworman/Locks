using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Locks.iOS
{
	[Register("AppDelegate")]
	class Program : UIApplicationDelegate
	{
		LocksGame game;

		public override void FinishedLaunching (UIApplication app)
		{
			game = new LocksGame ();
			game.Run ();
			Locks.iOS.Views.BannerAd.Initialize ();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}


