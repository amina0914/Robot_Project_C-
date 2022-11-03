using System;
namespace GeneticAlgortihm
{
  internal class GeneticAlgorithm : IGeneticAlgorithm
  {
    public int PopulationSize { get; }
    public int NumberOfGenes { get; }
    public int LengthOfGene { get; }
    public double MutationRate { get; }
    public double EliteRate { get; }
    int? _seed;


    public GeneticAlgorithm(int populationSize, int numberOfGenes, int lengthOfGene, double mutationRate, double eliteRate, int numberOfTrials, FitnessEventHandler fitnessCalculation, int? seed = null)
    {
      PopulationSize = populationSize;
      NumberOfGenes = numberOfGenes;
      LengthOfGene = lengthOfGene;
      MutationRate = mutationRate;
      EliteRate = eliteRate;
      NumberOfTrials = numberOfTrials;
      FitnessCalculation = fitnessCalculation;
      _seed = seed;
    }
    /// <summary>
    /// The number of times the fitness function should be called when computing the result
    /// </summary>
    public int NumberOfTrials { get; }

    /// <summary>
    /// The current number of generations generated since the start of the algorithm
    /// </summary>
    public long GenerationCount { get; }

    /// <summary>
    /// Returns the current generation
    /// </summary>
    public IGeneration CurrentGeneration { get; }

    /// <summary>
    /// The delegate of the fitness method to be called
    /// </summary>
    /// <value></value>
    public FitnessEventHandler FitnessCalculation { get; }

    /// <summary>
    /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
    /// If a generation has already been created, it will provide the next generation.
    /// </summary>
    /// <returns>The current generation</returns>
    public IGeneration GenerateGeneration()
    {
      Random rand= _seed != null ? new Random((int)_seed): new Random();
      if (CurrentGeneration is null)
      {
        Generation currentgen = new Generation(this, FitnessCalculation, _seed);
        return currentgen;
      }
      else
      {
        int count = 0;
        int elitepopulation = 0;
        elitepopulation = (int)EliteRate * PopulationSize;//modulus % for even 2 numbers.
        if(elitepopulation%2 !=0){
          elitepopulation+=1;
        }
        Chromosome[] newgen = new Chromosome[PopulationSize];//  remember final array[] has same population
        for(int i=0; i < elitepopulation; i ++){
           IChromosome parent = (CurrentGeneration as IGenerationDetails)?.SelectParent();
          newgen[i] = parent as Chromosome;
        }
        count=elitepopulation;
       
        while (count < PopulationSize)//Subset + childs. 
        {
          IChromosome[] childs=newgen[rand.Next(0,elitepopulation)].Reproduce(newgen[rand.Next(0,elitepopulation)],MutationRate);
          foreach(Chromosome kid in childs)
          {
              newgen[count]=kid;
              count++;          
          }
        }
        Generation newgeneration = new Generation(newgen);
        return newgeneration;
      }
    }

  }
}