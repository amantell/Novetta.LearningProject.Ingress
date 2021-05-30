using System;

namespace Novetta.LearningProject.DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Facade _facade = new Facade();

            Console.WriteLine("Starting Data Generator");
            Console.WriteLine("Type any alphanumeric key and then hit 'Enter' to end the program.");

            bool isRunning = true;

            while (isRunning)
            {
                if (Console.ReadLine() != string.Empty) isRunning = false;
            };
            _facade.SendMessage();

            _facade.Shutdown();

            Console.WriteLine("Closing Data Generator");
        }
    }
}
