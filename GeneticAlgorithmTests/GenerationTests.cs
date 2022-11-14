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
      Random rand=new Random(4);
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
    public void TestSelectParent()
    {
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(27,10,7,0.05,0.05,3,testcalc,7);
      IGeneration mygen=new Generation(alg,testcalc,7);
      (mygen as IGenerationDetails).EvaluateFitnessOfPopulation();
      IChromosome potentialparent=(mygen as IGenerationDetails).SelectParent();
      foreach(Int32 x in potentialparent.Genes)
      {
        Console.Write(x+ " ");
      }
      Assert.AreEqual(mygen[0].Fitness,8);
      Assert.AreEqual(mygen.MaxFitness,8);
      Assert.AreEqual(mygen.AverageFitness,8);   
    }
  }
}

