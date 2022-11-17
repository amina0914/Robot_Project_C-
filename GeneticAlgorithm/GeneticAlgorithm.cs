using System;
namespace GeneticAlgorithm
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
      if(seed !=null){
        _seed = seed ;
      }else{
        _seed=null;
      }
      PopulationSize = populationSize;
      NumberOfGenes = numberOfGenes;
      LengthOfGene = lengthOfGene;
      MutationRate = mutationRate;
      EliteRate = eliteRate;
      NumberOfTrials = numberOfTrials;
      FitnessCalculation = fitnessCalculation;
      
     
    }
    /// <summary>
    /// The number of times the fitness function should be called when computing the result
    /// </summary>
    public int NumberOfTrials { get; }

    /// <summary>
    /// The current number of generations generated since the start of the algorithm
    /// </summary>
    public long GenerationCount { get;set; }

    /// <summary>
    /// Returns the current generation
    /// </summary>
    public IGeneration CurrentGeneration { get; set; }

    /// <summary>
    /// The delegate of the fitness method to be called
    /// </summary>
    /// <value></value>
    public FitnessEventHandler FitnessCalculation {get;}

    /// <summary>
    /// Generates a generation for the given parameters. If no generation has been created the initial one will be constructed. 
    /// If a generation has already been created, it will provide the next generation.
    /// </summary>
    /// <returns>The current generation</returns>
    public IGeneration GenerateGeneration()
    {
      GenerationCount++;
      Random rand = _seed != null ? new Random((int)_seed) : new Random();
      if (CurrentGeneration == null)
      {
      CurrentGeneration= new Generation(this, FitnessCalculation, _seed);
      (CurrentGeneration as IGenerationDetails).EvaluateFitnessOfPopulation();

      }
      else
      {
        int count = 0;
        int elitepopulation = 0;
        elitepopulation = (int)(EliteRate * PopulationSize);
        if (elitepopulation % 2 != 0)
        {
          elitepopulation += 1;
        }
        Chromosome[] newgen = new Chromosome[PopulationSize];

        //WILL SELECT % ELITE PARENTS AS WELL AS THEIR FITNESS 
        for (int i = 0; i < elitepopulation; i++)
        {
          IChromosome parent = (CurrentGeneration as IGenerationDetails)?.SelectParent();
          newgen[i] = new Chromosome(parent as Chromosome);
        }

        count = elitepopulation;

        //Before It ignored the Evaluation step and only % elite had fitness but not the new childs
        for(int i=elitepopulation; i < PopulationSize; i ++)
        {
           int index1 = rand.Next(0, elitepopulation);
          int index2 = rand.Next(0, elitepopulation);
          IChromosome[] childs = newgen[index1]?.Reproduce(newgen[index2], MutationRate);
          newgen[i] = new Chromosome(childs[0] as Chromosome);
          newgen[i+=1] = new Chromosome(childs[0] as Chromosome);

        }
        //Making sure Everyone gets evaluated
        CurrentGeneration = new Generation(newgen,this);
        (CurrentGeneration as IGenerationDetails).EvaluateFitnessOfPopulation();
        // return CurrentGeneration;
      }
      return CurrentGeneration;
    }
  }
}