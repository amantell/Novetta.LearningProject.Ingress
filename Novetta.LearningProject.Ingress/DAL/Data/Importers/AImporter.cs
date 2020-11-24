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
    public abstract class AImporter
    {
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;

        //public Import()
        //{
        //    connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=password");
        //    database = connectionMultiplexer.GetDatabase();
        //}

        public virtual void ImportData(Data.IAssembler assembler)
        {

        }
    }
}
