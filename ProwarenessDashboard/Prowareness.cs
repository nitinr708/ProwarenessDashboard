using System.Collections.Generic;


namespace ProwarenessDashboard
{
    public class Prowareness
    {
        public static List<Team> GetTeams()
        {
            List<Team> teamsList = new List<Team>();
            teamsList.Add(new Team("CALVI Team (IN)", 12, 20, 80,"http://192.168.1.201/view/viewer_index.shtml?id=5"));
            teamsList.Add(new Team("Prowareness Sales Team (NL)", 28, 28, 27, "http://192.168.0.30/view/viewer_index.shtml?id=11"));
            return teamsList;
        }
    }
}
