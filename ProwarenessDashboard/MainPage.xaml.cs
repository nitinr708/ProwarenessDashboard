using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ProwarenessDashboard
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

           // the main object for interacting with the audio/video devices
        public CaptureSource captureSource;

        // the webcam device
        public VideoCaptureDevice webcam;

        // brush for the video feed
        public VideoBrush webcamBrush;

        // brush for the captured video frame
        public ImageBrush capturedImage;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            /******************************************************/
            /*Code to Initialize, Render & Control local WebCamera*/
            /******************************************************/
            captureSource = new CaptureSource();

            // async capture failed event handler
            captureSource.CaptureFailed +=
                new EventHandler<ExceptionRoutedEventArgs>(CaptureSource_CaptureFailed);

            // async capture completed event handler
            captureSource.CaptureImageCompleted +=
                new EventHandler<CaptureImageCompletedEventArgs>(CaptureSource_CaptureImageCompleted);

            // get the webcam.  null is returned if there is not a webcam installed
            webcam = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();

            if (null != webcam)
            {
                // set the video capture source the WebCam
                captureSource.VideoCaptureDevice = webcam;

                // set the source on the VideoBrush used to display the video
                webcamBrush = new VideoBrush();
                webcamBrush.SetSource(captureSource);

                // set the Fill property of the Rectangle (defined in XAML) to the VideoBrush
                webcamDisplay.Fill = webcamBrush;

                // the brush used to fill the display rectangle
                capturedImage = new ImageBrush();

                // set the Fill property of the Rectangle (defined in XAML) to the ImageBrush
                capturedDisplay.Fill = capturedImage;
            }



            List<Team> teamsList = Team.GetList();
            
            Label label = new Label()
            {
                    Content = "Team -"//,
                    //Width = 40,
                    //Height = 32,
                    //Margin = new Thickness(14, 4, 0, 0),
                    //HorizontalAlignment = HorizontalAlignment.Left,
                    //VerticalAlignment = VerticalAlignment.Top,
                    //HorizontalContentAlignment = HorizontalAlignment.Center,
                    //VerticalContentAlignment = VerticalAlignment.Center,
                    //BorderBrush = Brushes.Black,
                    //BorderThickness = new Thickness(1),
                    //Name = "label16"
            };
            Label label2 = new Label() { Content = "Velocity:" };
            Label label3 = new Label() { Content = "Quality:" };
            Label label4 = new Label() { Content = "Reliability:" }; 
            Label label5 = new Label() { Content = "NETGEAR" }; 
            Label label6 = new Label() { Content = "18" };
            Label label7 = new Label() { Content = "1" }; 
            Label label8 = new Label() { Content = "10" }; 


        Grid grid = new Grid();
        //grid.Width = 768;
        //grid.Height = 1056;
        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        Grid.SetRow(label, 0);
        Grid.SetRow(label2, 1);
        Grid.SetRow(label3, 2); 
        Grid.SetRow(label4, 3);
        Grid.SetRow(label5, 0);
        Grid.SetColumn(label5, 1);
        Grid.SetRow(label6, 1);
        Grid.SetColumn(label6, 1);
        Grid.SetRow(label7, 2);
        Grid.SetColumn(label7, 1);
        Grid.SetRow(label8, 3);
        Grid.SetColumn(label8, 1);

        grid.Children.Add(label);
        grid.Children.Add(label3);
        grid.Children.Add(label4);
        grid.Children.Add(label2);
        grid.Children.Add(label5);
        grid.Children.Add(label6);
        grid.Children.Add(label7);
        grid.Children.Add(label8);
        //this.Content = grid;


 
            
            Button first = new Button();

            //Grid firstGrid = new Grid();
            //firstGrid.RowDefinitions.Add(Row

            //Button second = new Button();

            first.Content = grid;

            TeamsListStackPanel.Children.Add(first);
            //TeamsListStackPanel.Children.Add(second);
            


        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // request access to the device and verify the VideoCaptureDevice is not null
            if(CaptureDeviceConfiguration.RequestDeviceAccess() && captureSource.VideoCaptureDevice != null )
            {
                captureSource.Start();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // verify the VideoCaptureDevice is not null
            if (captureSource.VideoCaptureDevice != null)
            {
                captureSource.Stop();
            }
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            // verify the VideoCaptureDevice is not null and the device is started
            if (captureSource.VideoCaptureDevice != null && captureSource.State == CaptureState.Started)
            {
                captureSource.CaptureImageAsync();
            }
        }

        void CaptureSource_CaptureImageCompleted(object sender, CaptureImageCompletedEventArgs e)
        {
            // Set the ImageBrush source to the WriteableBitmap passed in through the event arguments
            capturedImage.ImageSource = e.Result;
        }

        void CaptureSource_CaptureFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(string.Format("Failed to capture image: {0}", e.ErrorException.Message));
        }
    }
}




     

