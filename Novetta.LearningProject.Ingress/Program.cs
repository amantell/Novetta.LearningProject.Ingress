using System;
using System.Collections.Generic;

namespace Novetta.LearningProject.Ingress
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Ingress");
            Console.WriteLine("Type any alphanumeric key and then hit 'Enter' to end the program.");

            DAL.Facade _facade = new DAL.Facade();

            bool isRunning = true;

            while (isRunning)
            {
                if (Console.ReadLine() != string.Empty) isRunning = false;
                if (_facade.IsDataUpdated)
                {
                    try
                    {
                        Dictionary<string, List<string>> Data = _facade.GetScheduleData(DateTime.Now);
                        if (Data == null) throw new Exception("no results were stored in the database.");

                        Commands.PublishCommand publishCommand = new Commands.PublishCommand(Data);
                        publishCommand.Validate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally {
                        _facade.IsDataUpdated = false;
                    }
                }
            };

            _facade.Shutdown();
            Console.WriteLine("Closing Ingress");
        }
    }
}
