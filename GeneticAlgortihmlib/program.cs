using System;
using System.Collections.Generic;
namespace GeneticAlgortihmLib
{
  class Program{

      static void Main(string[] args)
        {
           Chromosome testa= new Chromosome(20,20,6);
           Chromosome testb= new Chromosome(20,20,6);
    
           Chromosome[] mychilds= testa.Reproduce(testb);
           Chromosome resutla= mychilds[0];           
           Chromosome resutl2b= mychilds[1];

        }

  }
}