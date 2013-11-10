using System;


namespace Models {

	/// <summary>
	/// A lock with one or more <see cref="Models.LockButton">LockButtons</see>
	/// </summary>
	public class Lock {

		public int GridRow { get; private set; }

		public int GridCol { get; private set; }

		/// <summary>
		/// Gets or sets the buttons on this lock.
		/// </summary>
		/// <value>The buttons.</value>
		public LockButton[] Buttons { get; set; }

		/// <summary>
		/// Gets the number of buttons on this lock.
		/// </summary>
		/// <value>The button count.</value>
		public int ButtonCount {
			get {
				return Buttons.Length;
			}
		}

		/// <summary>
		/// The current position of this lock.
		/// </summary>
		/// <value>The current position.</value>
		public int CurrentPosition { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Models.Lock"/> class with the specified number of buttons.
		/// </summary>
		/// <param name="buttonCount">Button count.</param>
		public Lock (int gridRow, int gridCol, int buttonCount, int currentPosition) {
			GridRow = gridRow;
			GridCol = gridCol;
			Buttons = new LockButton[buttonCount];
			for (int buttonIndex=0; buttonIndex<buttonCount; buttonIndex++) {
				Buttons [buttonIndex] = new LockButton (this, buttonIndex, null, false);
			}
			CurrentPosition = currentPosition;
		}

		/// <summary>
		/// Determines whether this instance is unlocked.
		/// </summary>
		/// <returns><c>true</c> if this instance is unlocked; otherwise, <c>false</c>.</returns>
		public bool IsUnlocked() {
			return CurrentPosition == 0;
		}

		/// <summary>
		/// Determines whether this instance is locked.
		/// </summary>
		/// <returns><c>true</c> if this instance is locked; otherwise, <c>false</c>.</returns>
		public bool IsLocked() {
			return !IsUnlocked();
		}

		public string GetRowColString() {
			return string.Format ("{0},{1}", GridRow, GridCol);
		}

	}
}

