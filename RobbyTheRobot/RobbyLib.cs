namespace RobbyTheRobot{
    public static class RobbyLib{
        public static IRobbyTheRobot CreateRobby(int numGen, int populationSize, int numTrials, double mutationRate, double eliteRate,int? seed = null){
            return new RobbyTheRobot(numGen, populationSize, numTrials,mutationRate,eliteRate);
        }
    }
}