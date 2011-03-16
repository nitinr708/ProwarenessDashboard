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

            Label Label = new Label() { Content = "Team -" };
            Label lblVelocity = new Label() { Content = "Velocity:" };
            Label lblQuality = new Label() { Content = "Quality:" };
            Label lblReliability = new Label() { Content = "Reliability:" };
            Label lblTeamName = new Label() { Content = team.name.ToString() };
            Label lblTeamVelocity = new Label() { Content = team.velocity.ToString() };
            Label lblTeamQuality = new Label() { Content = team.quality.ToString() };
            Label lblTeamReliability = new Label() { Content = team.reliability.ToString() };


            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(15) });
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

            btnTeam.Margin = new Thickness(0, 0, 0, 5);

            btnTeam.Content = grid;

            LoadRightTopPanel(team);
            loadVideoPanel(team);
            btnTeam.CommandParameter = team.videoUrl;
            TeamsListStackPanel.Children.Add(btnTeam); 
        }

        public void loadVideoPanel(Team team)
        {
            btnTeam.Click += new RoutedEventHandler(StartButton_Click);
            
        }



        private void LoadRightTopPanel(Team team)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
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



     

