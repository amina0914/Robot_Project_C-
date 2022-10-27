using System;
namespace GeneticAlgortihmLib
{
    public class Chromosome: IChromosome
    {
        /// <summary>
        /// The fitness score of the IChromosome
        /// </summary>
        /// <value>A value representing the fitness of the IChromosome</value>
        double Fitness {get;}

        public int[] Genes { get; }
        private int _seed;
        public Chromosome(int numbergenes, int lenght, int seed )
        {
            Length= lenght;
            _seed=seed;
            Genes= new int[numbergenes];
            Random rand= new Random();
            for(int i=0; i < Genes.Length;i++){
                Genes[i]= rand.Next(0,6);
            }

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
        public Chromosome[] Reproduce(Chromosome spouse, double mutationProb)
        {   
            Random rand= new Random();
            Chromosome child1= new(this.Genes.Length,Length,_seed);
             Chromosome child2=new(spouse.Genes.Length,spouse.Length,2);
            int pointa= rand.Next(1,this.Genes.Length-5);
            int pointb=rand.Next(pointa,Genes.Length);
            for(int i=0; i<pointa; i++)
            {
                child1.Genes[i]=Genes[i];
                child2.Genes[i]=spouse.Genes[i];
            }
            for(int j=pointa;j<pointb;j++)
            {
                child1.Genes[j]=Genes[j];
                child2.Genes[j]=spouse.Genes[j];
            }
            for(int z=pointb; z <Genes.Length; z++)
            {
                child1.Genes[z]= Genes[z];
                child2.Genes[z]= spouse.Genes[z];
            }
            
            return new Chromosome[]{child1,child2};
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
        int this[int index] {
            get{
                return Genes[index];
            }}

        /// <summary>
        /// The length of the genes
        /// </summary>
        long Length {get;}

        
    }

   

}