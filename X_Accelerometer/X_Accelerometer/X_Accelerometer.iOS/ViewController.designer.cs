// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace X_Accelerometer.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton StartButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton StopButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel xLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel yLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel zLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (StartButton != null) {
				StartButton.Dispose ();
				StartButton = null;
			}
			if (StopButton != null) {
				StopButton.Dispose ();
				StopButton = null;
			}
			if (xLabel != null) {
				xLabel.Dispose ();
				xLabel = null;
			}
			if (yLabel != null) {
				yLabel.Dispose ();
				yLabel = null;
			}
			if (zLabel != null) {
				zLabel.Dispose ();
				zLabel = null;
			}
		}
	}
}
