using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BI_Magnetometer
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
            motion.Start(MotionSensorType.Magnetometer, MotionSensorDelay.Default);
            if (motion.IsActive(MotionSensorType.Magnetometer))
            {
                motion.SensorValueChanged += (object s, SensorValueChangedEventArgs a) =>
                {
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
            motion.Stop(MotionSensorType.Magnetometer);
        }
    }
}
