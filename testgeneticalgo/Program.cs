using System;
using GeneticAlgorithm;
using RobbyTheRobot;
namespace testgeneticalgo
{
  class Program
  {
    static void Main(string[] args)
    {
     
     
  
     IRobbyTheRobot robot= Robby.CreateRobby(5,200,100);
      
      Console.WriteLine("Robby Generation One ");    
      Console.WriteLine("Rolling 1000 Gens");
      for(int i=0; i <1000; i ++)
      {
        robot.GeneticA.GenerateGeneration();
        if(i==0 ||i==19 ||i==99 ||i==499 ||i==999){
        Console.WriteLine("Robby Generation: "+ (i+1));
      Console.WriteLine("Numm Of Chromosomes In  "+(i+1)+" generation inside Robby " +robot.GeneticA.CurrentGeneration.NumberOfChromosomes);
      Console.WriteLine("MAX fitness in robby "+ robot.GeneticA.CurrentGeneration.MaxFitness);
      Console.WriteLine("*********************************************************************************************");
        }
         
      }
        

    }

  
  }
}
