using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using GoogleAdMobAds;

namespace Locks.iOS.Views
{
	public class BannerAd
	{

		private static GADBannerView BannerAdView;

		private const string BannerAdUnitId = "ca-app-pub-7544936678327930/7085475207";

		private static bool BannerAdAlreadyAddedToWindow = false;

		private static bool HideBannerAd = false;

		private static bool BannerAdAvailable = false;

		public static void Initialize() {
			HideBannerAd = false; 
			BannerAdAvailable = false;
			InitializeBannerAd ();
		}

		private static void InitializeBannerAd () {

			UIWindow mainWindow = UIApplication.SharedApplication.KeyWindow;
			UIViewController rootViewController = mainWindow.RootViewController;
			UIView rootView = rootViewController.View;

			// Setup your GADBannerView, review GADAdSizeCons class for more Ad sizes. 
			float bannerAdHeight = 60f;
			BannerAdView = new GADBannerView (size: GADAdSizeCons.SmartBannerLandscape);
			BannerAdView.Hidden = true;
			BannerAdView.AdUnitID = BannerAdUnitId;
			BannerAdView.Frame = new RectangleF (0, rootView.Frame.Width - bannerAdHeight, rootView.Frame.Height, bannerAdHeight + 1);
			BannerAdView.RootViewController = rootViewController;

			// Wire DidReceiveAd event to know when the Ad is ready to be displayed
			BannerAdView.DidReceiveAd += (object sender, EventArgs e) => {
				if (!BannerAdAlreadyAddedToWindow) {
					rootView.AddSubview (BannerAdView);
					BannerAdAlreadyAddedToWindow = true;
				}
				BannerAdAvailable = true;
				BannerAdView.Hidden = HideBannerAd;
			};

			BannerAdView.DidFailToReceiveAd += (object sender, GADBannerViewErrorEventArgs e) => {
				BannerAdAvailable = false;
				Hide();
			};

			//GADRequest.Request.TestDevices = new string[] { "faa970547eb47b57b855e5ca9e3b8bc7" };
			BannerAdView.LoadRequest (GADRequest.Request);

		}

		public static void Show() {
			if (BannerAdAvailable) {
				SetBannerAdHidden (false);
			} else {
				HideBannerAd = false;
			}
		}

		public static void Hide() {
			SetBannerAdHidden (true);
		}

		private static void SetBannerAdHidden(bool hidden) {
			HideBannerAd = hidden;
			if (BannerAdView != null) {
				BannerAdView.Hidden = HideBannerAd;
			}
		}


	}
}

