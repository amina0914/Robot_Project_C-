using System;
using System.Linq;
namespace GeneticAlgorithm
{
    internal class Chromosome: IChromosome
    {
          /// <summary>
        /// The fitness score of the IChromosome
        /// </summary>
        /// <value>A value representing the fitness of the IChromosome</value>
        public double Fitness {get;set;}

        public int[] Genes => _genes.ToArray();//Better practice
        private int[] _genes;
        private int? _seed;
        private int _lengthgene;
        public Chromosome(int numbergenes, int length, int? seed=null )
        {
            _seed=seed;
            _lengthgene=length;
            _genes= new int[numbergenes];
            Random rand= new Random();
            for(int i=0; i < _genes.Length;i++){
                _genes[i]= rand.Next(0,7);
            }
        }
        public int CompareTo(IChromosome chromosome)
        {
            return this.Fitness.CompareTo(chromosome.Fitness);
        }

        public Chromosome(Chromosome chromosome)
        {
            _seed= chromosome._seed;
            _genes=new int[chromosome._genes.Length];
            for(int i=0; i<_genes.Length; i++){
                _genes[i]=chromosome._genes[i];
            }
            Fitness=chromosome.Fitness;
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
        private Chromosome[] CrossoverFunction(IChromosome spouse, double mutationprob){
            Chromosome child1= new Chromosome(this.Genes.Length,_lengthgene,_seed);   
            Chromosome child2=new Chromosome(spouse.Genes.Length,_lengthgene,_seed);
            Random rand= _seed != null ? new Random((int)_seed): new Random();
            int pointa= rand.Next(1,this.Genes.Length-15);
            int pointb=rand.Next(pointa,Genes.Length);
            for(int i=0; i<pointa; i++)
            {
                child1._genes[i]=_genes[i];
                child2._genes[i]=spouse[i];
            }
            for(int j=pointa;j<pointb;j++)
            {
                child1._genes[j]=spouse[j];
                child2._genes[j]=Genes[j];
            }
            for(int z=pointb; z <_genes.Length; z++)
            {
                child1._genes[z]= Genes[z];
                child2._genes[z]= spouse[z];
            }
            //Mutate
            for(int c=0; c< child1.Genes.Length;c++){
                if(mutationprob>rand.NextDouble())
                {
                child1._genes[c]= rand.Next(0,6);
                child2._genes[c]= rand.Next(0,6);
                }
            }
            Chromosome[] childs=new Chromosome[]{child1,child2};
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
                return _genes[index];
            }
           }

        /// <summary>
        /// The length of the genes
        /// </summary>
        public long Length => _genes.Length;

        
    }

   

}