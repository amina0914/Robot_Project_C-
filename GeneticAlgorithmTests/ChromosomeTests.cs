using System;
using GeneticAlgorithm;
namespace GeneticAlgorithmTests{
    [TestClass]
public class ChromosomeTests
{
    [TestMethod]
    public void TestConstructor()
    {
        Chromosome chromo= new Chromosome(243,7);
        Assert.AreEqual(243,chromo.Length);
    }
    [TestMethod]
    public void TestCopyConstructor()
    {
        Chromosome chromo= new Chromosome(243,7,4);
        Chromosome copy= new Chromosome(chromo);
        Assert.AreEqual(copy.Length,chromo.Length);
        Assert.AreEqual(copy[0],chromo[0]);
        Assert.AreEqual(copy[7],chromo[7]);
    }
     [TestMethod]
    public void TestReproduce()
    {   
        //Testing the Reproduce Method With seed 4 and 5 for respective parent
        Chromosome chromo= new Chromosome(243,7,4);
        Chromosome spouse= new Chromosome(243,7,5);
        //Generating the Child
        IChromosome[] childs= chromo.Reproduce(spouse,0.05);
        Console.WriteLine("chromo");
        foreach(Int32 x in chromo.Genes){
            Console.Write(x+ "");
        }
        Console.WriteLine("");
         Console.WriteLine("spouse");
        foreach(Int32 x in spouse.Genes){
            Console.Write(x+ "");
        }
        Console.WriteLine("");
         Console.WriteLine("child[0]");
        foreach(Int32 x in childs[0].Genes){
            Console.Write(x+ "");
        }
        Console.WriteLine("");
         Console.WriteLine("child[1]");
        foreach(Int32 x in childs[1].Genes){
            Console.Write(x+ "");
        }
        Console.WriteLine("");
        //Making Sure the Childs is = to my expected output
        Chromosome chromo2= new Chromosome(243,7,4);
        Chromosome spouse2= new Chromosome(243,7,5);
         IChromosome[] childs2= chromo2.Reproduce(spouse2,0.05);

        Assert.AreEqual(childs2[0].Length,childs[0].Length);
        Assert.AreEqual(childs2[0],childs[0]);
        Assert.AreEqual(childs2[1],childs[1]);
        // Assert.AreEqual(copy[7],chromo[7]);
    }
}
}

