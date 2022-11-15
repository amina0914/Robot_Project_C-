using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
namespace GeneticAlgorithmTests
{
  [TestClass]
  public class GeneticAlgorithmTests
  {
    [TestMethod]
    public void TestConstructor()
    {
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(5,63,7,0.05,0.05,3,testcalc,7);
      IGeneration mygen=new Generation(alg,testcalc,7);
      Assert.AreEqual(mygen.NumberOfChromosomes, alg.PopulationSize);
      
    }
    public double testcalc(IChromosome chromo, IGeneration gen){
      double fitness=0;
      for(int i=0; i < chromo.Length;i++)
      {
        fitness+= chromo.Genes[i];
      }
      fitness=fitness/(double)chromo.Length;
      return fitness;}
  }
}