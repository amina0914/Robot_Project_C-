using System;
using System.Collections.Generic;
using GeneticAlgortihmLib;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        private int NbGenerations {get;}
        private int PopulationSize {get;}
        private int NbTrials {get;}
        private int Seed {get;}

        public int NumberOfActions {get;}
        public int NumberOfTestGrids {get;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}
        public double MutationRate {get;}
        public double EliteRate {get;}

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, int seed){
            NumberOfGenerations = nbGenerations;
            PopulationSize = populationSize;
            NbTrials = nbTrials;
            Seed = seed;
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
            for (int i=0; i<randomCansPositions.Count; i++){
                String randomPosString = randomCansPositions[i].ToString();
                grid[randomPosString[0], randomPosString[1]] = ContentsOfGrid.Can;
            }

            // sets the positions of empty to the rest of the grid
            for (int a=0; a<grid.GetLength(0); a++){
                for (int b=0; b<grid.GetLength(1); b++){
                    if (grid[a,b] != ContentsOfGrid.Can){
                        grid[a,b] = ContentsOfGrid.Empty;
                    }
                }
            }
            return grid;
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new NotImplementedException();
        }

        private List<int> generateRandomLocation(){
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
    }
}
