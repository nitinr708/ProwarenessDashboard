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
        public static List<Team> GetTeams()
        {
            List<Team> teamsList = new List<Team>();
            teamsList.Add(new Team("CALVI", 12, 20, 80,"http://192.168.1.201/view/viewer_index.shtml?id=5"));
            teamsList.Add(new Team("Yes Telecom", 17, 21, 90, "http://192.168.0.30/view/viewer_index.shtml?id=11"));
            teamsList.Add(new Team("Effectory", 25, 25, 20,""));
            teamsList.Add(new Team("Bearing Point", 19, 20, 55, "http://192.168.1.201/view/viewer_index.shtml?id=5"));
            teamsList.Add(new Team("AMS-IX", 28, 28, 27, "http://192.168.0.30/view/viewer_index.shtml?id=11"));
            return teamsList;
        }
    }
}
