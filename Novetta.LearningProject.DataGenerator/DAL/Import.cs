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
    class Import
    {
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;

        public Import()
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=password");
            database = connectionMultiplexer.GetDatabase();
        }

        public void ImportData(Data.IAssembler assembler)
        {
            var manager = new RedisManagerPool("localhost:6379");
            using (var client = manager.GetClient())
            {


//                assembler.


                //client.set("foo2", "bar2");
                //console.writeline("foo2={0}", client.get<string>("foo2"));

                //int iCounter = 0;
                //string[] dataTableNames = new string[_metaDataDTO.Data.Count];
                //_metaDataDTO.Data.ForEach(
                //    dataTable => {
                //        dataTableNames[iCounter] = dataTable.Rows[0][0].ToString();
                //        iCounter++;
                //    }
                //);

                //Dictionary<string, string> serializedData = new Dictionary<string, string>();

                //for (iCounter = 0; iCounter < _metaDataDTO.Data.Count; iCounter++)
                //{
                //    serializedData = SerializeData(_metaDataDTO.Data[iCounter]);

                //    HashEntry[] hashEntries = serializedData.Select(sd =>
                //    {
                //        return new HashEntry(sd.Key, sd.Value);
                //    }).ToArray();
                //    HashSet<HashEntry> hashSet = new HashSet<HashEntry>(hashEntries);

                //    client.Set(dataTableNames[iCounter], hashSet);
                //}
            }
        }


    }
}
