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
using System.Windows.Browser;

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
        
        //private RoutedEventHandler StartButton_Click;
        public MainPage()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(Page_SizeChanged);
            
            //string s = @"<Center><a href='http://google.com'>Go To Silverlight</a></Center><br/>You Can put some HTML here, it will be displayed in the box below";
            //this.HtmlBox.Text = s;
        }

     
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

           List<Team> teamsList = Prowareness.GetTeams();
            
            foreach (Team team in teamsList)
            {
                addTeamTab(team);
            }
        }


        public void addTeamTab(Team team)
        {

            Label Label = new Label() { Content = "Team -", Margin = new Thickness(5,0,0,0) };
            Label lblVelocity = new Label() { Content = "Velocity:", Margin = new Thickness(5, 0, 0, 0) };
            Label lblQuality = new Label() { Content = "Quality:", Margin = new Thickness(5, 0, 0, 0) };
            Label lblReliability = new Label() { Content = "Reliability:", Margin = new Thickness(5, 0, 0, 0) };
            Label lblTeamName = new Label() { Content = team.name.ToString(), Margin = new Thickness(5, 0, 0, 0) };
            Label lblTeamVelocity = new Label() { Content = team.velocity.ToString(), Margin = new Thickness(5, 0, 0, 0) };
            Label lblTeamQuality = new Label() { Content = team.quality.ToString(), Margin = new Thickness(5, 0, 0, 0) };
            Label lblTeamReliability = new Label() { Content = team.reliability.ToString(), Margin = new Thickness(5, 0, 0, 0) };


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


            btnTeam = new Button();
            btnTeam.HorizontalContentAlignment = HorizontalAlignment.Left;

            btnTeam.Margin = new Thickness(6, 6, 0, 6);

            btnTeam.Content = grid;
            btnTeam.Background = new SolidColorBrush(Colors.Orange);
            btnTeam.BorderBrush = new SolidColorBrush(Colors.LightGray);
            btnTeam.Click += new RoutedEventHandler(StartButton_Click);
            LoadRightTopPanel(team);
            loadVideoPanel(team);
            btnTeam.CommandParameter = team.videoUrl;
            TeamsListStackPanel.Children.Add(btnTeam); 
        }

        public void loadVideoPanel(Team team)
        {
            //btnTeam.Click += new RoutedEventHandler(StartButton_Click);
            
        }



        private void LoadRightTopPanel(Team team)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

            //this.btnTeam.Background = new SolidColorBrush(Colors.Red);
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

            senderButton.BorderThickness = new Thickness(5);
            senderButton.Background = new SolidColorBrush(Colors.White);
            senderButton.FontWeight = FontWeights.ExtraBold;
            senderButton.BorderBrush = new SolidColorBrush(Colors.Black);
            Button team = (Button) sender;
            string videoUrl =  team.CommandParameter.ToString();

            if (currentDisplay != DisplayElement.Frame)
            {
                currentDisplay = DisplayElement.Frame;
                theDisplay.SetStyleAttribute("visibility", "hidden");
                PositionElement();
            }

            theDisplay.SetAttribute("src",videoUrl);
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



     

