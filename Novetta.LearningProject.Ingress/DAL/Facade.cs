using System;
using StackExchange.Redis;
using System.Collections.Generic;
using ServiceStack.Redis;
using System.Data;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq;
using ServiceStack;

namespace Novetta.LearningProject.Ingress.DAL
{
    public class Facade
    {
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;
        ISubscriber subscriber;

        private bool _isDataUpdated = false;
        public bool IsDataUpdated
        {
            get { return _isDataUpdated; } 
            set { _isDataUpdated = value; }
        }

        public Facade()
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=password");
            subscriber = connectionMultiplexer.GetSubscriber();
            subscriber.Subscribe("messages", (channel, message) => {
                Console.WriteLine((string)message);
                _isDataUpdated = true; 
            });
        }

        public void Shutdown()
        {
            connectionMultiplexer.Close();
        }

        public Dictionary<string, List<string>> GetScheduleData(DateTime now)
        {
            Dictionary<string, List<string>> schedules = new Dictionary<string, List<string>>();

            schedules.Add("arrivals", new List<string>());
            schedules.Add("departures", new List<string>());

            database = connectionMultiplexer.GetDatabase();

            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                var filteredArrivals = Filter(client.GetAllItemsFromList("arrivals"), now);
                var filteredDepartures = Filter(client.GetAllItemsFromList("departures"), now);

                schedules["arrivals"].AddRange(filteredArrivals);
                schedules["departures"].AddRange(filteredDepartures);
            }

            return schedules;
        }

        private List<string> Filter(List<string> flights, DateTime now)
        {
            List<string> result = new List<string>();

            for (int iCounter = 0; iCounter < flights.Count; iCounter++)
            {
                var flightData = flights[iCounter].Split(',').ToList();
                string fromtime = flightData.Where(f => f.ToLower().Contains("fromtime")).FirstNonDefaultOrEmpty();
                if (string.IsNullOrWhiteSpace(fromtime)) continue;

                string hourString = fromtime.Split(':')[1].Split('T')[1];

                if (int.TryParse(hourString, out int hour) && hour >= now.Hour - 2 && hour <= now.Hour + 5)
                {
                    result.Add(flights[iCounter]);
                } else
                {
                    continue;
                }
            }
            return result;
        }
    }
}
