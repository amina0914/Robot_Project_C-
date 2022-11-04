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
    private double _avergfitness;
    private double _maxfit;
    /// <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness => _chromosomes.Average(i=>i.Fitness);
    /// <summary
    /// The maximum fitness across all Chromosomes
    /// </summary>
    public double MaxFitness => _maxfit;

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
        _chromosomes[i] = arrayChromosomes[i];
      }
    }

    public Generation(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int? seed)
    {
      _algorithm = algorithm;
      _fitnessHandler = fitnessEvent;
      _seed = seed;
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
      double averagefitness = 0;
    
      foreach(Chromosome chromo in _chromosomes)
      {
        double fitness=0;
        for(int z=0; z < _algorithm.NumberOfTrials;z++){
           fitness+=_fitnessHandler.Invoke(chromo, this);
        }
       chromo.Fitness=fitness/_algorithm.NumberOfTrials;
      
      }
        
      
      // for (int i = 0; i < _chromosomes.Length; i++)
      // {
      //   double fitness = _fitnessHandler.Invoke(_chromosomes[i], this);
      //   averagefitness += fitness;
      //   _chromosomes[i].Fitness = fitness;
      // }
      // _avergfitness = averagefitness / NumberOfChromosomes;
      Array.Sort(_chromosomes);
      Array.Reverse(_chromosomes); //This is expensive goes on mehtod below Recommendation usign a subset
      _maxfit = _chromosomes[0].Fitness;
    // int nbmoftria= _algorithm.NumberOfTrials;
    // double fintessscore=0;
    // double totalFitnessScore = 0;
    // double finalaveragefitsc=0;
    // foreach(Chromosome a in _chromosomes){

    //     fintessscore=
        

    // }
    // //per chromosome nth number of trials
    // for(int i=0; i < nbmoftria;i++){
    //   fintessscore = _fitnessHandler.Invoke(a, this);
    //   totalFitnessScore += fintessscore;
    //   nbmoftria++;

    // }
    // //find the average of fitness score of chromosome after nth trials
    // finalaveragefitsc = totalFitnessScore / nbmoftria;

    //pass the fitness score to the chromosome 
    //then put the chromosome into the chromosome array



    }


  }
}