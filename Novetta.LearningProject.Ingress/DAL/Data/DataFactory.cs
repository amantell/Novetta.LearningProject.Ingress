using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.Ingress.DAL.Data
{
    public class DataFactory
    {
        public IAssembler GetAssembler(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "city":
                    return new CityAssembler();
                case "airline":
                    return new AirlineAssembler();
                case "schedule":
                    return new ScheduleAssembler();
            }
            throw new Exception("No assembler returned.");
        }
    }
}
