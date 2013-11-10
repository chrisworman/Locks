using System;
using System.IO;
using Locks.Core;

namespace Rules {

	public static class Level {

		public static Models.Level ReadLevel(int worldNumber, int levelNumber) {
			Models.Level result = new Models.Level (worldNumber, levelNumber);
			string levelFilePath = GetLevelFilePath (worldNumber, levelNumber);
			if (File.Exists (levelFilePath)) {
				string serializedLevel = File.ReadAllText (levelFilePath);
				result.LockGrid = Rules.LockGrid.Deserialize (serializedLevel);
			}
			return result;
		}

		private static string GetLevelFilePath(int worldNumber, int levelNumber) {
			return String.Format ("{0}/World{1}Level{2}.txt", Constants.LevelsFolder, worldNumber.ToString (), levelNumber.ToString ());
		}

	}
}

