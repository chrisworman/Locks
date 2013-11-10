using System;

namespace Models
{
	public class LockButtonPushResult {

		public LockButton LinkedButton { get; private set; }

		public int LinkedButtonContainingLockPriorPosition { get; private set; }

		public LockButtonPushResult (LockButton linkedButton, int linkedButtonContainingLockPriorPosition) {
			LinkedButton = linkedButton;
			LinkedButtonContainingLockPriorPosition = linkedButtonContainingLockPriorPosition;
		}

		public int GetLinkedButtonContainingLockPositionDelta() {
			return LinkedButton.ContainingLock.CurrentPosition - LinkedButtonContainingLockPriorPosition;
		}

	}
}

