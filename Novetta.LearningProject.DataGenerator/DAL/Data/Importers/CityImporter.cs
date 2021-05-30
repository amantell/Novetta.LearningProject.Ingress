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
    class CityImporter : AImporter
    {
        public CityImporter(ConnectionMultiplexer multiplexer)
        {
            connectionMultiplexer = multiplexer;
        }

        public override void ImportData(Data.IAssembler assembler)
        {
            IDatabase cache = connectionMultiplexer.GetDatabase();
            List<object> data = assembler.GetData();
            cache.StringSet("cities", data.Select(d => JsonConvert.SerializeObject(d)).ToString());
        }
    }
}
