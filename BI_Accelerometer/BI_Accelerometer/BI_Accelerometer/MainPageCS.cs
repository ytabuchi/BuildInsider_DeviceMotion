﻿using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BI_Accelerometer
{
    public class MainPageCS : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;
        Label xLabel;
        Label yLabel;
        Label zLabel;
        Button startButton;
        Button stopButton;

        public MainPageCS()
        {
            xLabel = new Label { Text = "x = " };
            yLabel = new Label { Text = "y = " };
            zLabel = new Label { Text = "z = " };

            startButton = new Button { Text = "Start" };
            startButton.Clicked += (_, __) =>
            {
                motion.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Default);
                if (motion.IsActive(MotionSensorType.Accelerometer))
                {
                    motion.SensorValueChanged += (object sender, SensorValueChangedEventArgs e) =>
                    {
                        System.Diagnostics.Debug.WriteLine("X:{0}, Y:{1}, Z:{2}", ((MotionVector)e.Value).X, ((MotionVector)e.Value).Y, ((MotionVector)e.Value).Z);

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
                motion.Stop(MotionSensorType.Accelerometer);
            };

            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 0);
            Content = new StackLayout
            {
                Children = {
                    xLabel,
                    yLabel,
                    zLabel,
                    startButton,
                    stopButton
                }
            };
        }
    }
}
