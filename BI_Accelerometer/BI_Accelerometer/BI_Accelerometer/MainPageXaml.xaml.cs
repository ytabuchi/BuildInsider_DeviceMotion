using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BI_Accelerometer
{
    public partial class MainPageXaml : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;

        public MainPageXaml()
        {
            InitializeComponent();
        }

        private void StartClicked(object sender, EventArgs e)
        {
            motion.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Default);
            if (motion.IsActive(MotionSensorType.Accelerometer))
            {
                motion.SensorValueChanged += (object s, SensorValueChangedEventArgs a) =>
                {
                    System.Diagnostics.Debug.WriteLine("X:{0}, Y:{1}, Z:{2}", ((MotionVector)a.Value).X, ((MotionVector)a.Value).Y, ((MotionVector)a.Value).Z);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        xLabel.Text = ((MotionVector)a.Value).X.ToString("0.0000");
                        yLabel.Text = ((MotionVector)a.Value).Y.ToString("0.0000");
                        zLabel.Text = ((MotionVector)a.Value).Z.ToString("0.0000");
                    });
                };
            }
        }

        private void StopClicked(object sender, EventArgs e)
        {
            motion.Stop(MotionSensorType.Accelerometer);
        }
    }
}
