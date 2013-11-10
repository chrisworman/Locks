using System;
using Locks.Core;

namespace Models {

	public class GameProgress {

		public int Slot { get; set; }

		private SolvedLevel[,] SolvedLevels { get; set; }

		public GameProgress (int slot) {
			Slot = slot;
			SolvedLevels = new SolvedLevel[Constants.WorldCount, Constants.WorldLevelCount];
		}

		public SolvedLevel GetSolvedLevel(int worldNumber, int levelNumber) {
			return SolvedLevels[worldNumber, levelNumber];
		}

		public void AddSolvedLevel(SolvedLevel solvedLevel) {
			SolvedLevels [solvedLevel.WorldNumber, solvedLevel.LevelNumber] = solvedLevel;
		}

		public bool IsWorldUnlocked(int worldNumber) {
			if (worldNumber == 0) {
				return true;
			} else {
				return (SolvedLevels [worldNumber - 1, Constants.WorldLevelCount - 1]) != null;
			}
		}

	}
}

