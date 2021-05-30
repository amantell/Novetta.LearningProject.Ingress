using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.DataGenerator.Data
{
    class TimeAssembler : IAssembler
    {
        public List<object> GetData()
        {
            List<object> result = new List<object>();
            for (int iCounter = 0; iCounter <= 23; iCounter++)
            {
                for (int jCounter = 0; jCounter <= 59; jCounter++)
                {
                    result.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, iCounter, jCounter, 0));
                }
            }
            return result;
        }
    }
}
