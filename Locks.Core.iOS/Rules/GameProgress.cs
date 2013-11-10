using System;
using Locks.Core;
using System.IO;
using Utilities;
using Locks.Core;

namespace Rules {

	public static class GameProgress {

		/// <summary>
		/// Loads all saved game progress, and returns the results in a an array indexed by 'slot'.
		/// </summary>
		/// <returns>The game progress.</returns>
		public static Models.GameProgress[] LoadGameProgress() {

			Models.GameProgress[] savedGameProgress = new Models.GameProgress[Constants.SlotsCount];

			for (int slot=0; slot < savedGameProgress.Length; slot++) {
				string savedGameProgressFilePath = GetSavedGameProgressFilePath (slot);
				if (File.Exists (savedGameProgressFilePath)) {
					string serializedGameProgress = File.ReadAllText (savedGameProgressFilePath);
					savedGameProgress [slot] = Deserialize (serializedGameProgress);
				}
			}

			return savedGameProgress;

		}

		public static void SaveGameProgress(Models.GameProgress gameProgress) {
			string serializedGameProgress = Serialize (gameProgress);
			string savedGameProgressFilePath = GetSavedGameProgressFilePath (gameProgress.Slot);
			File.WriteAllText (savedGameProgressFilePath, serializedGameProgress);
		}

		public static string Serialize(Models.GameProgress gameProgress) {

			Csv csv = new Csv ();

			csv.Append (gameProgress.Slot);
			for (int world=0; world < Constants.WorldCount; world++) {
				for (int level=0; level < Constants.WorldLevelCount; level++) {
					Models.SolvedLevel solvedLevel = gameProgress.GetSolvedLevel (world, level);
					if (solvedLevel == null) {
						break; // Levels must be solved in order
					} else {
						csv.Append (solvedLevel.WorldNumber);
						csv.Append (solvedLevel.LevelNumber);
						csv.Append (solvedLevel.Moves);
						csv.Append (solvedLevel.Stars);
					}
				}
			}

			return csv.ToString ();

		}

		public static Models.GameProgress Deserialize(string serializedGameProgress) {

			Csv csv = new Csv (serializedGameProgress);
			int[] savedGameData = csv.ToIntArray ();
			int dataIndex = 0;

			Models.GameProgress gameProgress = new Models.GameProgress (savedGameData [dataIndex++]);

			while (dataIndex < savedGameData.Length) {
				int worldNumber = savedGameData [dataIndex++];
				int levelNumber = savedGameData [dataIndex++];
				int moves = savedGameData [dataIndex++];
				int stars = savedGameData [dataIndex++];
				Models.SolvedLevel solvedLevel = new Models.SolvedLevel (worldNumber, levelNumber, moves, stars);
				gameProgress.AddSolvedLevel (solvedLevel);
			}

			return gameProgress;

		}

		private static string GetSavedGameProgressFilePath(int slot) {
			string documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			string fileName = String.Format ("Slot{0}.txt", slot.ToString ());
			return Path.Combine (documents, fileName);
		}

	}
}

