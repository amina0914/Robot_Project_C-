using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;
namespace GeneticAlgorithmTests
{
  [TestClass]
  public class ChromosomeTests
  {
    [TestMethod]
    public void TestConstructor()
    {
      Chromosome chromo = new Chromosome(243, 7);
      Assert.AreEqual(243, chromo.Length);
    }
    [TestMethod]
    public void TestCopyConstructor()
    {
      Chromosome chromo = new Chromosome(243, 7, 4);
      Chromosome copy = new Chromosome(chromo);
      Assert.AreEqual(copy.Length, chromo.Length);
      Assert.AreEqual(copy[0], chromo[0]);
      Assert.AreEqual(copy[7], chromo[7]);
    }
    [TestMethod]
    public void TestIndexer()
    {
      Chromosome chromo = new Chromosome(50, 7, 4);

      int test = chromo[7];
      int test2 = chromo[17];
      int test3 = chromo[27];

      Assert.AreEqual(test, chromo[7]);
      Assert.AreEqual(test2, chromo[17]);
      Assert.AreEqual(test3, chromo[27]);


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

