using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BI_Compass
{
    public class MainPageCS : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;
        Label angleLabel;
        Button startButton;
        Button stopButton;

        public MainPageCS()
        {
            angleLabel = new Label
            {
                Text = "Angle",
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            startButton = new Button { Text = "Start" };
            startButton.Clicked += (sender, e) =>
            {
                motion.Start(MotionSensorType.Compass, MotionSensorDelay.Default);
                if (motion.IsActive(MotionSensorType.Compass))
                {
                    motion.SensorValueChanged += (object s, SensorValueChangedEventArgs a) =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            angleLabel.Text = string.Format($"{a.Value.Value:N0}");
                        });
                    };
                }
            };

            stopButton = new Button { Text = "Stop" };
            stopButton.Clicked += (sender, e) =>
            {
                motion.Stop(MotionSensorType.Compass);
            };

            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 0);
            Content = new StackLayout
            {
                Children = {
                    new Frame
                    {
                        Padding = 30,
                        Content = angleLabel,
                    },
                    startButton,
                    stopButton
                }
            };
        }
    }
}
