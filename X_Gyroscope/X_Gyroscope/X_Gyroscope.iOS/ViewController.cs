using CoreMotion;
using Foundation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UIKit;

namespace X_Gyroscope.iOS
{
	public partial class ViewController : UIViewController
	{
        private CMMotionManager motionManager;
        ObservableCollection<double> gyroData;

        public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            motionManager = new CMMotionManager();

            StartButton.TouchUpInside += (sender, e) =>
            {
                gyroData = new ObservableCollection<double>();
                motionManager.StartGyroUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
                {
                    gyroData.Add(data.RotationRate.x);
                    gyroData.Add(data.RotationRate.y);
                    gyroData.Add(data.RotationRate.z);
                    var shake = gyroData.Where(gyroData => Math.Abs(gyroData) > 3).Count();
                    if (shake > 3)
                    {
                        motionManager.StopGyroUpdates();

                        var alert = new UIAlertView("Gyroscope", "Shaked!", null, "OK");
                        alert.Show();
                    }

                    this.xLabel.Text = data.RotationRate.x.ToString("0.0000");
                    this.yLabel.Text = data.RotationRate.y.ToString("0.0000");
                    this.zLabel.Text = data.RotationRate.z.ToString("0.0000");
                });
            };

            StopButton.TouchUpInside += (sender, e) =>
            {
                motionManager.StopGyroUpdates();
            };
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

