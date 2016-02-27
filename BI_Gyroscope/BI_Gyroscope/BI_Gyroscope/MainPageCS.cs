using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BI_Gyroscope
{
    public class MainPageCS : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;
        Label xLabel;
        Label yLabel;
        Label zLabel;
        Button startButton;
        Button stopButton;
        ObservableCollection<double> gyroData;

        public MainPageCS()
        {
            xLabel = new Label { Text = "x = " };
            yLabel = new Label { Text = "y = " };
            zLabel = new Label { Text = "z = " };

            startButton = new Button { Text = "Start" };
            startButton.Clicked += (_, __) =>
            {
                gyroData = new ObservableCollection<double>();

                motion.Start(MotionSensorType.Gyroscope, MotionSensorDelay.Default);
                if (motion.IsActive(MotionSensorType.Gyroscope))
                {
                    motion.SensorValueChanged += (object sender, SensorValueChangedEventArgs e) =>
                    {
                        // 乱暴ですが、Gyroscopeの各絶対値が4を超えた回数が4回以上（2回シェイク）でシェイクと判定しています。
                        gyroData.Add(((MotionVector)e.Value).X);
                        gyroData.Add(((MotionVector)e.Value).Y);
                        gyroData.Add(((MotionVector)e.Value).Z);
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
                            xLabel.Text = ((MotionVector)e.Value).X.ToString("0.0000");
                            yLabel.Text = ((MotionVector)e.Value).Y.ToString("0.0000");
                            zLabel.Text = ((MotionVector)e.Value).Z.ToString("0.0000");
                        });
                    };
                }
            };

            stopButton = new Button { Text = "Stop" };
            stopButton.Clicked += (_, __) =>
            {
                motion.Stop(MotionSensorType.Gyroscope);
            };

            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 0);
            Content = new StackLayout
            {
                Children = {
                    xLabel,
                    yLabel,
                    zLabel,
                    startButton,
                    stopButton,
                }
            };
        }
    }
}
