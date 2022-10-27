using System;
using System.Collections.Generic;
namespace GeneticAlgortihmLib
{
  class Program{

      static void Main(string[] args)
        {
           IChromosome testa= new Chromosome(20,20,6);
           IChromosome testb= new Chromosome(20,20,6);
    
           IChromosome[] mychilds= testa.Reproduce(testb,2);
           IChromosome resutla= mychilds[0];           
           IChromosome resutl2b= mychilds[1];

        }

  }
}