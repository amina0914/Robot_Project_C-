/**
@author: Octavio Abel Ganchozo Paladines 
@student id: 1539613
*/
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
      IGeneticAlgorithm alg = GeneticLib.CreateGeneticAlgorithm(5, 63, 7, 0.05, 0.05, 3, testcalc, 7);
      Assert.AreEqual(5, alg.PopulationSize);
      Assert.AreEqual(63, alg.NumberOfGenes);
      Assert.AreEqual(7, alg.LengthOfGene);
      Assert.AreEqual(0.05, alg.MutationRate);
      Assert.AreEqual(0.05, alg.EliteRate);
      Assert.AreEqual(3, alg.NumberOfTrials);
      Assert.AreEqual(testcalc, alg.FitnessCalculation);



    }
    public double testcalc(IChromosome chromo, IGeneration gen)
    {
      double fitness = 0;
      for (int i = 0; i < chromo.Length; i++)
      {
        fitness += chromo.Genes[i];
      }
      fitness = fitness / (double)chromo.Length;
      return fitness;
    }
    [TestMethod]
    public void TestGenerateGeneration1()
    {
      IGeneticAlgorithm alg = GeneticLib.CreateGeneticAlgorithm(200, 63, 7, 0.05, 0.05, 3, testcalc, 7);
      alg.GenerateGeneration();
      alg.GenerateGeneration();
      IGeneration testgen= alg.GenerateGeneration();

      Assert.AreEqual(3, alg.GenerationCount);
      Assert.AreEqual(testgen, alg.CurrentGeneration);
      Assert.AreEqual(testgen.NumberOfChromosomes, alg.CurrentGeneration.NumberOfChromosomes);
      Assert.AreEqual(testgen.AverageFitness, alg.CurrentGeneration.AverageFitness);
    }
     [TestMethod]
    public void TestGenerateGeneration5()
    {
      IGeneticAlgorithm alg = GeneticLib.CreateGeneticAlgorithm(200, 63, 7, 0.05, 0.05, 3, testcalc, 8);
       IGeneration testgen;
      for(int i=0; i <4;i++){
        alg.GenerateGeneration();
      }
       testgen= alg.GenerateGeneration();

      Assert.AreEqual(5, alg.GenerationCount);
      Assert.AreEqual(testgen, alg.CurrentGeneration);
      Assert.AreEqual(testgen.NumberOfChromosomes, alg.CurrentGeneration.NumberOfChromosomes);
      Assert.AreEqual(testgen.AverageFitness, alg.CurrentGeneration.AverageFitness);
    }

  }


}