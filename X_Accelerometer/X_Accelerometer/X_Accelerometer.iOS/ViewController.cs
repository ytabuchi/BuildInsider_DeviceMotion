using CoreMotion;
using Foundation;
using System;

using UIKit;

namespace X_Accelerometer.iOS
{
	public partial class ViewController : UIViewController
	{
        private CMMotionManager motionManager;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            motionManager = new CMMotionManager();

            StartButton.TouchUpInside += (sender, e) =>
            {
                motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    this.xLabel.Text = data.Acceleration.X.ToString("0.0000");
                    this.yLabel.Text = data.Acceleration.Y.ToString("0.0000");
                    this.zLabel.Text = data.Acceleration.Z.ToString("0.0000");
                });
            };

            StopButton.TouchUpInside += (sender, e) =>
            {
                motionManager.StopAccelerometerUpdates();
            };
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

