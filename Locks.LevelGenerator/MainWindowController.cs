using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace Locks.LevelGenerator
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors
		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion
		private NSBox LockGridPreviewView { get; set; }

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			this.Window.Title = "Lock Game Level Generator";

			System.Drawing.RectangleF lockGridPreviewViewRectangle = this.Window.ContentView.Bounds;
			lockGridPreviewViewRectangle.Height -= 200;
			LockGridPreviewView = new NSBox (lockGridPreviewViewRectangle);
			LockGridPreviewView.BorderColor = NSColor.Gray;
			LockGridPreviewView.Title = "";


			DifficultyStepper.MinValue = 0.0f;
			DifficultyStepper.MaxValue = 1.0f;
			DifficultyStepper.Increment = 0.05f;
			DifficultyStepper.FloatValue = DifficultyTextField.FloatValue = 0.5f;

			DifficultyStepper.Activated += (object sender, EventArgs e) => {
				DifficultyTextField.FloatValue = DifficultyStepper.FloatValue;
			};

			GenerateButton.Activated += (object sender, EventArgs e) => {
				GenerateLockGrid ();
			};

			SaveButton.Activated += (object sender, EventArgs e) => {
				SaveLockGrid();
			};

		}

		private void GenerateLockGrid ()
		{
			try {
				Models.LockGrid lockGrid = Rules.LockGrid.CreateRandom (Convert.ToInt32 (RowsPopupButton.SelectedItem.Title), Convert.ToInt32 (ColumnsPopupButton.SelectedItem.Title), Convert.ToInt32 (ButtonCountPopupButton.SelectedItem.Title), DifficultyTextField.FloatValue);
				LockGridSerializedTextField.StringValue = Rules.LockGrid.Serialize (lockGrid);
			} catch {
				LockGridSerializedTextField.StringValue = "Error generating level";
			}
		}

		private void SaveLockGrid ()
		{
			string filePath = System.IO.Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FileNameTextField.StringValue);
			System.IO.File.WriteAllText (filePath, LockGridSerializedTextField.StringValue);
		}

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}
	}
}

