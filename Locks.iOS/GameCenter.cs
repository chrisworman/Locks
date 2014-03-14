using System;
using System.Collections.Generic;
using MonoTouch.GameKit;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Locks.iOS
{
	/// <summary>
	/// A wrapper for Apple's game center that incorporates assumptions for the Locks game.  The wrapper deals with synchronization and
	/// duplicate achievement reporting so that consumers can simply detect and report achievements without being bothered with
	/// the underlying details.
	/// </summary>
	public static class GameCenter
	{

		#region View Controller Delegates

		private class LeaderBoardDelegate : GKGameCenterControllerDelegate {
			public override void Finished (GKGameCenterViewController controller) {
				controller.DismissViewController (true, null);
			}
		}

		private class AchievementDelegate : GKAchievementViewControllerDelegate {
			public override void DidFinish (GKAchievementViewController viewController) {
				viewController.DismissViewController (true, null);
			}
		}

		#endregion

		/// <summary>
		/// A map containing all known achievements that the player has earned.
		/// </summary>
		private static Dictionary<string, bool> KnownAchievements;

		/// <summary>
		/// An identifier for the currently logged in game center player.  May be null if the current user is not logged into game center.
		/// </summary>
		private static string CurrentPlayerId = null;

		/// <summary>
		/// Initialize game center as required for the game.
		/// </summary>
		public static void Initialize() {

			// Initially there is no player with no achievements
			CurrentPlayerId = null;
			KnownAchievements = new Dictionary<string, bool> ();

			// Set the authenticate handler for game center, which may trigger the user to login right away, but will 
			// also set the handler for subsequent authentication-related events.
			GKLocalPlayer.LocalPlayer.AuthenticateHandler = LocalPlayerAuthenticateHandler;

		}

		/// <summary>
		/// Called each time the players authentication status changes.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		/// <param name="error">Error.</param>
		private static void LocalPlayerAuthenticateHandler(UIViewController viewController, NSError error) {

			if (error == null) {

				// If a login view controller is provided, pause the game and show it to the user so that they can login
				if (viewController != null) {

					UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController (viewController, true, null);

				} else if (GKLocalPlayer.LocalPlayer != null && GKLocalPlayer.LocalPlayer.Authenticated) { // Authenticated player?

					// Is this a different player than who has been playing up until now?  If there is no current player
					// then assume they just weren't logged in until now.
					if (CurrentPlayerId != null && CurrentPlayerId != GKLocalPlayer.LocalPlayer.PlayerID) {
						KnownAchievements = new Dictionary<string, bool> (); // A new player has no known achievements at this point
					}

					CurrentPlayerId = GKLocalPlayer.LocalPlayer.PlayerID; // Remember this player

					//SynchronizeAchievements (); 
					GKAchievement.ResetAchivements ((resetError) => {
					});

				} else {

					CurrentPlayerId = null; // No current player I guess

				}

			}

		}

		/// <summary>
		/// Synchronize the locally stored known achievements with those stored in game center
		/// </summary>
		private static void SynchronizeAchievements() {

			if (GKLocalPlayer.LocalPlayer != null && GKLocalPlayer.LocalPlayer.Authenticated) {

				GKAchievement.LoadAchievements ((achievements, error) => {

					if (error == null && achievements != null) {

						// Create a dictionary of achievements that are in game center for quick lookup in the next step
						Dictionary<string,bool> gameCenterAchievements = new Dictionary<string, bool> ();
						foreach (GKAchievement achievement in achievements) {
							gameCenterAchievements.Add (achievement.Identifier, true);
						}

						// Build a list of known achievements that have not yet been reported to game center
						List<GKAchievement> unreportedAchievements = new List<GKAchievement> ();
						foreach (string knownAchievementIdentifier in KnownAchievements.Keys) {
							if (!gameCenterAchievements.ContainsKey (knownAchievementIdentifier)) {
								unreportedAchievements.Add (CreateCompleteAchievement (knownAchievementIdentifier, false));
							}
						}

						// Report the unreported achievements, if any
						if (unreportedAchievements.Count > 0) {
							GKAchievement.ReportAchievements (unreportedAchievements.ToArray (), (achievementError) => {
							});
						}

						// Update the known achievements dictionary to prevent re-reporting
						foreach (GKAchievement achievement in achievements) {
							if (!KnownAchievements.ContainsKey (achievement.Identifier)) {
								KnownAchievements.Add (achievement.Identifier, true);
							}
						}

					}

				});

			}

		}

		/// <summary>
		/// Records a world complete achievement for the specified world number (0..2).
		/// </summary>
		/// <param name="worldNumber">World number.</param>
		public static void RecordWorldCompleteAchievement(int worldNumber) {
			string worldCompleteAchievementId = "worldComplete." + worldNumber.ToString ();
			RecordAchievement (worldCompleteAchievementId);
		}

		/// <summary>
		/// Record that the achievement with the specified id was earned.
		/// </summary>
		/// <param name="achievementId">Achievement identifier.</param>
		public static void RecordAchievement(string achievementId) {

			// Is this achievement already in the dictionary?  If not, it needs to be recorded.
			if (!KnownAchievements.ContainsKey (achievementId)) {

				// Remember that this achievement has been earned ... this may need to be synchronized with game center later
				// if the user is logged into game center right now (see SynchronizeAchievements).
				KnownAchievements.Add (achievementId, true);

				// Immediately record with game center if possible
				if (IsGamePlayerAuthenticated()) {
					GKAchievement newAchievement = CreateCompleteAchievement (achievementId, true);
					GKAchievement[] newAchievementArray = new GKAchievement[1];
					newAchievementArray [0] = newAchievement;
					GKAchievement.ReportAchievements (newAchievementArray, (achievementError) => {
						if (achievementError != null) {
							Console.WriteLine("Error reporting achievement: " + achievementError.ToString());
						} else {
							Console.WriteLine("Reported achievement " + achievementId);
						}
					});
				}

			}

		}

		/// <summary>
		/// Create a game center achievement object with the specified identifier.
		/// </summary>
		/// <returns>The complete achievement.</returns>
		/// <param name="achievementId">Achievement identifier.</param>
		/// <param name="showBannerAfterReporting">If set to <c>true</c> show banner after reporting.</param>
		private static GKAchievement CreateCompleteAchievement(string achievementId, bool showBannerAfterReporting) {
			GKAchievement newAchievement = new GKAchievement (achievementId);
			newAchievement.PlayerID = CurrentPlayerId;
			newAchievement.PercentComplete = 100d;
			newAchievement.ShowsCompletionBanner = showBannerAfterReporting;
			return newAchievement;
		}

		/// <summary>
		/// Shows the leader board and achievments modal popup.
		/// </summary>
		public static void ShowLeaderBoardAndAchievements() {

			if (GKLocalPlayer.LocalPlayer.Authenticated) {
				GKGameCenterViewController gameCenterViewController = new GKGameCenterViewController ();
				gameCenterViewController.ViewState = GKGameCenterViewControllerState.Achievements;
				gameCenterViewController.Delegate = new LeaderBoardDelegate ();
				UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController (gameCenterViewController, true, null);
			}
		}

		/// <summary>
		/// Determines if is game player authenticated.
		/// </summary>
		/// <returns><c>true</c> if is game player authenticated; otherwise, <c>false</c>.</returns>
		public static bool IsGamePlayerAuthenticated() {
			return (GKLocalPlayer.LocalPlayer != null && GKLocalPlayer.LocalPlayer.Authenticated && CurrentPlayerId != null);
		}

	}
}

