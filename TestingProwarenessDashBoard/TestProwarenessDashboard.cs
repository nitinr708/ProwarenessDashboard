using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProwarenessDashboard;

namespace TestingProwarenessDashBoard
{
    [TestClass]
    public class TestProwarenessDashboard
    {
        
        [TestMethod]
        public void TestToCheckIfUrlIsAccessable()
        {
            String homeUrl = "http://localhost/Prowareness%20Dashboard/Prowareness%20DashboardTestPage.aspx";
            WebRequest webRequest = WebRequest.Create(homeUrl);
            WebResponse webResponse;
            webResponse = webRequest.GetResponse();
            Assert.IsNotNull(webResponse);
        }

        [TestMethod]
        public void TestToCheckListOfTeams()
        {
            List<Team> listOfTeams = Prowareness.GetTeams();
            Assert.IsNotNull(listOfTeams);
        }

        [TestMethod]
        public void TestToCheckTheQualityKpi()
        {
            Team team = new Team("Calvi", 20, 0.5, 20);
            Assert.AreEqual(team.quality, 20);
        }

        [TestMethod]
        public void TestToCheckTheVelocityKpi()
        {
            Team team = new Team("Calvi", 20, 0.5, 20);
            Assert.AreEqual(team.quality, 20);
        }

        [TestMethod]
        public void TestToCheckTheReliabilityKpi()
        {
            Team team = new Team("Calvi", 20, 0.5, 20);
            Assert.AreEqual(team.quality, 20);
        }



    }
    
}
