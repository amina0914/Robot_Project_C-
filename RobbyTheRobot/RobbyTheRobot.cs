using System;
using System.Collections.Generic;
using GeneticAlgorithm;
using System.IO;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        private int _nbGenerations {get;}
        private int _populationSize {get;}
        private int _nbTrials {get;}
        private int _seed {get;}
        public int NumberOfActions {get;}
        public int NumberOfTestGrids {get;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}
        public double MutationRate {get;}
        public double EliteRate {get;}

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, int seed){
            NumberOfGenerations = nbGenerations;
            _populationSize = populationSize;
            _nbTrials = nbTrials;
            _seed = seed;
            //hardcoded the value to 100
            GridSize = 100;
            NumberOfActions = 200;
            NumberOfTestGrids = nbTrials;
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

        // Method not finished yet, only started the file reader, will need the genetic algorithm to get the genes and moves
        public void GeneratePossibleSolutions(string folderPath)
        {
            double maxScore = 0.0;
            int nbMoves = 200;
            IChromosome genes = null;
            int lengthOfGene = 0;
            // IGeneticAlgorithm geneticAlg = GeneticLib.CreateGeneticAlgorithm(this._populationSize, 1000, lengthOfGene, this.MutationRate, this.EliteRate, NumberOfActions, ComputeFitness());


            // Write to a file the top candidate of the 1st, 20th, 100, 200, 500 and 1000th generation.
            using (StreamWriter writer = new StreamWriter(folderPath))
            {
                writer.WriteLine("Max score " + maxScore);
                writer.WriteLine("Number of moves " + nbMoves);
                writer.WriteLine("Robby's actions " + genes);
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
        public double ComputeFitness(IChromosome chromosome){
            // calls the generate grid
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            // generates Robby's moves from geneticAlgorithm
            int [] moves = chromosome.Genes;
            // generating random for the first position at x and y (position between 0 and 10, since width and height are 10)
            Random rnd = new Random();
            int xPos = rnd.Next(0, 10);
            int yPos = rnd.Next(0, 10);
            double fitness = RobbyHelper.ScoreForAllele(moves, grid, rnd, ref xPos, ref yPos);
            return fitness;
        }
    }
}
