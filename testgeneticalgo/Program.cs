using System;
using GeneticAlgorithm;
namespace testgeneticalgo
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }

    public static void chromosome()
    {
      Console.WriteLine("Testing Chromosome Class");
      Console.WriteLine("Generating chromosomes .....");
      IChromosome testa = new Chromosome(20, 20, 6);
      IChromosome testb = new Chromosome(20, 20, 6);
      Console.WriteLine("Generating Childs");
      IChromosome[] mychilds = testa.Reproduce(testb, 0.1);
      IChromosome resutla = mychilds[0];
      IChromosome resutl2b = mychilds[1];
      Console.WriteLine("Pringting Parent 1");
      foreach (Int32 a in testa.Genes)
      {
        Console.Write(a + " ");
      }
      Console.WriteLine("");

      Console.WriteLine("Pringting Child 1");

      foreach (Int32 a in resutla.Genes)
      {
        Console.Write(a + " ");
      }
      Console.WriteLine("");
      Console.WriteLine("Pringting Parent 2");
      foreach (Int32 a in testb.Genes)
      {
        Console.Write(a + " ");
      }
      Console.WriteLine("");


      Console.WriteLine("Pringting Child2 1");
      foreach (Int32 a in resutl2b.Genes)
      {
        Console.Write(a + " ");
      }
      Console.WriteLine("");
    }

    public static void generation()
    {
      Console.WriteLine("Testing Generation Class");
      Console.WriteLine("Generating Generation .....");
      IChromosome testa = new Chromosome(20, 20, 6);
      IChromosome testb = new Chromosome(20, 20, 6);
      Console.WriteLine("Generating Childs");
      IChromosome[] mychilds = (testa.Reproduce(testb, 0.1));
      Generation mygeneration = new Generation(mychilds as Chromosome[]);

      Console.WriteLine("Testing Properties .....");
      Console.WriteLine("Pringting Average Fitness");
      Console.WriteLine("Pringting Average Fitness =" + mygeneration.AverageFitness);
      Console.WriteLine("Pringting Max Fitness");
      Console.WriteLine("Pringting Max Fitness =" + mygeneration.MaxFitness);
      Console.WriteLine("Pringting Number Of chromosomes in Generation =" + mygeneration.NumberOfChromosomes);
      Console.WriteLine("Pringting Chromosome at 0 in Generation");
      foreach (Int32 a in mygeneration[0].Genes)
      {
        Console.Write(a + " ");
      }
      Console.WriteLine("");
      Console.WriteLine("Pringting Chromosome at 1 in Generation");
      foreach (Int32 a in mygeneration[1].Genes)
      {
        Console.Write(a + " ");
      }
      Console.WriteLine("");
      Console.WriteLine("Selecting a Random Parent");
      IChromosome testc = mygeneration?.SelectParent();
      if (testc != null)
      {
        Console.WriteLine("Pringting the parent and Its Properties");
        foreach (Int32 a in testc.Genes)
        {
          Console.Write(a + " ");

        }
        Console.WriteLine("");
        Console.WriteLine("Print Fitness= " + testc.Fitness + " lenght genes= " + testc.Length);
      }


    }

    public static void alggeneration()
    {
      FitnessEventHandler fitnessCalculation = null;
      GeneticAlgorithm test = GeneticLib.CreateGeneticAlgorithm(14, 20, 7, 0.3, 0.5, 3, fitnessCalculation, null) as GeneticAlgorithm;
      Console.WriteLine("CREATING A RANDOM GENERATION FROM SCRATCH");
      Generation testb = test.GenerateGeneration() as Generation;
      if (testb == null)
      {
        Console.WriteLine("Generation is NUll");
      }

      Console.WriteLine("Print #ofChromosomes= " + testb.NumberOfChromosomes);
      Console.WriteLine("Print Random Parant and Its genes");
      Random rand = new Random();
      for (int i = 0; i < testb.NumberOfChromosomes; i++)
      {
        (testb[i] as Chromosome).Fitness = rand.Next(1, 10);
      }
      for (int i = 0; i < testb.NumberOfChromosomes; i++)
      {
        Console.WriteLine("Printing Chromose at GenerationB[" + i + "]");
        foreach (Int32 a in testb[i].Genes)
        {
          Console.Write(a + " ");
        }

        Console.WriteLine("");

      }

      Chromosome testd = testb?.SelectParent() as Chromosome;
      if (testd is null)
      {
        Console.WriteLine("Chromosme is NUll");
      }
      Console.WriteLine("Printing Random Chromosome Selected for test");
      for (int i = 0; i < testd.Length; i++)
      {
        Console.Write(testd[i] + " ");

        //  Console.WriteLine(""); 

      }
      Console.WriteLine("");
      Console.WriteLine("Calling Test generation on itself to replace the random one");
      test.GenerateGeneration();
      Console.WriteLine("Generating the Next Generation C");
      Generation sample = test.CurrentGeneration as Generation;
      Generation testc = test.CurrentGeneration as Generation;
      for (int i = 0; i < testb.NumberOfChromosomes; i++)
      {
        Console.WriteLine("Printing Chromose at GenerationC[" + i + "]");
        foreach (Int32 a in testb[i].Genes)
        {
          Console.Write(a + " ");
        }

        Console.WriteLine("");

      }
      Console.WriteLine("Test Select Parent With Generation C");
      Chromosome testf = testc?.SelectParent() as Chromosome;
      if (testd is null)
      {
        Console.WriteLine("Chromosme is NUll");
      }

      for (int i = 0; i < testf.Length; i++)
      {
        Console.Write(testd[i] + " ");
        //  Console.WriteLine(""); 

      }

    }
  }
}
