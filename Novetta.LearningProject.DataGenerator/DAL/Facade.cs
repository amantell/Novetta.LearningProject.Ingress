using System;
using StackExchange.Redis;
using System.Collections.Generic;
using ServiceStack.Redis;
using System.Data;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq;
using ServiceStack;

namespace Novetta.LearningProject.DataGenerator
{
    public class Facade
    {
        ConnectionMultiplexer connectionMultiplexer;
        Data.DataFactory dataFactory;
        Data.IAssembler assembler;
        ISubscriber subscriber;

        public Facade()
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            //Initialize();
            //GenerateData();
        }

        public void SendMessage()
        {
            subscriber = connectionMultiplexer.GetSubscriber();
            subscriber.Publish("messages", "Sending a published message worked!");
        }

        public void Shutdown()
        {
            connectionMultiplexer.Close();
        }

        private void Initialize()
        {
            try
            {
                IDatabase cache = connectionMultiplexer.GetDatabase();
                cache.KeyDelete("airlines");
                cache.KeyDelete("cities");
                cache.KeyDelete("arrivals");
                cache.KeyDelete("departures");
            } catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void GenerateData()
        {
            dataFactory = new Data.DataFactory();

            assembler = dataFactory.GetAssembler("city");
            Data.Importers.AImporter importer = new Data.Importers.CityImporter(connectionMultiplexer);
            importer.ImportData(assembler);

            assembler = dataFactory.GetAssembler("airline");
            importer = new Data.Importers.AirlineImporter(connectionMultiplexer);
            importer.ImportData(assembler);

            assembler = dataFactory.GetAssembler("schedule");
            importer = new Data.Importers.ScheduleImporter(connectionMultiplexer);
            importer.ImportData(assembler);
        }
    }
}
