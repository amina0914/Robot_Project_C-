using System;
using System.Linq;
namespace GeneticAlgorithm
{
  internal class Generation : IGenerationDetails
  {
    public Chromosome[] _chromosomes;
    private IGeneticAlgorithm _algorithm;
    private FitnessEventHandler _fitnessHandler;
    int? _seed;

    /// <summary>
    /// The average fitness across all Chromosomes
    /// </summary>
    public double AverageFitness => _chromosomes.Average(i => i.Fitness);
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
    //WORK AROUND TO MAKE SURE THE FITNESSCAL EXIST ON THE CONTEXT OTHERWISE IT BE ALWAYS 0 WHEN
    //FITNESS IS CALCULATED
    public Generation(Chromosome[] arrayChromosomes,IGeneticAlgorithm algorithm)
    {
      _algorithm = algorithm;
      _fitnessHandler=_algorithm.FitnessCalculation;
      _chromosomes = new Chromosome[arrayChromosomes.Length];
      for (int i = 0; i < arrayChromosomes.Length; i++)
      {
        _chromosomes[i] = new Chromosome(arrayChromosomes[i]);
      }
      // EvaluateFitnessOfPopulation();Uncommenting to let GA Handle this and see if it breaks
    }


    //We talk about the Null check but not sure if done properly
    public Generation(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int? seed)
    {
      _algorithm = algorithm;
      _fitnessHandler += fitnessEvent;
      if (seed != null)
      {
        _seed = seed;

      }else{
        _seed=null;
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
       int elitepopulation=0;


       //SELECTING FROM THE % Elite batch only randomly
      if(_algorithm != null)
      {
        elitepopulation = (int) (_algorithm.EliteRate * _algorithm.PopulationSize);
         if (elitepopulation % 2 != 0)
        {
          elitepopulation += 1;
        }
      }else{
        elitepopulation=15;
      }
              
      Chromosome potentialparent = _chromosomes[rand.Next(elitepopulation)];//Subesett.
      int subset= elitepopulation;
      for(int i=0; i < subset; i++)
      {
        if(potentialparent.Fitness > _chromosomes[i].Fitness)
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
      //To Review it Again.
      if (_fitnessHandler != null && _algorithm != null) 
      {
        foreach (Chromosome chromo in _chromosomes)
        {
          double fitness = 0;
          for (int z = 0; z < _algorithm.NumberOfTrials; z++)
          {
            fitness += _fitnessHandler.Invoke(chromo, this);
          }         
          chromo.Fitness = (fitness /((double) _algorithm.NumberOfTrials));
        }
      }
      Array.Sort(_chromosomes);
      Array.Reverse(_chromosomes);

    }


  }
}