using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;

namespace RobbyTheRobotTest
{
    [TestClass]
    public class RobbyTheRobotTest
    {
        // Testing if RobbyTheRobot object gets created and gets the right values assigned 
        [TestMethod]
        public void TestRobbyCreation()
        {
            IRobbyTheRobot robby = Robby.CreateRobby(300, 400, 70, 50);
            // GridSize = 100;
            // NumberOfActions = 200;
            // NumberOfTestGrids = nbTrials;
            Assert.AreEqual(100, robby.GridSize);
            Assert.AreEqual(200, robby.NumberOfActions);
            Assert.AreEqual(70, robby.NumberOfTestGrids);
            Assert.AreEqual(300, robby.NumberOfGenerations);
        }
    }
}
