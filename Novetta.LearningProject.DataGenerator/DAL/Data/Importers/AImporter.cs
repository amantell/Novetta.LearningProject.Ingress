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
    public abstract class AImporter
    {
        protected ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;

        public virtual void ImportData(Data.IAssembler assembler) {}
    }
}
