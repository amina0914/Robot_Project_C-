using System;
using RobbyTheRobot;
using System.Diagnostics;
using System.Threading.Tasks;


namespace RobbyIterationGenerator
{
    public class Program
    {
        public static Task t;
        public static void Main(string[] args)
        {
            //Here, there should be the way to stop the process if the user presses any key 
            //Stopwatch here is gonna be the total time of generating all the files
            //There will be the stopwatch for each generation file displayed in the console
            
            try{
            //If the user presses any key during the textfile generator
            ConsoleKeyInfo cki;
            Console.Clear();
            // Establish an event handler to process key press events.
            Console.CancelKeyPress += new ConsoleCancelEventHandler(abortingKey);
           
            //Declare the stopwatch
            Stopwatch stopWatch = new Stopwatch();
            Stopwatch totalWatch = new Stopwatch();
        
        
            Console.WriteLine("Welcome to RobbyTheRobot Game. Please enter some information to start to game process");
            Console.WriteLine("Note that if you press X, you will stop the generator and the total time will be printed out");
            
            Console.WriteLine("Enter the population size");
            int populationSize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the number of generations");
            int numGenerations = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the number of trials");
            int numberOfTrials = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the elite rate");
            double eliteRate = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the mutation rate");
            double mutationRate = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the folder directory");
            string path = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Generating the file reports....");

            

            Task.Run(() => {
                    cki = Console.ReadKey(true);
                    if(cki.Key == ConsoleKey.X){
                        notPressedX = false;
                        TimeSpan ts = stopWatch.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                        Console.WriteLine("Total time for generating the files is " + elapsedTime);
                        Environment.Exit(0);

                    }
                });
                    stopWatch.Start();
                    var robby = RobbyLib.CreateRobby(numGenerations, populationSize, numberOfTrials, mutationRate,eliteRate);
                    robby.FileWritten += PrintCurrentGeneration;
                    robby.GeneratePossibleSolutions(path);
                    stopWatch.Stop();

                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                    Console.WriteLine("Total time for generating the files is " + elapsedTime);
        }
        catch(Exception){
           Console.WriteLine("Aborting... \n Program Ended");
        }
    }
   
    public static void PrintCurrentGeneration(string gen){
        Console.WriteLine("Generation " + gen + " is generated");
    }
    }
}
