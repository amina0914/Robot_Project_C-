using System;
using System.Collections.Generic;
using System.IO;
using GeneticAlgorithm;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        private int _populationSize {get;}
        private int? _seed;
        private int _nbGenes;
        private IGeneticAlgorithm _geneticAlg;
        public int NumberOfActions {get;}
        public int NumberOfTestGrids {get;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}
        public double MutationRate {get;}
        public double EliteRate {get;}

        public event FileEventHandler FileWritten;

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, double mutationRate, double eliteRate, int? seed=null){
            if(seed !=null){
                _seed = seed ;
            }
            NumberOfGenerations = nbGenerations;
            //is pop size just nbGenes
            _populationSize = populationSize;
            GridSize = 100;
            NumberOfActions = 200;
            NumberOfTestGrids = nbTrials;
            MutationRate = mutationRate;
            EliteRate = eliteRate;
            _nbGenes = 243;
            _geneticAlg = GeneticLib.CreateGeneticAlgorithm(this._populationSize, this._nbGenes, Enum.GetNames(typeof(PossibleMoves)).Length, this.MutationRate, this.EliteRate, this.NumberOfTestGrids, ComputeFitness);
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] grid = new ContentsOfGrid[Convert.ToInt32(Math.Sqrt(GridSize)), Convert.ToInt32(Math.Sqrt(GridSize))];
            // sets the positions of the cans 
            List<int> randomCansPositions = generateRandomLocation();

            // sets all positions of the grid to start with empty
            for (int a=0; a<grid.GetLength(0); a++){
                for (int b=0; b<grid.GetLength(1); b++){
                    grid[a,b] = ContentsOfGrid.Empty;
                }
            }

            // sets the positions of the grid from the generateRandomLocation to have a can 
            for (int i=0; i<randomCansPositions.Count; i++)
            {
                // if the location contains 2 digits, it separates the digit and sets the grid at digit 1 and digit 2 to can
                if (randomCansPositions[i]>9)
                {
                    String randomPosString = randomCansPositions[i].ToString();
                    grid[randomPosString[0]-'0',randomPosString[1]-'0'] = ContentsOfGrid.Can;
                } 
                else 
                {
                    grid[0, randomCansPositions[i]] = ContentsOfGrid.Can;
                }
                
            }
            return grid;
        }

        // Method was tested, works!
        public void GeneratePossibleSolutions(string folderPath)
        {
            string fileName = "Generation";
            double maxScore = 0.0;
            int nbMoves = 200;
            int[] genes = null;

            IGeneration gen;
           
            for (int i=0; i<this.NumberOfGenerations; i++){
                gen = _geneticAlg.GenerateGeneration();
                maxScore = gen.MaxFitness;
                nbMoves = _nbGenes;
                genes = gen[0].Genes;
                // (if i%) save to file 1st, 20th, 100, 200, 500 and 1000th
                if (i == 0 || i==19 || i==99 || i==199 || i==499 || i==999)
                {        
                    int fileIndex = i+1;
                    writeToFile(folderPath, fileName + fileIndex, maxScore, nbMoves, genes); 
                    //  not sure about the event param
                    FileWritten?.Invoke(folderPath + maxScore + nbMoves + genes);
                }
            }  
        }

        // This methods writes results of a generation from the genetic algorithm to a file
        // If the folder does not exist, it creates it
        private void writeToFile(string folderPath, string fileName, double maxScore, int nbMoves, int[] genes){
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);
            
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Max score " + maxScore);
                writer.WriteLine("Number of moves " + nbMoves);
                writer.Write("Robby's actions ");
                foreach (int gene in genes)
                {
                    writer.Write(gene + " ");
                }
            }
        }

        private List<int> generateRandomLocation()
        {
            Random rnd = new Random();
            
            int randomLocation;
            List<int> listRandomLocations = new List<int>();
            for (int i=0; i<50; i++){
                do {
                    randomLocation = rnd.Next(0, GridSize);
                }
                while (listRandomLocations.Contains(randomLocation));
                listRandomLocations.Add(randomLocation);
            }
            return listRandomLocations;
        }

        // method to calculate fitness ComputeFitness()
        // calls generateRandomGrid, runs Robby through grid, scoring moves 
        public double ComputeFitness(IChromosome chromosome, IGeneration gen){
            // calls the generate grid
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            // generates Robby's moves from geneticAlgorithm
            int [] moves = chromosome.Genes;
            // generating random for the first position at x and y (position between 0 and 10, since width and height are 10)
            Random rnd = _seed != null ? new Random((int)_seed) : new Random();
            int xPos = rnd.Next(0, 10);
            int yPos = rnd.Next(0, 10);
            double fitness = 0;
            for (int i=0; i<this.NumberOfActions; i++){
                fitness = fitness + RobbyHelper.ScoreForAllele(moves, grid, rnd, ref xPos, ref yPos);
            }
            return fitness;
        }


    }
}
