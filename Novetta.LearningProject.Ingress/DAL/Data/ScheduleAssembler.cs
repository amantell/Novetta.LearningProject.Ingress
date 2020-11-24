using System;
using StackExchange.Redis;
using System.Collections.Generic;
using ServiceStack.Redis;
using System.Data;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq;
using ServiceStack;

namespace Novetta.LearningProject.Ingress.DAL.Data
{
    class ScheduleAssembler : IAssembler
    {
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;
        List<string> _cities;
        List<string> _airlines;

        public ScheduleAssembler()
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=password");
            database = connectionMultiplexer.GetDatabase();

            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                _cities = client.GetAllItemsFromList("cities");
                _airlines = client.GetAllItemsFromList("airlines");
            }
        }

        public List<object> GetData()
        {
            List<object> flights = new List<object>(); 

            while (flights.Count < 360)
            {
                if (flights.Count % 2 == 0)
                {
                    flights.Add(GetArrival());
                }
                else
                {
                    flights.Add(GetDeparture());
                }
            }

            return flights;
        }

        private Arrival GetArrival()
        {
            DateTime[] startEndTimes = GetStartEndTimes();

            Random random = new Random();
            int fromIndex = random.Next(0, _cities.Count - 1);
            int toIndex = random.Next(0, _cities.Count - 1);

            while (toIndex == fromIndex)
            {
                toIndex = random.Next(0, _cities.Count - 1);
            };

            return new Arrival()
            {
                FromCity = _cities[fromIndex],
                ToCity = _cities[toIndex],
                FromTime = startEndTimes[0],
                ToTime = startEndTimes[1],
                Flight = GetAirline(),
                Notifications = GetNotification()
            };
        }

        private string GetNotification()
        {

            Random random = new Random();
            int result = random.Next(0, 4);

            if (result == 4)
            {
                return string.Format(@"The plane is delayed {0} minutes.", random.Next(0, 15).ToString());
            }
            return string.Empty;
        }

        private Departure GetDeparture()
        {
            DateTime[] startEndTimes = GetStartEndTimes();
            int[] cityIndexes = GetCityIndexes();

            return new Departure()
            {
                FromCity = _cities[cityIndexes[0]],
                ToCity = _cities[cityIndexes[1]],
                FromTime = startEndTimes[0],
                ToTime = startEndTimes[1],
                Flight = GetAirline(),
                Notifications = GetNotification()
            };
        }

        private int[] GetCityIndexes()
        {
            Random random = new Random();
            int fromIndex = random.Next(0, _cities.Count - 1);
            int toIndex = random.Next(0, _cities.Count - 1);

            while (toIndex == fromIndex)
            {
                toIndex = random.Next(0, _cities.Count - 1);
            };
            return new int[] { fromIndex, toIndex };
        }

        private string GetAirline()
        {
            Random random = new Random();
            return _airlines[random.Next(0, _airlines.Count - 1)];
        }

        private DateTime[] GetStartEndTimes()
        {
            Random random = new Random();

            int randomStartHour = random.Next(0, 23);
            int randomStartMinute = random.Next(0, 59);

            int randomEndHour = random.Next((randomStartHour == 23 ? 18 : randomStartHour), 23);
            int randomEndMinute = random.Next(0, 59);

            return new DateTime[]
            {
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, randomStartHour, randomStartMinute, 0),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, randomEndHour, randomEndMinute, 0),
            };
        }
    }
}
