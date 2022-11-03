using System;
namespace GeneticAlgortihm
{
  internal class Generation : IGenerationDetails
  {
     private Chromosome[] _chromosomes;
    private IGeneticAlgorithm algorithm;
    private FitnessEventHandler _fitnessHandler;
    int? _seed;
    private double _avergfitness;
    private double _maxfit;
    /// <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness => _avergfitness;
    /// <summary
    /// The maximum fitness across all Chromosomes
    /// </summary>
    public double MaxFitness =>_maxfit;

    /// <summary>
    /// Returns the number of Chromosomes in the generation
    /// </summary>
    public long NumberOfChromosomes =>_chromosomes.Length; 

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
        _chromosomes[i] =arrayChromosomes[i];
      }
    }

    public Generation(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int? seed)
    {
      this.algorithm = algorithm;
      _fitnessHandler = fitnessEvent;
      _seed = seed;
      _chromosomes = new Chromosome[this.algorithm.PopulationSize];
      for(int i=0; i<_chromosomes.Length; i++)
      {
        _chromosomes[i] = new Chromosome(this.algorithm.NumberOfGenes, this.algorithm.LengthOfGene, _seed);
      }

    }
    /// <summary>
    /// Randomly selects a parent by comparing its fitness to others in the population
    /// </summary>
    /// <returns></returns>
    public IChromosome SelectParent()
    {
      Random rand= _seed != null ? new Random((int)_seed): new Random();
      
      Chromosome potentialparent = _chromosomes[rand.Next(_chromosomes.Length)];//Subesett.
      foreach (Chromosome chromosome in _chromosomes)
      {
        if (potentialparent.Fitness > chromosome.Fitness)
        {
          return chromosome;
        }
      }
      return null;
    }

    /// <summary>
    /// Computes the fitness of all the Chromosomes in the generation. 
    /// Note, a FitnessEventHandler deleagte is invoked for every fitness function that must be calculated and is provided by the user
    /// Note, if NumberOfTrials is greater than 1 in IGeneticAlgorithm, 
    /// the average of the number of trials is used to compute the final fitness of the Chromosome.
    /// </summary>
    public void EvaluateFitnessOfPopulation() { 
      //Here aI have someherwe fitnesshandler.ino
      double averagefitness=0;
      for(int i=0; i < _chromosomes.Length; i++)
      {
        double fitness=_fitnessHandler.Invoke(_chromosomes[i],this);
        averagefitness+=fitness;
        _chromosomes[i].Fitness=fitness;
      }
      _avergfitness= averagefitness/NumberOfChromosomes;
      Array.Sort(_chromosomes);
      Array.Reverse(_chromosomes); //This is expensive goes on mehtod below Recommendation usign a subset
      _maxfit= _chromosomes[0].Fitness;
    }
    

  }
}