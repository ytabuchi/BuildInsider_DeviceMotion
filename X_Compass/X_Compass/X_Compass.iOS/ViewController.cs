using System;
using CoreLocation;
using UIKit;

namespace X_Compass.iOS
{
	public partial class ViewController : UIViewController
	{
        private CLLocationManager locationManager;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            locationManager = new CLLocationManager();

            StartButton.TouchUpInside += (sender, e) =>
            {
                locationManager.StartUpdatingHeading();

                locationManager.UpdatedHeading += (object s, CLHeadingUpdatedEventArgs a) =>
                {
                    AngleLabel.Text = string.Format($"{locationManager.Heading.MagneticHeading:N0}°");
                };
            };

            StopButton.TouchUpInside += (sender, e) =>
            {
                locationManager.StopUpdatingHeading();
            };
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

