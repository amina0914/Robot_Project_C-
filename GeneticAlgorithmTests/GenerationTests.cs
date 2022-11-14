using System;
using GeneticAlgorithm;
namespace GeneticAlgorithmTests
{
  [TestClass]
  public class GenerationTests
  {
    [TestMethod]
    public void TestConstructor()
    {
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(5,63,7,0.05,0.05,3,testcalc,7);
      IGeneration mygen=new Generation(alg,testcalc,7);
      Assert.AreEqual(mygen.NumberOfChromosomes, alg.PopulationSize);
      
    }
    public double testcalc(IChromosome chromo, IGeneration gen){
      Random rand=new Random(5);
      return (double)(rand.Next(1,10));}
    [TestMethod]
    public void TestCopyConstructor()
    {
      Chromosome chromo = new Chromosome(63, 7, 4);
      Chromosome spouse = new Chromosome(63, 7, 5);
      //Generating the Child
      IChromosome[] childs = chromo.Reproduce(spouse, 0);
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(2,63,7,0.05,0.05,3,testcalc,7);
      IGeneration mygen=new Generation((childs as Chromosome[]),alg);
      Assert.AreEqual(mygen.NumberOfChromosomes, alg.PopulationSize);
      Assert.AreEqual(mygen[0], childs[0]);
      Assert.AreEqual(mygen[1], childs[1]);
      
      
    }
    [TestMethod]
    public void TestIndexer()
    {
       Chromosome chromo = new Chromosome(63, 7, 4);
      Chromosome spouse = new Chromosome(63, 7, 5);
      //Generating the Child
      IChromosome[] childs = chromo.Reproduce(spouse, 0);
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(2,63,7,0.05,0.05,3,testcalc,7);
      IGeneration mygen=new Generation((childs as Chromosome[]),alg);
      IChromosome test1=mygen[0];
      IChromosome test2=mygen[1];
      Assert.AreEqual(mygen[0],test1);
      Assert.AreEqual(mygen[1], test2);

    }
    [TestMethod]
    public void TestReproduce()
    {
      //Testing the Reproduce Method With seed 4 and 5 for respective parent
      Chromosome chromo = new Chromosome(63, 7, 4);
      Chromosome spouse = new Chromosome(63, 7, 5);
      //Generating the Child
      IChromosome[] childs = chromo.Reproduce(spouse, 0);
      int[] pointa = { 5, 6, 3, 2, 0, 0, 4, 3 };
      int pointb = 5;
      Console.WriteLine("chromo");
      bool pointacheck = false;
      bool pointendcheck = pointb == childs[0].Genes[62];
      for (int i = 0; i < pointa.Length; i++)
      {
        if (chromo[i] == childs[0].Genes[i])
        {
          pointacheck = true;
        }
        else
        {
          pointacheck = false;
        }
      }
      bool betweenpointcheck = false;
      for (int i = 9; i < spouse.Length - 2; i++)
      {
        if (spouse[i] == childs[0].Genes[i])
        {
          betweenpointcheck = true;
        }
        else
        {
          betweenpointcheck = false;
        }
      }


      int[] pointachilds2check = { 2, 1, 1, 4, 3, 6, 1, 6 };
      int pointbchild2 = 0;
      Console.WriteLine("chromo");
      bool pointachild2check = false;
      bool pointendchild2check = pointbchild2 == childs[1].Genes[62];

      for (int i = 0; i < pointachilds2check.Length; i++)
      {
        if (spouse[i] == childs[1].Genes[i])
        {
          pointachild2check = true;
        }
        else
        {
          pointachild2check = false;
        }
      }
      bool betweenpointchild2check = false;
      for (int i = 9; i < chromo.Length - 2; i++)
      {
        if (chromo[i] == childs[1].Genes[i])
        {
          betweenpointchild2check = true;
        }
        else
        {
          betweenpointchild2check = false;
        }
      }
      Assert.IsTrue(pointacheck);
      Assert.IsTrue(pointendcheck);
      Assert.IsTrue(betweenpointcheck);
      Assert.IsTrue(pointachild2check);
      Assert.IsTrue(pointendchild2check);
      Assert.IsTrue(betweenpointchild2check);
    }
  }
}

