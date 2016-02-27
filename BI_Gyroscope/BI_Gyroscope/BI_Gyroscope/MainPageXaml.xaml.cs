using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BI_Gyroscope
{
    public partial class MainPageXaml : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;
        ObservableCollection<double> gyroData;

        public MainPageXaml()
        {
            InitializeComponent();
        }

        private void StartClicked(object sender, EventArgs e)
        {
            gyroData = new ObservableCollection<double>();

            motion.Start(MotionSensorType.Gyroscope, MotionSensorDelay.Default);
            if (motion.IsActive(MotionSensorType.Gyroscope))
            {
                motion.SensorValueChanged += (object s, SensorValueChangedEventArgs v) =>
                {
                    // 乱暴ですが、Gyroscopeの各絶対値が4を超えた回数が4回以上（2回シェイク）でシェイクと判定しています。
                    gyroData.Add(((MotionVector)v.Value).X);
                    gyroData.Add(((MotionVector)v.Value).Y);
                    gyroData.Add(((MotionVector)v.Value).Z);
                    var shake = gyroData.Where(gyroData => Math.Abs(gyroData) > 4).Count();
                    if (shake > 3)
                    {
                        motion.Stop(MotionSensorType.Gyroscope);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert("Gyroscope", "Shaked!", "OK");
                        });
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        xLabel.Text = ((MotionVector)v.Value).X.ToString("0.0000");
                        yLabel.Text = ((MotionVector)v.Value).Y.ToString("0.0000");
                        zLabel.Text = ((MotionVector)v.Value).Z.ToString("0.0000");
                    });
                };
            }
        }

        private void StopClicked(object sender, EventArgs e)
        {
            motion.Stop(MotionSensorType.Gyroscope);
        }
    }
}
