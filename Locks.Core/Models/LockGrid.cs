using System;

namespace Models {

	public class LockGrid {

		/// <summary>
		/// The locks contained in this grid.
		/// </summary>
		/// <value>The locks.</value>
		public Lock[,] Locks { get; set; }

		/// <summary>
		/// The number of colums of locks.
		/// </summary>
		/// <value>The col count.</value>
		public int ColCount { get; private set; }

		/// <summary>
		/// The number of rows of locks.
		/// </summary>
		/// <value>The row count.</value>
		public int RowCount { get; private set; }

		/// <summary>
		/// The number of buttons per lock in this grid.
		/// </summary>
		/// <value>The button count.</value>
		public int ButtonCount { get; private set; }

		public int MaxMovesFor3Stars { get; set; }

		public int MaxMovesFor2Stars { get; set; }

		/// <summary>
		/// Create a new lock grid of the specified dimensions, with each lock having the specified number of buttons.
		/// Initializes each lock as Unlinked in the 'Off' state.
		/// </summary>
		/// <param name="rowCount">Row count.</param>
		/// <param name="colCount">Col count.</param>
		/// <param name="buttonCount">Button count.</param>
		public LockGrid (int rowCount, int colCount, int buttonCount, int maxMovesFor2Stars, int maxMovesFor3Stars) {
			RowCount = rowCount;
			ColCount = colCount;
			ButtonCount = buttonCount;
			MaxMovesFor2Stars = maxMovesFor2Stars;
			MaxMovesFor3Stars = maxMovesFor3Stars;
			Locks = new Lock[rowCount,colCount];
			for (int row=0; row<rowCount; row++) {
				for (int col=0; col<colCount; col++) {
					Locks [row, col] = new Lock (row, col, buttonCount, 0);
				}
			}
		}

		public bool IsSolved() {
			for (int row=0; row<RowCount; row++) {
				for (int col=0; col<ColCount; col++) {
					Lock childLock = Locks [row, col];
					if (childLock.IsLocked ()) {
						return false;
					}
				}
			}
			return true;
		}

		public int CountUnlocked() {
			int count = 0;
			for (int row=0; row<RowCount; row++) {
				for (int col=0; col<ColCount; col++) {
					Lock childLock = Locks [row, col];
					if (childLock.IsUnlocked()) {
						count++;
					}
				}
			}
			return count;
		}

		public Models.LockButton GetButtonAt(int row, int col, int index){
			return Locks[row, col].Buttons[index];
		}

		public int GetStars (int moves)
		{
			if (moves <= MaxMovesFor3Stars) {
				return 3;
			} else if (moves <= MaxMovesFor2Stars) {
				return 2;
			} else {
				return 1;
			}
		}

	}
}

