using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;
using System;
using System.IO;
using System.Collections.Generic;

namespace RobbyTheRobotTest
{
    [TestClass]
    public class RobbyTheRobotTest
    {
        IRobbyTheRobot robby = Robby.CreateRobby(300, 400, 50, 0.5, 1, 4);

        // Testing if RobbyTheRobot object gets created and gets the right values assigned 
        [TestMethod]
        public void TestRobbyCreation()
        {        
            Assert.AreEqual(100, robby.GridSize);
            Assert.AreEqual(200, robby.NumberOfActions);
            Assert.AreEqual(50, robby.NumberOfTestGrids);
            Assert.AreEqual(300, robby.NumberOfGenerations);
            Assert.AreEqual(0.5, robby.MutationRate);
            Assert.AreEqual(1, robby.EliteRate);
        }

        // Test if 50% of the grid is empty and the other 50% is filled with cans 
        [TestMethod]
        public void TestGenerateRandomGrid()
        {        
             ContentsOfGrid[,] randomGrid = robby.GenerateRandomTestGrid();
             int countCans = 0;
             int countEmpty = 0;
             for (int a=0; a<randomGrid.GetLength(0); a++)
             {
                for (int b=0; b<randomGrid.GetLength(1); b++)
                {
                    if (randomGrid[a,b]==ContentsOfGrid.Can)
                    {
                        countCans ++;
                    }
                    else 
                    {
                        countEmpty++;
                    }
                }
            }
            // calculating the 50% of the grid size 
            int halfGrid = 50 * (randomGrid.GetLength(0) * randomGrid.GetLength(1)) / 100;
            Assert.AreEqual(halfGrid, countCans);
            Assert.AreEqual(halfGrid, countEmpty);
        }

        
        // // Test the compute fitness function with the seed
        // [TestMethod]
        // public void TestComputeFitness()
        // { 
        // }

        // Test the GeneratePossibleSolutions function, testing if file gets generated
        [TestMethod]
        public void TestComputeFitness()
        { 
            bool fileExists = false;
            robby.GeneratePossibleSolutions("./testingGA.txt");
            if (File.Exists("./testingGA.txt")) {
                fileExists = true;
            }
            Assert.IsTrue(fileExists);
        }
    }
}
