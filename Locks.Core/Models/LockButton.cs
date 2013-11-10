using System;

namespace Models {

	public class LockButton {

		/// <summary>
		/// The Lock that contains this button.
		/// </summary>
		/// <value>The containing lock.</value>
		public Lock ContainingLock { get; set;}

		/// <summary>
		/// Gets the index relative to the containing lock.
		/// </summary>
		/// <value>The index of the containing lock.</value>
		public int ContainingLockIndex { get; private set; }

		/// <summary>
		/// The button that is linked to this button: the 'On' state of this button is the inverse of the linked button.
		/// </summary>
		/// <value>The linked button.</value>
		public LockButton LinkedButton { get; private set; }

		/// <summary>
		/// Gets the containing lock grid row.
		/// </summary>
		/// <value>The containing lock grid row.</value>
		public int ContainingLockGridRow {
			get {
				return ContainingLock.GridRow;
			}
		}

		/// <summary>
		/// Gets the containg lock grid col.
		/// </summary>
		/// <value>The containg lock grid col.</value>
		public int ContaingLockGridCol {
			get {
				return ContainingLock.GridCol;
			}
		}

		/// <summary>
		/// Is this button 'On'?
		/// </summary>
		/// <value><c>true</c> if on; otherwise, <c>false</c>.</value>
		public bool IsOn { get; internal set; }

		/// <summary>
		/// Create a new LockButton contained on the specified Lock, linked to the specified button.
		/// </summary>
		/// <param name="containingLock">Containing lock.</param>
		/// <param name="linkedButton">Linked button.</param>
		/// <param name="on">If set to <c>true</c> on.</param>
		public LockButton (Lock containingLock, int containingLockIndex, LockButton linkedButton, bool isOn) {
			ContainingLock = containingLock;
			ContainingLockIndex = containingLockIndex;
			LinkedButton = linkedButton;
			IsOn = isOn;
		}

		/// <summary>
		/// Push this button, toggling the 'On' state of this button and it's linked button and changing the position
		/// of the containing lock.  Returns the 'On' state of this button after the push.
		/// </summary>
		public LockButtonPushResult Push() {
			// Update linked button
			int linkedButtonContainingLockPriorPosition = 0;
			if (IsLinked ()) {
				LinkedButton.IsOn = IsOn;
				linkedButtonContainingLockPriorPosition = LinkedButton.ContainingLock.CurrentPosition;
				LinkedButton.ContainingLock.CurrentPosition += IsOn ? 1 : -1;
			}
			// Update this button's state
			IsOn = !IsOn;
			ContainingLock.CurrentPosition += IsOn ? 1 : -1;
			return new LockButtonPushResult (LinkedButton, linkedButtonContainingLockPriorPosition);
		}

		/// <summary>
		/// Link this button with the specified button.
		/// </summary>
		/// <param name="lockButton">Lock button.</param>
		public void LinkWithButton(Models.LockButton lockButton){
			LinkedButton = lockButton;
			lockButton.LinkedButton = this;
		}

		/// <summary>
		/// Is this button linked to another button?
		/// </summary>
		/// <returns><c>true</c> if this instance is linked; otherwise, <c>false</c>.</returns>
		public bool IsLinked(){
			return LinkedButton != null;
		}

		/// <summary>
		/// Is this button not linked to another button?
		/// </summary>
		/// <returns><c>true</c> if this instance is unlinked; otherwise, <c>false</c>.</returns>
		public bool IsUnlinked(){
			return !IsLinked();
		}

	}
}

