using System;
namespace GeneticAlgortihmLib
{
  public class Generation : IGenerationDetails
  {
    private IChromosome[] _chromosomes;
    private IGeneticAlgorithm algorithm1;
    private FitnessEventHandler fitnessEvent1;
    int _seed;
    /// <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness { get{
        double average=0;
        foreach(IChromosome chrom in _chromosomes){
            average+=chrom.Fitness;
        }
        return (average/_chromosomes.Length);
    } }
    /// <summary
    /// The maximum fitness across all Chromosomes
    /// </summary>
    public double MaxFitness { get{
        double maxfitness;
        maxfitness = _chromosomes[0].Fitness;
      foreach (Chromosome chromosome in _chromosomes)
      {
        if (chromosome.Fitness > maxfitness)
        {
          maxfitness = chromosome.Fitness;
        }
      }
      return maxfitness;

    } }

    /// <summary>
    /// Returns the number of Chromosomes in the generation
    /// </summary>
    public long NumberOfChromosomes { get{
        return _chromosomes.LongLength;
    } }

    /// <summary>
    /// Retrieves the IChromosome from the generation
    /// </summary>
    /// <value>The selected IChromosome</value>
    public IChromosome this[int index]
    {
      get
      {
        return _chromosomes[index];
      }
    }
    public Generation(Chromosome[] arrayChromosomes)
    {
      _chromosomes = new Chromosome[arrayChromosomes.Length];
      for (int i = 0; i < arrayChromosomes.Length; i++)
      {
        _chromosomes[i] = new Chromosome(arrayChromosomes[i]);
      }
    }

    public Generation(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int seed)
    {
      algorithm1 = algorithm;
      fitnessEvent1 = fitnessEvent;
      _seed = seed;
    }
    /// <summary>
    /// Randomly selects a parent by comparing its fitness to others in the population
    /// </summary>
    /// <returns></returns>
    public IChromosome SelectParent()
    {
      IChromosome potentialparent = _chromosomes[0];
      foreach (Chromosome chromosome in _chromosomes)
      {
        if (chromosome.Fitness > potentialparent.Fitness)
        {
          potentialparent = chromosome;
        }
      }
      return potentialparent;
    }

    /// <summary>
    /// Computes the fitness of all the Chromosomes in the generation. 
    /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
    /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
    /// the average of the number of trials is used to compute the final fitness of the Chromosome.
    /// </summary>
    public void EvaluateFitnessOfPopulation() { }


  }
}