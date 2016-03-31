using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware;
using System.Linq;

namespace X_Compass.Droid
{
	[Activity (Label = "X_Compass.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ISensorEventListener
    {
        SensorManager sensorManager;
        TextView angleLabel;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            sensorManager = (SensorManager)GetSystemService(SensorService);
            angleLabel = FindViewById<TextView>(Resource.Id.AngleLabel);

            var startButton = FindViewById<Button>(Resource.Id.StartButton);
            startButton.Click += (sender, e) =>
            {
                sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Normal);
                sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.MagneticField), SensorDelay.Normal);
            };

            var stopButton = FindViewById<Button>(Resource.Id.StopButton);
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

        private float[] accelerometerValue;
        private float[] magneticFieldValue;

        // http://furuya02.hatenablog.com/entry/20140526/1401130007 SIN/札幌ワークスさんの記事を参考（パクリ）にしました。
        public void OnSensorChanged(SensorEvent e)
        {
            switch (e.Sensor.Type)
            {
                case SensorType.Accelerometer:
                    accelerometerValue = new float[3];
                    accelerometerValue = e.Values.ToArray();
                    break;
                case SensorType.MagneticField:
                    magneticFieldValue = new float[3];
                    magneticFieldValue = e.Values.ToArray();
                    break;
                default:
                    break;
            }

            if (magneticFieldValue != null && accelerometerValue != null)
            {
                var Rotate1 = new float[16];
                var Rotate2 = new float[16];
                var Inclination = new float[16];
                var val = new float[3];

                // 加速度センサーと磁気センサーの値から回転行列を求める
                SensorManager.GetRotationMatrix(Rotate1, Inclination, accelerometerValue, magneticFieldValue);
                // 端末の画面設定に合わせる変換行列を求める(以下は, 縦表示で画面を上にした場合)
                SensorManager.RemapCoordinateSystem(Rotate1, Android.Hardware.Axis.X, Android.Hardware.Axis.Y, Rotate2);
                // 方位角及び傾きを求める
                SensorManager.GetOrientation(Rotate2, val);
                // ラジアンを角度に変換
                for (var i = 0; i < 3; i++)
                {
                    val[i] = (float)(val[i] * 180 / Math.PI);
                }

                System.Diagnostics.Debug.WriteLine($"0: {val[0]}, 1: {val[1]}, 2: {val[2]}");

                angleLabel.Text = string.Format("{0:F0}°", (val[0] < 0) ? val[0] + 360 : val[0]);
            }
        }
    }
}


