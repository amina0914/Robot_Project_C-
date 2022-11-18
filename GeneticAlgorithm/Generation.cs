using System;
<<<<<<< HEAD
=======
using System.Diagnostics;
>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
  internal class Generation : IGenerationDetails
  {
    private Chromosome[] _chromosomes;
<<<<<<< HEAD
    private IGeneticAlgorithm _algorithm;
    public FitnessEventHandler _fitnessHandler{get;set;}
    int? _seed;

=======
    private IGeneticAlgorithm _algorithm{get;set;}
    public FitnessEventHandler _fitnessHandler{get;set;}
    int? _seed;
>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
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
        Debug.Assert(index < _chromosomes.Length);
        return _chromosomes[index];
      }
    }
<<<<<<< HEAD
    //WORK AROUND TO MAKE SURE THE FITNESSCAL EXIST ON THE CONTEXT OTHERWISE IT BE ALWAYS 0 WHEN
    //FITNESS IS CALCULATED
    public Generation(Chromosome[] arrayChromosomes, IGeneticAlgorithm algorithm)
    {
      
       _algorithm = algorithm;
       _fitnessHandler=_algorithm.FitnessCalculation;
      _chromosomes = new Chromosome[arrayChromosomes.Length];
=======
    public Generation(Chromosome[] arrayChromosomes,IGeneticAlgorithm algorithm)
    {
      Debug.Assert(arrayChromosomes != null && algorithm != null);
      _algorithm = algorithm;
      _fitnessHandler=_algorithm.FitnessCalculation;

       _chromosomes = new Chromosome[arrayChromosomes.Length];
>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
      for (int i = 0; i < arrayChromosomes.Length; i++)
      {
        _chromosomes[i] = new Chromosome(arrayChromosomes[i]);
      }
<<<<<<< HEAD
      // EvaluateFitnessOfPopulation();Uncommenting to let GA Handle this and see if it breaks
=======

>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
    }

    public Generation(IGeneticAlgorithm algorithm, FitnessEventHandler fitnessEvent, int? seed)
    {
      Debug.Assert(fitnessEvent != null && algorithm != null);
      _algorithm = algorithm;
      _fitnessHandler += fitnessEvent;
<<<<<<< HEAD
      if (seed != null)
      {
        _seed = seed;

      }else{
        _seed=null;
      }
=======
      _seed = seed;

>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
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
<<<<<<< HEAD
      Random rand = _seed != null ? new Random((int)_seed) : new Random();
=======
     Random rand = _seed != null ? new Random((int)_seed) : new Random();
>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
       int elitepopulation=0;
      Chromosome potentialparent;
       //SELECTING FROM THE % Elite batch only randomly
      if(_algorithm != null)
      {
        elitepopulation = (int) (_algorithm.EliteRate * _algorithm.PopulationSize);
         if (elitepopulation % 2 != 0)
        {
          elitepopulation += 1;
        }
         potentialparent = new Chromosome(_chromosomes[rand.Next(elitepopulation)]);
      }else{
        elitepopulation=25;
         int subset= elitepopulation;
        potentialparent = new Chromosome(_chromosomes[rand.Next(elitepopulation)]);
      }
      Debug.Assert(potentialparent!= null);     
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
<<<<<<< HEAD
      //To Review it Again.
      if (_fitnessHandler != null && _algorithm != null) 
=======

      if (_fitnessHandler != null && _algorithm != null && _algorithm.NumberOfTrials>1) 
>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
      {
        
        Parallel.ForEach(_chromosomes, chromo => {
         double fitness = 0;
        for (int z = 0; z < _algorithm.NumberOfTrials; z++)
          {
            fitness += _fitnessHandler.Invoke(chromo, this);
          }   
         chromo.Fitness = (fitness /((double) _algorithm.NumberOfTrials));}
         );
       
<<<<<<< HEAD
      }
     
      Array.Sort(_chromosomes);
      Array.Reverse(_chromosomes);

=======
      }else{
        Parallel.ForEach(_chromosomes, chromo => {
         double fitness = 0;
          fitness += _fitnessHandler.Invoke(chromo, this); 
         chromo.Fitness = (fitness /((double) _algorithm.NumberOfTrials));}
         );
      }
      Array.Sort(_chromosomes);
      Array.Reverse(_chromosomes);
>>>>>>> 7369fdae25654cfea43ea0dcdd6f2c1f7e7f68a9
    }


  }
}