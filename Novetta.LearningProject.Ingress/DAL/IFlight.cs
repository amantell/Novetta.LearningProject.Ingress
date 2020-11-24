using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.Ingress.DAL
{
    interface IFlight
    {

        string Flight { get; set; }
        string FromCity { get; set; }
        DateTime FromTime { get; set; }
        string ToCity { get; set; }
        DateTime ToTime { get; set; }
        string Aircraft { get; set; }
        string Notifications { get; set; }
    }
}
