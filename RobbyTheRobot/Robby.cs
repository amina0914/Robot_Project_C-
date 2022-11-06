namespace RobbyTheRobot
{
    public static class Robby
    {
        public static IRobbyTheRobot CreateRobby(int nbGenerations, int populationSize, int nbTrials, int seed)
        {
            return new RobbyTheRobot(nbGenerations,populationSize, nbTrials, seed);
        }
    }
}