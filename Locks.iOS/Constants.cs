using System;

namespace Locks.iOS {

	public static class Constants {

		public const string ContentFolder = "Content";

		public const string IPadImageContentFolder = "\\Images\\iPad\\";

		public const string IPadRetinaImageContentFolder = "\\Images\\iPadRetina\\";

		public const string AudioContentFolder = "\\Audio\\";

		public const string FontContentFolder = "\\Fonts\\";

		public enum ViewLayer {
			Layer1 = 0,
			Layer2 = 1,
			Layer3 = 2,
			Layer4 = 3 ,
			Layer5 = 4 ,
			Layer6 = 5 ,
			ModalOverlay = 6,
			Modal = 7
		}

		public enum ViewContainerLayout {
			Absolute = 0 ,
			FloatLeft = 1 ,
			Stack = 2 ,
			StackCentered = 3
		}

	}
}

