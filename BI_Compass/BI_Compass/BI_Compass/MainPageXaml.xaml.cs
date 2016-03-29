using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BI_Compass
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
            motion.Start(MotionSensorType.Compass, MotionSensorDelay.Default);
            if (motion.IsActive(MotionSensorType.Compass))
            {
                motion.SensorValueChanged += (object s, SensorValueChangedEventArgs a) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        compassLabel.Text = string.Format($"{a.Value.Value:N0}");
                    });
                };
            }
        }

        private void StopClicked(object sender, EventArgs e)
        {
            motion.Stop(MotionSensorType.Compass);
        }
    }
}
