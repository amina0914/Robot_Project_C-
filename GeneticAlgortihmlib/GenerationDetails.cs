using System;
namespace GeneticAlgortihmLib
{
    public class GenerationDetails: IGenerationDetails
    { 
      private Chromosome[] _chromosomes;
      IGeneticAlgorithm algorithm1;
      FitnessEventHandler fitnessEvent1;
      int _seed;
      public GenerationDetails(IChromosome[] arrayChromosomes)
      {
        _chromosomes= new IChromosome[arrayChromosomes.Length];
        for(int i=0; i < arrayChromosomes.Length; i++){
          _chromosomes[i]= new Chromosome(arrayChromosomes[i]);
        }
      }

      public GenerationDetails(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int seed){
        algorithm1=algorithm;
        fitnessEvent1=fitnessEvent;
        _seed=seed;
      }
       /// <summary>
        /// Randomly selects a parent by comparing its fitness to others in the population
        /// </summary>
        /// <returns></returns>
        public IChromosome SelectParent(){
          Chromosome potentialparent= _chromosomes[0];
         foreach(IChromosome chromosome in _chromosomes){
          if(chromosome.Fitness> potentialparent){
            potentialparent=chromosome;
          }
         }
        }

        /// <summary>
        /// Computes the fitness of all the Chromosomes in the generation. 
        /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
        /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
        /// the average of the number of trials is used to compute the final fitness of the Chromosome.
        /// </summary>
        void EvaluateFitnessOfPopulation();

   
    }
}