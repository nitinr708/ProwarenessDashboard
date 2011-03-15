using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;


namespace ProwarenessDashboard
{
    public class Prowareness
    {
        public List<Team> GetTeams()
        {
            List<Team> teamsList = new List<Team>();
            teamsList.Add(new Team("CALVI", 12, 20, 80));
            teamsList.Add(new Team("Yes Telecom", 17, 21, 90));
            teamsList.Add(new Team("Effectory", 25, 25, 20));
            teamsList.Add(new Team("Bearint Point", 19, 20, 55));
            teamsList.Add(new Team("AMS-IX", 28, 28, 27));
            return teamsList;
        }
    }
}
