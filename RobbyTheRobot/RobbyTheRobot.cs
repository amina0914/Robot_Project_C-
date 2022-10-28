using System;

namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
        private int NbGenerations {get;}
        private int PopulationSize {get;}
        private int NbTrials {get;}
        private int Seed {get;}

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, int seed){
            NbGenerations = nbGenerations;
            PopulationSize = populationSize;
            NbTrials = nbTrials;
            Seed = seed;
        }
    }
}
