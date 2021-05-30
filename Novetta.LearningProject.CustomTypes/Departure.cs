using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.CustomTypes
{
    public class Departure : IFlight
    {
        public string Flight { get; set; }
        public string FromCity { get; set; }
        public DateTime FromTime { get; set; }
        public string ToCity { get; set; }
        public DateTime ToTime { get; set; }
        public string Aircraft { get; set; }
        public string Notifications { get; set; }    
    }
}
