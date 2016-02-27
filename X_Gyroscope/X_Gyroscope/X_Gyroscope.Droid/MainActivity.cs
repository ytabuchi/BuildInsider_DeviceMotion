using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using System.Collections.ObjectModel;
using System.Linq;

namespace X_Gyroscope.Droid
{
	[Activity (Label = "X_Gyroscope.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ISensorEventListener
    {
        static readonly object syncLock = new object();
        SensorManager sensorManager;
        TextView xLabel;
        TextView yLabel;
        TextView zLabel;
        Button startButton;
        Button stopButton;
        ObservableCollection<double> gyroData;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

            sensorManager = (SensorManager)GetSystemService(SensorService);

            xLabel = FindViewById<TextView>(Resource.Id.xLabel);
            yLabel = FindViewById<TextView>(Resource.Id.yLabel);
            zLabel = FindViewById<TextView>(Resource.Id.zLabel);

            startButton = FindViewById<Button>(Resource.Id.StartButton);
            startButton.Click += (sender, e) =>
            {
                gyroData = new ObservableCollection<double>();
                sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Gyroscope), SensorDelay.Normal);
            };

            stopButton = FindViewById<Button>(Resource.Id.StopButton);
            stopButton.Click += (sender, e) =>
            {
                sensorManager.UnregisterListener(this);
            };
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (syncLock)
            {
                gyroData.Add(e.Values[0]);
                gyroData.Add(e.Values[1]);
                gyroData.Add(e.Values[2]);
                var shake = gyroData.Where(gyroData => Math.Abs(gyroData) > 3).Count();
                if (shake > 3)
                {
                    sensorManager.UnregisterListener(this);

                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Gyroscope");
                    dialog.SetMessage("Shaked!");
                    dialog.SetNegativeButton("OK", (_, __) => { });
                    dialog.Show();
                }

                xLabel.Text = e.Values[0].ToString("0.0000");
                yLabel.Text = e.Values[1].ToString("0.0000");
                zLabel.Text = e.Values[2].ToString("0.0000");
            }
        }
    }
}


