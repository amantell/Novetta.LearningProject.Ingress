using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.Ingress.DAL.Data
{
    class AirlineAssembler : IAssembler
    {
        public List<object> GetData()
        {
            return new List<object>()
            {
                "Alaskan Airlines",
                "Air Japan",
                "Delta",
                "Air Lingus",
                "Air France",
                "Aroflot",
                "United"
            };
        }
    }
}
