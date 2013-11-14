using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Content;
using Locks.Core;

namespace Locks.iOS.Screens
{
	public class LevelChooser : Sunfish.Screen
	{

		private Views.SettingsPopup SettingsPopup;

		#region "Initialization"

		public LevelChooser (Sunfish.SunfishGame currentGame) : base(currentGame, Color.DarkSlateGray)
		{
		}

		public override void PopulateScreenViews ()
		{
			CreateAndPopulateTopBar ();
			CreateWorldsContainer ();
		}

		private void CreateAndPopulateTopBar ()
		{

			Sunfish.Views.Sprite backButton = new Sunfish.Views.Sprite (LoadTexture ("BackButton"));
			backButton.EnableTapGesture(HandleBackButtonTap);

			Sunfish.Views.Sprite settingsButton = new Sunfish.Views.Sprite (LoadTexture ("SettingsButton"));
			settingsButton.EnableTapGesture(HandleSettingsButtonTap);

			SettingsPopup = new Views.SettingsPopup ();
			AddChildView (SettingsPopup);

			Sunfish.Views.Label chooseALevelLabel = new Sunfish.Views.Label ("Choose a Level", LocksGame.GetTopBarFont(), Color.Black);

			CreateTopBar ();
			TopBar.AddChild (backButton, PixelsWithDensity(10), PixelsWithDensity(10));
			TopBar.AddChild (settingsButton, PixelsWithDensity(10), PixelsWithDensity(10));
			TopBar.AddChild (chooseALevelLabel, PixelsWithDensity(240), PixelsWithDensity(20));

		}

		private void CreateWorldsContainer ()
		{

			Sunfish.Views.Sprite world0Title = new Sunfish.Views.Sprite (LoadTexture ("World0Title"));
			Sunfish.Views.Sprite world1Title = new Sunfish.Views.Sprite (LoadTexture ("World1Title"));
			Sunfish.Views.Sprite world2Title = new Sunfish.Views.Sprite (LoadTexture ("World2Title"));

			Sunfish.Views.Container world0LevelsContainer = new Sunfish.Views.Container (LoadTexture ("WorldLevelsContainer_1"), Sunfish.Constants.ViewContainerLayout.FloatLeft);
			Sunfish.Views.Container world1LevelsContainer = new Sunfish.Views.Container (LoadTexture ("WorldLevelsContainer_2"), Sunfish.Constants.ViewContainerLayout.FloatLeft);
			Sunfish.Views.Container world2LevelsContainer = new Sunfish.Views.Container (LoadTexture ("WorldLevelsContainer_3"), Sunfish.Constants.ViewContainerLayout.FloatLeft);

			Sunfish.Views.Container world0Container = new Sunfish.Views.Container (Math.Max(world0Title.Width, world0LevelsContainer.Width), 0, Sunfish.Constants.ViewContainerLayout.StackCentered);
			world0Container.ShouldExpandHeight = true;
			Sunfish.Views.Container world1Container = new Sunfish.Views.Container (Math.Max(world1Title.Width, world1LevelsContainer.Width), 0, Sunfish.Constants.ViewContainerLayout.StackCentered);
			world1Container.ShouldExpandHeight = true;
			Sunfish.Views.Container world2Container = new Sunfish.Views.Container (Math.Max(world2Title.Width, world2LevelsContainer.Width), 0, Sunfish.Constants.ViewContainerLayout.StackCentered);
			world2Container.ShouldExpandHeight = true;

			PopulateWorldContainer (world0LevelsContainer, 0);
			PopulateWorldContainer (world1LevelsContainer, 1);
			PopulateWorldContainer (world2LevelsContainer, 2);

			world0Container.AddChild (world0Title);
			world0Container.AddChild (world0LevelsContainer, 0, PixelsWithDensity(20));
			world1Container.AddChild (world1Title);
			world1Container.AddChild (world1LevelsContainer, 0, PixelsWithDensity(20));
			world2Container.AddChild (world2Title);
			world2Container.AddChild (world2LevelsContainer, 0, PixelsWithDensity(20));

			Sunfish.Views.Container worldsContainer = new Sunfish.Views.Container (LocksGame.ScreenHeight, LocksGame.ScreenWidth, new Vector2 (0, PixelsWithDensity(160)), Sunfish.Constants.ViewContainerLayout.FloatLeft);
			worldsContainer.AddChild (world0Container, PixelsWithDensity(31), 0);
			worldsContainer.AddChild (world1Container, PixelsWithDensity(31), 0);
			worldsContainer.AddChild (world2Container, PixelsWithDensity(31), 0);

			AddChildView (worldsContainer);

		}

		private void PopulateWorldContainer (Sunfish.Views.Container worldContainer, int worldNumber)
		{

			bool worldIsUnlocked = LocksGame.GameProgress.IsWorldUnlocked (worldNumber);
			bool isFirstUnsolvedLevel = true;
			for (int levelNumber=0; levelNumber < 9; levelNumber++) {
				Models.SolvedLevel solvedLevel = LocksGame.GameProgress.GetSolvedLevel (worldNumber, levelNumber);
				Views.WorldLevelButton levelButton;
				if (solvedLevel == null) {
					if (isFirstUnsolvedLevel && worldIsUnlocked) {
						levelButton = Views.WorldLevelButton.CreateFirstUnsolved (worldNumber, levelNumber);
						levelButton.EnableTapGesture(HandleLevelButtonTap);
						isFirstUnsolvedLevel = false;
					} else {
						levelButton = Views.WorldLevelButton.CreateUnsolved (worldNumber, levelNumber);
					}
				} else {
					levelButton = Views.WorldLevelButton.CreateSolved (worldNumber, levelNumber, solvedLevel.Stars);
					levelButton.EnableTapGesture(HandleLevelButtonTap);
				}
				worldContainer.AddChild (levelButton, PixelsWithDensity(10), PixelsWithDensity(15));
			}
		}

		#endregion

		#region "Event handling"

		private void HandleSettingsButtonTap (Sunfish.Views.View settingsButton)
		{
			SettingsPopup.Show ();
		}

		private void HandleLevelButtonTap (Sunfish.Views.View levelButtonView)
		{
			PlaySoundEffect ("Unlocked");
			Views.WorldLevelButton levelButton = (Views.WorldLevelButton)levelButtonView;
			CurrentGame.SetActiveScreen (new Screens.Level (CurrentGame, levelButton.WorldNumber, levelButton.LevelNumber));
		}

		private void HandleBackButtonTap (Sunfish.Views.View backButton)
		{
			CurrentGame.SetActiveScreen (new Screens.Home (CurrentGame));
		}

		#endregion

	}
}

