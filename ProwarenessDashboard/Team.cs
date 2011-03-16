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
        public double reliability { get; set; }
        public int quality { get; set; }
        public string videoUrl { get; set; }
       
        public Team(string nameVal, int velocityVal, double reliabilityVal, int qualityVal, string videoUrlVal)
        {
            name = nameVal;
            velocity = velocityVal;
            reliability = reliabilityVal;
            quality = qualityVal;
            videoUrl = videoUrlVal;
          
        }
        public Team()
        {
            //name = nameVal;
            //velocity = velocityVal;
            //reliability = reliabilityVal;
            //quality = qualityVal;
        }

       
    }
}
