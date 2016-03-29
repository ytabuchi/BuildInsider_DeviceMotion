using System;
using CoreMotion;
using UIKit;
using Foundation;

namespace X_Magnetometer.iOS
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
                motionManager.StartMagnetometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    this.xLabel.Text = data.MagneticField.X.ToString("0.0000");
                    this.yLabel.Text = data.MagneticField.Y.ToString("0.0000");
                    this.zLabel.Text = data.MagneticField.Z.ToString("0.0000");
                });
            };

            StopButton.TouchUpInside += (sender, e) =>
            {
                motionManager.StopMagnetometerUpdates();
            };
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

