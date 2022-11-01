using System;
namespace GeneticAlgortihmLib
{
    public class Chromosome: IChromosome
    {
        /// <summary>
        /// The fitness score of the IChromosome
        /// </summary>
        /// <value>A value representing the fitness of the IChromosome</value>
        public double Fitness {get;}

        public int[] Genes { get; }
        private int _seed;
        public Chromosome(int numbergenes, long lenght, int seed )
        {
            Length= lenght;
            _seed=seed;
            Genes= new int[numbergenes];
            Random rand= new Random();
            for(int i=0; i < Genes.Length;i++){
                Genes[i]= rand.Next(0,7);
            }

        }
        public int CompareTo(IChromosome chromosome)
        {
            return this.Fitness.CompareTo(chromosome.Fitness);
        }

        public Chromosome(Chromosome chromosome)
        {
            _seed= chromosome._seed;
            Genes=new int[chromosome.Genes.Length];
            for(int i=0; i<Genes.Length; i++){
                Genes[i]=chromosome.Genes[i];
            }
            Length=chromosome.Length;
        }
        /// <summary>
        /// Uses a crossover function to create two offspring, then iterates through the
        /// two child Chromosomes genes, changing them to random values according to the mutation rate.
        /// </summary>
        /// <param name="spouse">The Chromosome to reproduce with</param>
        /// <param name="mutationProb">The rate of mutation</param>
        /// <returns></returns>
        public IChromosome[] Reproduce(IChromosome spouse, double mutationProb)
        {   
            return CrossoverFunction(spouse, mutationProb);
        }
        private IChromosome[] CrossoverFunction(IChromosome spouse, double mutationprob){
            Chromosome child1= new Chromosome(this.Genes.Length,Length,_seed);
            Chromosome child2=new Chromosome(spouse.Genes.Length,spouse.Length,_seed);
            Random rand= new Random();
            int pointa= rand.Next(1,this.Genes.Length-15);
            int pointb=rand.Next(pointa,Genes.Length);
            for(int i=0; i<pointa; i++)
            {
                child1.Genes[i]=Genes[i];
                child2.Genes[i]=spouse.Genes[i];
            }
            for(int j=pointa;j<pointb;j++)
            {
                child1.Genes[j]=spouse.Genes[j];
                child2.Genes[j]=Genes[j];
            }
            for(int z=pointb; z <Genes.Length; z++)
            {
                child1.Genes[z]= Genes[z];
                child2.Genes[z]= spouse.Genes[z];
            }
            for(int c=0; c< child1.Genes.Length;c++){
                if(mutationprob>rand.NextDouble())
                {
                child1.Genes[c]= rand.Next(0,6);
                child2.Genes[c]= rand.Next(0,6);
                }
            }
            IChromosome[] childs=new IChromosome[]{child1,child2};
            return childs;
        }
       public override bool Equals(object obj)
    {
        return Equals(obj as Chromosome);
    }

    public bool Equals(Chromosome other)
    {
        return other != null &&
               Fitness==other.Fitness;
    }

    public override int GetHashCode()
    {
        return Fitness.GetHashCode();
    }

        /// <summary>
        /// Returns the current gene at the provided position
        /// </summary>
        /// <value></value>
        public int this[int index] {
            get{
                return Genes[index];
            }}

        /// <summary>
        /// The length of the genes
        /// </summary>
        public long Length {get;}

        
    }

   

}