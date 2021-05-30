using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.DataGenerator.Data
{
    class CityAssembler : IAssembler
    {
        public List<object> GetData()
        {
            return new List<object>()
            {
                "New York",
                "London",
                "Tokyo",
                "Paris",
                "Los Angeles",
                "Buenos Aires",
                "Capetown"
            };
        }
    }
}
