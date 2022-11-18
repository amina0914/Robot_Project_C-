using System;
using RobbyTheRobot;
using System.Diagnostics;
using System.Threading;
namespace RobbyIterationGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try{
            
            Stopwatch stopWatch = new Stopwatch();
        
            Console.WriteLine("Hello World");
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
            // Console.WriteLine("Enter the number of genes");
            // int numberOfGenes = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine("Enter the length of genes");
            // int lengthOfGenes = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the folder directory");
            string path = Convert.ToString(Console.ReadLine());
            stopWatch.Start();
            //The problem is here
            var robby = RobbyLib.CreateRobby(numGenerations, populationSize, numberOfTrials, mutationRate,eliteRate);
            robby.GeneratePossibleSolutions(path);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

        }
        catch(Exception){
            Console.WriteLine("Error");
        }

        }
           

        
        


    }
}
