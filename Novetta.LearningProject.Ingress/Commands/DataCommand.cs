using System;
using System.Collections.Generic;
using System.Text;

namespace Novetta.LearningProject.Ingress.Commands
{
    class DataCommand : ACommand
    {
        DAL.Facade _facade;
        public Dictionary<string, List<string>> Data { get; set; }

        public DataCommand()
        {
            _facade = new DAL.Facade();
        }

        public override bool Validate()
        {
            try
            {
                Data = _facade.GetScheduleData(DateTime.Now);
                if (Data == null) throw new Exception("no results were stored in the database.");

                PublishCommand publishCommand = new PublishCommand(Data);
                return publishCommand.Validate();
            
            } catch (Exception exception)
            {
                Error = exception.Message;
                return false;
            }
        }

    }
}
