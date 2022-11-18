using System;
using RobbyTheRobot;
using System.Diagnostics;
using System.Threading;
// using LINQPad;

namespace RobbyIterationGenerator
{
    public class Program
    {

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
        
        
            Console.WriteLine("Welcome to RobbyTheRobot Game. Please enter some information to start to game process");
            Console.WriteLine("Please note that if you press 'X' key when generating the file, the file generations report about Robby will be stopped and the folder will contain the generation reports up till the time you stopped");
            Console.WriteLine("If you press 'CTRL C, you will halt the program");
            
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

            // bool notPressedX = true;

            // while(true){
            //         cki = Console.ReadKey(true);
                    // if(cki.Key == ConsoleKey.X){
                    //     notPressedX = false;
                    // }
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
                    // break;
                // }
            
              
                    // TimeSpan ts = stopWatch.Elapsed;
                    // // Format and display the TimeSpan value.
                    // string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    // ts.Hours, ts.Minutes, ts.Seconds,
                    // ts.Milliseconds / 10);
                    // Console.WriteLine("Total time for generating the files is " + elapsedTime);
        }
        catch(Exception){
           Console.WriteLine("Error \n Program Ended");
        }
    }
    public static void abortingKey(object sender, ConsoleCancelEventArgs args){
        Console.WriteLine("\nThe generatign operation has been interrupted.");
        Console.WriteLine($"You have pressed : {args.SpecialKey}");
        Console.WriteLine("Are you sure that you would like to quit the file generator? (y/n)");
        string answer = Convert.ToString(Console.ReadLine());
        //if "n" the program will continue
        if(answer == "n"){
             args.Cancel = true;
            Console.WriteLine("The read operation will resume...\n");
        }
        else{
            args.Cancel = false;
            Console.WriteLine("The read operation has been discontinued. Please check the folder you have entered for the report files");
        }
    }

    public static void PrintCurrentGeneration(string gen){
        Console.WriteLine("Generation " + gen + " is generated");
    }
    }
}
