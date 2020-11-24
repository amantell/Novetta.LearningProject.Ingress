using System;
using StackExchange.Redis;
using System.Collections.Generic;
using ServiceStack.Redis;
using System.Data;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq;
using ServiceStack;

namespace Novetta.LearningProject.Ingress.DAL.Data.Importers
{
    class ScheduleImporter : AImporter
    {
        public override void ImportData(Data.IAssembler assembler)
        {
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                List<object> data = assembler.GetData();

                for (int iCounter = 0; iCounter < data.Count; iCounter++)
                {
                    if (data[iCounter] is Arrival arrival)
                    {
                        client.AddItemToList("arrivals", JsonConvert.SerializeObject(arrival));
                    } else if (data[iCounter] is Departure departure)
                    {
                        client.AddItemToList("departures", JsonConvert.SerializeObject(departure));
                    }
                }
            }
        }
    }
}
