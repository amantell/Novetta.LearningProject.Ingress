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
    class TimeImporter : AImporter
    {
        public override void ImportData(Data.IAssembler assembler)
        {
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {
                List<object> data = assembler.GetData();
                client.AddRangeToList("times", data.Select(d => JsonConvert.SerializeObject(d)).ToList());
            }
        }
    }
}
