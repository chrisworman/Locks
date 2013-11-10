using System;

namespace Models {

	public class SolvedLevel {

		public int WorldNumber { get; set; }

		public int LevelNumber { get; set; }

		public int Moves { get; set; }

		public int Stars { get; set; }

		public SolvedLevel () {
		}

		public SolvedLevel (int worldNumber, int levelNumber, int moves, int stars) {
			WorldNumber = worldNumber;
			LevelNumber = levelNumber;
			Moves = moves;
			Stars = stars;
		}

	}
}

