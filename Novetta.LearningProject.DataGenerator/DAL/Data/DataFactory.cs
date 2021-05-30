﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.DataGenerator.Data
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
                case "time":
                    return new TimeAssembler();
            }
            throw new Exception("No assembler returned.");
        }
    }
}
