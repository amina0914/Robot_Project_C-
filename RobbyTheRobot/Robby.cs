namespace RobbyTheRobot
{
    public static class Robby
    {
        public static IRobbyTheRobot CreateRobby(int nbGenerations, int populationSize, int nbTrials, double mutationRate, double eliteRate, int? seed=null)
        {
            return new RobbyTheRobot(nbGenerations,populationSize, nbTrials, mutationRate, eliteRate, seed);
        }
    }
}