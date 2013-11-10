// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace Locks.LevelGenerator
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSPopUpButton ButtonCountPopupButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopUpButton ColumnsPopupButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSStepper DifficultyStepper { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField DifficultyTextField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField FileNameTextField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton GenerateButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField LockGridSerializedTextField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopUpButton RowsPopupButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton SaveButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FileNameTextField != null) {
				FileNameTextField.Dispose ();
				FileNameTextField = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}

			if (ButtonCountPopupButton != null) {
				ButtonCountPopupButton.Dispose ();
				ButtonCountPopupButton = null;
			}

			if (ColumnsPopupButton != null) {
				ColumnsPopupButton.Dispose ();
				ColumnsPopupButton = null;
			}

			if (DifficultyStepper != null) {
				DifficultyStepper.Dispose ();
				DifficultyStepper = null;
			}

			if (DifficultyTextField != null) {
				DifficultyTextField.Dispose ();
				DifficultyTextField = null;
			}

			if (GenerateButton != null) {
				GenerateButton.Dispose ();
				GenerateButton = null;
			}

			if (LockGridSerializedTextField != null) {
				LockGridSerializedTextField.Dispose ();
				LockGridSerializedTextField = null;
			}

			if (RowsPopupButton != null) {
				RowsPopupButton.Dispose ();
				RowsPopupButton = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
