using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProwarenessDashboard
{
    public class Team
    {
        public string name { get; set; }
        public int velocity { get; set; }
        public int reliability { get; set; }
        public int quality { get; set; }

        public Team(string nameVal, int velocityVal, int reliabilityVal, int qualityVal)
        {
            name = nameVal;
            velocity = velocityVal;
            reliability = reliabilityVal;
            quality = qualityVal;
        }

        public static List<Team> GetList()
        {
            List<Team> teamsList = new List<Team>();
            teamsList.Add(new Team("CALVI", 20, 20, 80));
            teamsList.Add(new Team("Yes Telecom", 17, 21, 90));
            teamsList.Add(new Team("Effectory", 25, 25, 20));
            teamsList.Add(new Team("Bearint Point", 19, 20, 55));
            teamsList.Add(new Team("AMS-IX", 28, 28, 27));
            return teamsList;
        }
    }
}
