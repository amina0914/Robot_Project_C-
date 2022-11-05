using System;
using System.Linq;
namespace GeneticAlgorithm
{
  internal class Generation : IGenerationDetails
  {
    private Chromosome[] _chromosomes;
    private IGeneticAlgorithm _algorithm;
    private FitnessEventHandler _fitnessHandler;
    int? _seed;
    
    /// <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness => _chromosomes.Average(i=>i.Fitness);
    /// <summary
    /// The maximum fitness across all Chromosomes
    /// </summary>
    public double MaxFitness => _chromosomes[0].Fitness;

    /// <summary>
    /// Returns the number of Chromosomes in the generation
    /// </summary>
    public long NumberOfChromosomes => _chromosomes.Length;

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

    public Generation(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int? seed)
    {
      _algorithm = algorithm;
      _fitnessHandler = fitnessEvent;
      if(seed != null){
        _seed= seed;

      }
      _chromosomes = new Chromosome[_algorithm.PopulationSize];
      for (int i = 0; i < _chromosomes.Length; i++)
      {
        _chromosomes[i] = new Chromosome(_algorithm.NumberOfGenes, _algorithm.LengthOfGene, _seed);
      }

    }
    /// <summary>
    /// Randomly selects a parent by comparing its fitness to others in the population
    /// </summary>
    /// <returns></returns>
    public IChromosome SelectParent()
    {
      Random rand = _seed != null ? new Random((int)_seed) : new Random();

      Chromosome potentialparent = _chromosomes[rand.Next(_chromosomes.Length)];//Subesett.
      foreach (Chromosome chromosome in _chromosomes)
      {
        if (potentialparent.Fitness > chromosome.Fitness)
        {
          return potentialparent;
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
    public void EvaluateFitnessOfPopulation()
    {
      //Here Invoke the Handler and that should be it.
      foreach(Chromosome chromo in _chromosomes)
      {
        double fitness=0;
        for(int z=0; z < _algorithm.NumberOfTrials;z++){
           fitness+=_fitnessHandler.Invoke(chromo, this);
        }
       chromo.Fitness=(fitness/_algorithm.NumberOfTrials);
      
      }
        
      Array.Sort(_chromosomes);
      Array.Reverse(_chromosomes); 
      
    }


  }
}