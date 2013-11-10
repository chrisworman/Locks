using System;

namespace Models {

	public class Level {

		public int WorldNumber { get; set; }

		public int LevelNumber { get; set; }

		public LockGrid LockGrid { get; set; }

		public Level (int worldNumber, int levelNumber) {
			WorldNumber = worldNumber;
			LevelNumber = levelNumber;
		}

	}
}

