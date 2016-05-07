using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Windows.Devices.Sensors;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Shell;
using Mapper.Resources;

namespace Mapper
{
    public partial class MainPage : PhoneApplicationPage
    {
        Magnetometer magnetometer;
        Compass compass;
        private DispatcherTimer dispatcherTimer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Loaded += (s, e) => {
                InitMagnetometer();
                InitCompass();

                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                dispatcherTimer.Start();
            };
        }

        private void InitMagnetometer()
        {
            magnetometer = Magnetometer.GetDefault();
            if(magnetometer == null)
            {
                MessageBox.Show(@"Nessun magnetometro trovato. Impossibile usare l'applicazione");
                return;
            }

            var currentReading = magnetometer.GetCurrentReading();
            MagneticValue.Text = currentReading.ToString();
        }

        private void InitCompass()
        {
            compass = Compass.GetDefault();
            var currentReading = compass.GetCurrentReading();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SetMagneticValue();
            SetCompassValue();   
        }
        
        private void SetMagneticValue()
        {
            var currentReading = magnetometer.GetCurrentReading();
            double avg = Math.Sqrt(Math.Pow(currentReading.MagneticFieldX, 2) 
                + Math.Pow(currentReading.MagneticFieldY, 2)
                + Math.Pow(currentReading.MagneticFieldZ, 2));

            string txt = "Misura del " + currentReading.Timestamp + Environment.NewLine +
                "x: " + currentReading.MagneticFieldX + Environment.NewLine +
                "y: " + currentReading.MagneticFieldY + Environment.NewLine +
                "z: " + currentReading.MagneticFieldZ + Environment.NewLine +
                "directional accuracy: " + currentReading.DirectionalAccuracy + Environment.NewLine +
                "media: " + avg.ToString();
            MagneticValue.Text = txt;
        }

        private void SetCompassValue()
        {
            var currentReading = compass.GetCurrentReading();

            string txt = "Misura del " + currentReading.Timestamp + Environment.NewLine +
                "Nord Magnetico: " + currentReading.HeadingMagneticNorth + Environment.NewLine +
                "Nord Reale: " + currentReading.HeadingTrueNorth + Environment.NewLine +
                "heading accuracy: " + currentReading.HeadingAccuracy;
            CompassValue.Text = txt;
        }
    }
}