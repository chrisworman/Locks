using System;
using Locks.Core;
using Utilities;

namespace Rules {

	public static class LockGrid {

		/// <summary>
		/// A random number generator.
		/// </summary>
		private static Random randomGenerator = new Random();

		/// <summary>
		/// The maximum number of buttons to push when creating a random LockGrid: see CreateRandom.  The actual number
		/// of pushes perfomed by CreateRandom depends on the difficulty passed into the CreateRandom function.
		/// </summary>
		private const int RandomLockGridMaxButtonPushes = 100;


		/// <summary>
		/// Create a random LockGrid with the specified number of rows, columns, and buttons per lock.  The difficulty
		/// parameter determines the difficulty of the generated LockGrid, which should be between 0 and 1.0.
		/// </summary>
		/// <returns>The random.</returns>
		/// <param name="rowCount">The number of rows in the generated LockGrid.</param>
		/// <param name="colCount">The number of columns in the generated LockGrid.</param>
		/// <param name="buttonCount">The number of buttons per lock in the generated LockGrid.</param>
		/// <param name="difficulty">Difficulty value between 0 and 1.0.</param>
		public static Models.LockGrid CreateRandom(int rowCount, int colCount, int buttonCount, float difficulty) {

			Models.LockGrid randomLockGrid = new Models.LockGrid (rowCount, colCount, buttonCount, 5, 10);

			// Link up random buttons
			for (int row=0; row<rowCount; row++) {
				for (int col=0; col<colCount; col++) {

					Models.Lock currentLock = randomLockGrid.Locks [row, col];
					//int onCount = 0; // The number of buttons that are 'On' 
					for (int buttonIndex=0; buttonIndex<buttonCount; buttonIndex++) {

						Models.LockButton currentButton = currentLock.Buttons [buttonIndex];
						if (currentButton.IsUnlinked()) {
							Models.LockButton randomUnlinkedButton = GetRandomUnlinkedLockButton (randomLockGrid, currentLock);
							currentButton.LinkWithButton (randomUnlinkedButton);
							// Make the buttons 'On' state consistent
							currentButton.IsOn = GetRandomBool();
							randomUnlinkedButton.IsOn = !currentButton.IsOn;
						}

						//if (currentButton.IsOn) {
						//	onCount++;
						//}

					}

					// Set the position of the lock according to the buttons
					//int offCount = buttonCount - onCount;
					//int randomValidPosition =  randomGenerator.Next (onCount, Constants.LockPositions - offCount - 1);
					//currentLock.CurrentPosition = randomValidPosition;
					//currentLock.UnlockedPosition = currentLock.CurrentPosition;

				}
			}

			// Push random buttons
			int pushCount = Convert.ToInt32(RandomLockGridMaxButtonPushes * difficulty);
			for (int pushes=0; pushes<pushCount; pushes++){
				Models.LockButton randomButton = GetRandomLockButton (randomLockGrid);
				randomButton.Push ();
			}

			// Ensure the level is not solved
			while (randomLockGrid.IsSolved()) {
				Models.LockButton randomButton = GetRandomLockButton (randomLockGrid);
				randomButton.Push ();
			}

			return randomLockGrid;

		}

		/// <summary>
		/// Returns a random button from the specified LockGrid that 'IsUnLocked'.
		/// </summary>
		/// <returns>The random unlinked lock button.</returns>
		/// <param name="lockGrid">The lock grid to search for a button.</param>
		/// <param name="lockToAvoid">The lock that should not be returned.</param>
		public static Models.LockButton GetRandomUnlinkedLockButton(Models.LockGrid lockGrid, Models.Lock lockToAvoid) {
			int iterations = 0;
			Models.LockButton randomButton = null;
			do {
				randomButton = GetRandomLockButton(lockGrid);
				iterations++;
				if (iterations > 1000){
					throw new Exception();
				}
			} while (randomButton.IsLinked() || randomButton.ContainingLock == lockToAvoid);
			return randomButton;
		}

		/// <summary>
		/// Returns a random button from the specified LockGrid.
		/// </summary>
		/// <returns>The random lock button.</returns>
		/// <param name="lockGrid">The lock grid to search for a button.</param>
		public static Models.LockButton GetRandomLockButton(Models.LockGrid lockGrid) {
			int randomRow = randomGenerator.Next (0, lockGrid.RowCount);
			int randomCol = randomGenerator.Next (0, lockGrid.ColCount);
			Models.Lock randomLock = lockGrid.Locks [randomRow, randomCol];
			int randomButtonIndex = randomGenerator.Next (0, randomLock.ButtonCount);
			return randomLock.Buttons [randomButtonIndex];
		}

		/// <summary>
		/// Returns a random bool.
		/// </summary>
		/// <returns></returns>
		private static bool GetRandomBool() {
			return randomGenerator.NextDouble () > 0.5;
		}

		/// <summary>
		/// Generates a string representation of the specified LockGrid: see Deserialize.
		/// </summary>
		/// <param name="lockGrid">Lock grid.</param>
		public static string Serialize(Models.LockGrid lockGrid){

			Csv csv = new Csv ();

			// Serialize the lock grid properties
			csv.Append (lockGrid.RowCount);
			csv.Append (lockGrid.ColCount); 
			csv.Append (lockGrid.ButtonCount);
			csv.Append (lockGrid.MaxMovesFor2Stars);
			csv.Append (lockGrid.MaxMovesFor3Stars);

			// Serialize the locks and their buttons
			for (int row=0; row<lockGrid.RowCount; row++) {
				for (int col=0; col<lockGrid.ColCount; col++) {
					Models.Lock currentLock = lockGrid.Locks [row, col];
					// Serialize the lock properties
					//csv.Append (currentLock.UnlockedPosition);
					csv.Append (currentLock.CurrentPosition);
					// Serialize the buttons
					for (int buttonIndex=0; buttonIndex<currentLock.ButtonCount; buttonIndex++) {
						Models.LockButton button = currentLock.Buttons [buttonIndex];
						csv.Append (button.IsOn);
						csv.Append (button.LinkedButton.ContainingLockGridRow);
						csv.Append (button.LinkedButton.ContaingLockGridCol);
						csv.Append (button.LinkedButton.ContainingLockIndex);
					}
				}
			}

			return csv.ToString ();

		}

		/// <summary>
		/// Deserialize the specified serializedLockGrid: see Serialize.
		/// </summary>
		/// <param name="serializedLockGrid">Serialized lock grid.</param>
		public static Models.LockGrid Deserialize(string serializedLockGrid){

			Csv csv = new Csv (serializedLockGrid);
			int[] values = csv.ToIntArray ();
			int valueIndex = 0;

			// Deserialize the lock grid properties
			int rowCount = values [valueIndex++];
			int colCount = values [valueIndex++];
			int buttonCount = values [valueIndex++];
			int maxMovesFor2Stars = values [valueIndex++];
			int maxMovesFor3Stars = values [valueIndex++];
			Models.LockGrid lockGrid = new Models.LockGrid (rowCount, colCount, buttonCount, maxMovesFor2Stars, maxMovesFor3Stars);

			// Deserialize the locks and buttons
			for (int row=0; row<rowCount; row++) {
				for (int col=0; col<colCount; col++) {
					Models.Lock currentLock = lockGrid.Locks [row, col];
					// Deserialize the lock properties
					//currentLock.UnlockedPosition = values [valueIndex++];
					currentLock.CurrentPosition = values [valueIndex++];
					// Deserialize the buttons
					for (int buttonIndex=0; buttonIndex<buttonCount; buttonIndex++) {
						Models.LockButton button = currentLock.Buttons [buttonIndex];
						button.IsOn = (values [valueIndex++] == 1);
						int linkedButtonRow = values[valueIndex++];
						int linkedButtonCol = values [valueIndex++];
						int linkedButtonIndex = values [valueIndex++];
						if (button.IsUnlinked ()) {
							button.LinkWithButton (lockGrid.GetButtonAt (linkedButtonRow, linkedButtonCol, linkedButtonIndex));
						}
					}
				}
			}

			return lockGrid;
		}

	}
}

