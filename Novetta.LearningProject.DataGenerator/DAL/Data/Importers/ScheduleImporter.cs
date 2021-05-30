using System;
using StackExchange.Redis;
using System.Collections.Generic;
using ServiceStack.Redis;
using System.Data;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq;
using ServiceStack;

namespace Novetta.LearningProject.DataGenerator.Data.Importers
{
    class ScheduleImporter : AImporter
    {
        public ScheduleImporter(ConnectionMultiplexer multiplexer)
        {
            connectionMultiplexer = multiplexer;
        }

        public override void ImportData(Data.IAssembler assembler)
        {
            IDatabase cache = connectionMultiplexer.GetDatabase();
            List<object> data = assembler.GetData();

            for (int iCounter = 0; iCounter < data.Count; iCounter++)
            {
                if (data[iCounter] is Arrival arrival)
                {
                    cache.StringSet("arrivals", JsonConvert.SerializeObject(arrival));
                }
                else if (data[iCounter] is Departure departure)
                {
                    cache.StringSet("departures", JsonConvert.SerializeObject(departure));
                }
            }
        }
    }
}
