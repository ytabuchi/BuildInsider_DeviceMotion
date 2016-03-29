using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;

namespace X_Magnetometer.Droid
{
	[Activity (Label = "X_Magnetometer.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ISensorEventListener
	{
        static readonly object syncLock = new object();
        SensorManager sensorManager;
        TextView xLabel;
        TextView yLabel;
        TextView zLabel;
        Button startButton;
        Button stopButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            sensorManager = (SensorManager)GetSystemService(SensorService);

            xLabel = FindViewById<TextView>(Resource.Id.xLabel);
            yLabel = FindViewById<TextView>(Resource.Id.yLabel);
            zLabel = FindViewById<TextView>(Resource.Id.zLabel);

            startButton = FindViewById<Button>(Resource.Id.StartButton);
            startButton.Click += (sender, e) =>
            {
                sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.), SensorDelay.Normal);
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
                xLabel.Text = e.Values[0].ToString("0.0000");
                yLabel.Text = e.Values[1].ToString("0.0000");
                zLabel.Text = e.Values[2].ToString("0.0000");
            }
        }

	}
}


