using System;
using System.Collections.Generic;

namespace Novetta.LearningProject.Ingress
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            Commands.DataCommand dataCommand = new Commands.DataCommand();

            while (now.Hour < 23) {
                //if (now.Minute % 2 == 0 && now.Second == 0)
                if (now.Second == 0 || now.Second == 30)
                {
                    if (dataCommand.Validate())
                    {
                        Console.WriteLine("Successful data ingress");
                    }
                    else
                    {
                        Console.WriteLine(dataCommand.Error);
                    }
                }
                now = DateTime.Now;
            };
        }
    }
}
