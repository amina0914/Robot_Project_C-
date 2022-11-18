<<<<<<< HEAD
=======
﻿/**
@author: Octavio Abel Ganchozo Paladines 
@student id: 1539613
*/
>>>>>>> main
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
namespace GeneticAlgorithmTests
{
  [TestClass]
  public class GenerationTests
  {
    [TestMethod]
    public void TestConstructor()
    {
<<<<<<< HEAD
=======
      //Testing the constructor
>>>>>>> main
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
      return fitness;}//add the genes instead
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
<<<<<<< HEAD
=======
      //Generating population chromosomes
>>>>>>> main
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(4,10,7,0.05,0.05,3,testcalc,7);
      Chromosome chromo = new Chromosome(63, 7, 4);
      Chromosome chromo2 = new Chromosome(63, 7, 2);
      Chromosome chromo3 = new Chromosome(63, 7, 1);
      Chromosome chromo4 = new Chromosome(63, 7, 6);
      Chromosome[] chromos= {chromo,chromo2,chromo3,chromo4};
      IGeneration mygen=new Generation(chromos,alg);
      (mygen as IGenerationDetails).EvaluateFitnessOfPopulation();
      IChromosome potentialparent=(mygen as IGenerationDetails).SelectParent();
<<<<<<< HEAD
     
=======
      //Checking if potential parent has the same vals
>>>>>>> main
      Assert.AreEqual(mygen[0].Fitness,3.0476190476190474);
      Assert.AreEqual(mygen.MaxFitness,3.0476190476190474);
      Assert.AreEqual(potentialparent.Fitness,mygen[0].Fitness);   
    }

     [TestMethod]
    public void TestEvaluationFitness()
    {
<<<<<<< HEAD
=======
      //Generating Generation
>>>>>>> main
      IGeneticAlgorithm alg= GeneticLib.CreateGeneticAlgorithm(4,10,7,0.05,0.05,3,testcalc,7);
      Chromosome chromo = new Chromosome(63, 7, 4);
      Chromosome chromo2 = new Chromosome(63, 7, 2);
      Chromosome chromo3 = new Chromosome(63, 7, 1);
      Chromosome chromo4 = new Chromosome(63, 7, 6);
<<<<<<< HEAD
=======
      //Population to be evaluated
>>>>>>> main
      Chromosome[] chromos= {chromo,chromo2,chromo3,chromo4};
      IGeneration mygen=new Generation(chromos,alg);
      (mygen as IGenerationDetails).EvaluateFitnessOfPopulation();
      Assert.AreEqual(mygen[0].Fitness,3.0476190476190474);
      Assert.AreEqual(mygen[1].Fitness,2.984126984126984);
      Assert.AreEqual(mygen[2].Fitness,2.8412698412698414);
      Assert.AreEqual(mygen[3].Fitness,2.73015873015873);
      Assert.AreEqual(mygen.NumberOfChromosomes,4);
      Assert.AreEqual(mygen.AverageFitness,2.9007936507936507);   
    }
  }
}

