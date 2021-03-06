﻿using System;
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
using System.Windows.Browser;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Charting;







namespace ProwarenessDashboard
{
    public enum DisplayElement
    {
        Frame,
        Div
    }
    public partial class MainPage : UserControl
    {

        HtmlElement theDisplay;
        DisplayElement currentDisplay = DisplayElement.Frame;

       
        public MainPage()
        {
            InitializeComponent();
         
            this.SizeChanged += new SizeChangedEventHandler(Page_SizeChanged);
            VelocityChart.Loaded += new RoutedEventHandler(VelocityUC_Loaded);
            BurnDown.Loaded += new RoutedEventHandler(BurnLoadUC_Loaded);
            QualityChart.Loaded += new RoutedEventHandler(QualityUC_Loaded);
          
        }

        void BurnLoadUC_Loaded(object sender, RoutedEventArgs e)
        {
            List<BurnDownTrend> burndown = new List<BurnDownTrend>();
            burndown.Add(new BurnDownTrend() { Day = "Mon", Points = 60 });
            burndown.Add(new BurnDownTrend() { Day = "Tue", Points = 50 });
            burndown.Add(new BurnDownTrend() { Day = "Wed", Points = 40 });
            burndown.Add(new BurnDownTrend() { Day = "Thu", Points = 30 });
            burndown.Add(new BurnDownTrend() { Day = "Fri", Points = 20 });
            burndown.Add(new BurnDownTrend() { Day = "Mon", Points = 60 });
            burndown.Add(new BurnDownTrend() { Day = "Tue", Points = 50 });
            burndown.Add(new BurnDownTrend() { Day = "Wed", Points = 40 });
            burndown.Add(new BurnDownTrend() { Day = "Thu", Points = 30 });
            burndown.Add(new BurnDownTrend() { Day = "Fri", Points = 20 });
           
            BurnDown.DataContext = burndown;
        }


        void VelocityUC_Loaded(object sender, RoutedEventArgs e)
        {
            List<VelocityTrend> velocity = new List<VelocityTrend>();
            velocity.Add(new VelocityTrend() { SprintName = "Sprint 1", Velocity = 28 });
            velocity.Add(new VelocityTrend() { SprintName = "Sprint 2", Velocity = 34 });
            velocity.Add(new VelocityTrend() { SprintName = "Sprint 3", Velocity = 17 });
            velocity.Add(new VelocityTrend() { SprintName = "Sprint 4", Velocity = 22 });

            VelocityChart.DataContext = velocity;
        }

        void QualityUC_Loaded(object sender, RoutedEventArgs e)
        {
            List<QualityTrend> quality = new List<QualityTrend>();
            quality.Add(new QualityTrend() { SprintName = "Sprint 1", Quality = 10 });
            quality.Add(new QualityTrend() { SprintName = "Sprint 2", Quality = 40 });
            quality.Add(new QualityTrend() { SprintName = "Sprint 3", Quality = 120 });
            quality.Add(new QualityTrend() { SprintName = "Sprint 4", Quality = 100 });

            QualityChart.DataContext = quality;
        }

        public class BurnDownTrend
        {
            public string Day { get; set; }
            public int Points { get; set; }
        }

        public class VelocityTrend
        {
            public string SprintName { get; set; }
            public int Velocity { get; set; }
        }

        public class QualityTrend
        {
            public string SprintName { get; set; }
            public int Quality { get; set; }
        }

        

     
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           List<Team> teamsList = Prowareness.GetTeams();
          
        Telerik.Windows.Controls.RadChart radChart = new Telerik.Windows.Controls.RadChart();
        radChart.DefaultView.ChartArea.AxisX.IsDateTime = true;
        radChart.DefaultView.ChartArea.AxisX.Step = 5;
        radChart.DefaultView.ChartArea.AxisX.LabelStep = 2;
        radChart.DefaultView.ChartArea.AxisX.StepLabelLevelCount = 3;
        radChart.DefaultView.ChartArea.AxisX.StepLabelLevelHeight = 10;
        radChart.DefaultView.ChartArea.AxisX.DefaultLabelFormat = "dd-MMM";
        //....
        SeriesMapping sm = new SeriesMapping();
        sm.SeriesDefinition = new SplineSeriesDefinition();
        //....
        sm.ItemMappings.Add( new ItemMapping( "Date", DataPointMember.XValue ) );
        sm.ItemMappings.Add( new ItemMapping( "Value", DataPointMember.YValue ) );
        radChart.SeriesMappings.Add( sm );
        

        
            foreach (Team team in teamsList)
            {
                addTeamTab(team);
            }
        }

        
        public void addTeamTab(Team team)
        {

            TextBlock Label = new TextBlock() { Text = "Team -", Margin = new Thickness(5, 0, 0, 0) };
            TextBlock lblVelocity = new TextBlock() { Text = "Velocity:", Margin = new Thickness(5, 0, 0, 0) };
            TextBlock lblQuality = new TextBlock() { Text = "Quality:", Margin = new Thickness(5, 0, 0, 0) };
            TextBlock lblReliability = new TextBlock() { Text = "Reliability:", Margin = new Thickness(5, 0, 0, 0) };
            TextBlock lblTeamName = new TextBlock() { Text = team.name, Margin = new Thickness(5, 0, 0, 0), TextWrapping = TextWrapping.Wrap }; //TextTrimming=TextTrimming.WordEllipsis };
            TextBlock lblTeamVelocity = new TextBlock() { Text = team.velocity.ToString(), Margin = new Thickness(5, 0, 0, 0) };
            TextBlock lblTeamQuality = new TextBlock() { Text = team.quality.ToString(), Margin = new Thickness(5, 0, 0, 0) };
            TextBlock lblTeamReliability = new TextBlock() { Text = team.reliability.ToString(), Margin = new Thickness(5, 0, 0, 0) };


            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetRow(Label, 0);
            Grid.SetRow(lblVelocity, 1);
            Grid.SetRow(lblQuality, 2);
            Grid.SetRow(lblReliability, 3);
            Grid.SetRow(lblTeamName, 0);
            Grid.SetColumn(lblTeamName, 1);
            Grid.SetRow(lblTeamVelocity, 1);
            Grid.SetColumn(lblTeamVelocity, 1);
            Grid.SetRow(lblTeamQuality, 2);
            Grid.SetColumn(lblTeamQuality, 1);
            Grid.SetRow(lblTeamReliability, 3);
            Grid.SetColumn(lblTeamReliability, 1);

            grid.Children.Add(Label);
            grid.Children.Add(lblQuality);
            grid.Children.Add(lblReliability);
            grid.Children.Add(lblVelocity);
            grid.Children.Add(lblTeamName);
            grid.Children.Add(lblTeamVelocity);
            grid.Children.Add(lblTeamQuality);
            grid.Children.Add(lblTeamReliability);

            Rectangle teamRectangle = new Rectangle();
            teamRectangle.RadiusX = 6;
            teamRectangle.RadiusY = 6;
            //teamRectangle.Width = 390;
            //teamRectangle.Margin = new Thickness(6, 6, 6, 6);
            
            teamRectangle.HorizontalAlignment = HorizontalAlignment.Left;
            Button btnTeam = new Button();
            
            teamRectangle.DataContext = btnTeam;
            btnTeam.HorizontalContentAlignment = HorizontalAlignment.Left;
            btnTeam.Margin = new Thickness(8,8,4,4);
            btnTeam.Height = 180;
                

            btnTeam.Content = grid;
            btnTeam.Background = new SolidColorBrush(Colors.Orange);
            btnTeam.BorderBrush = new SolidColorBrush(Colors.LightGray);
            btnTeam.Click += new RoutedEventHandler(StartButton_Click);
          
            btnTeam.CommandParameter = team.videoUrl;
            TeamsListStackPanel.Children.Add(btnTeam);
        }

       


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;
            foreach (Button eachButton in TeamsListStackPanel.Children)
            {
                if (eachButton != senderButton)
                {
                    eachButton.BorderThickness = new Thickness(0);
		            eachButton.Background = new SolidColorBrush(Colors.Orange);
                    eachButton.BorderBrush = new SolidColorBrush(Colors.LightGray);
                    eachButton.FontWeight = FontWeights.Normal;
                }
            }

            senderButton.BorderThickness = new Thickness(2);
            senderButton.Background = new SolidColorBrush(Colors.Blue);
            
            senderButton.FontWeight = FontWeights.ExtraBold;
            senderButton.BorderBrush = new SolidColorBrush(Colors.Black);
            Button team = (Button) sender;
            string videoUrl =  team.CommandParameter.ToString();
            PositionElement();
            if (currentDisplay != DisplayElement.Frame)
            {
                currentDisplay = DisplayElement.Frame;
                theDisplay.SetStyleAttribute("visibility", "hidden");
                PositionElement();
            }

            theDisplay.SetAttribute("src", videoUrl);
        }

        void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PositionElement();
        }

        void FrameContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PositionElement();
        }

      
        void PositionElement()
        {
            if (currentDisplay == DisplayElement.Frame)
            {
                theDisplay = HtmlPage.Document.GetElementById("theFrame");
                if (theDisplay == null)
                {
                    theDisplay = HtmlPage.Document.CreateElement("IFrame");
                    theDisplay.SetAttribute("ID", "theFrame");
                    HtmlPage.Document.Body.AppendChild(theDisplay);
                }
            }
            else
            {
                theDisplay = HtmlPage.Document.GetElementById("theDiv");
                if (theDisplay == null)
                {
                    theDisplay = HtmlPage.Document.CreateElement("Div");
                    theDisplay.SetAttribute("ID", "theDiv");
                    theDisplay.SetStyleAttribute("backgroundColor", "white");
                    theDisplay.SetStyleAttribute("borderWidth", "1px");
                    theDisplay.SetStyleAttribute("borderColor", "black");
                    theDisplay.SetStyleAttribute("borderStyle", "solid");

                    HtmlPage.Document.Body.AppendChild(theDisplay);
                }
            }
            theDisplay.SetStyleAttribute("position", "absolute");
            Point topleft = Position.GetAbsolutePosition(this.FrameContainer);
            int controlTop = (int)topleft.Y;
            int controlLeft = (int)topleft.X;

            theDisplay.SetStyleAttribute("left", controlLeft.ToString() + "px");
            theDisplay.SetStyleAttribute("top", controlTop.ToString() + "px");
            theDisplay.SetStyleAttribute("visibility", "visible");
            theDisplay.SetStyleAttribute("width", this.FrameContainer.ActualWidth.ToString() + "px");
            theDisplay.SetStyleAttribute("height", this.FrameContainer.ActualHeight.ToString() + "px");
        }

        public Button btnTeam { get; set; }

    }
}



     

