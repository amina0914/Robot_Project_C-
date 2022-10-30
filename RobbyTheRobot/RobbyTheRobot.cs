using System;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        private int NbGenerations {get;}
        private int PopulationSize {get;}
        private int NbTrials {get;}
        private int Seed {get;}

        public int NumberOfActions => throw new NotImplementedException();

        public int NumberOfTestGrids => throw new NotImplementedException();

        public int GridSize => throw new NotImplementedException();

        public int NumberOfGenerations => throw new NotImplementedException();

        public double MutationRate => throw new NotImplementedException();

        public double EliteRate => throw new NotImplementedException();

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, int seed){
            NbGenerations = nbGenerations;
            PopulationSize = populationSize;
            NbTrials = nbTrials;
            Seed = seed;
        }

        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            throw new NotImplementedException();
        }

        public void GeneratePossibleSolutions(string folderPath)
        {
            throw new NotImplementedException();
        }
    }
}
