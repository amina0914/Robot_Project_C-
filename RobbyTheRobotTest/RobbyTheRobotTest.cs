/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 19-11-2022
@description: This is the class that tests RobbyTheRobot's class.
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace RobbyTheRobotTest
{
    [TestClass]
    public class RobbyTheRobotTest
    {
        IRobbyTheRobot robby = Robby.CreateRobby(1000, 200, 100,  0.5, 0.05, 4);
        // Testing if RobbyTheRobot object gets created and gets the right values assigned 
        [TestMethod]
        public void TestRobbyCreation()
        {        
            Assert.AreEqual(100, robby.GridSize);
            Assert.AreEqual(200, robby.NumberOfActions);
            Assert.AreEqual(100, robby.NumberOfTestGrids);
            Assert.AreEqual(1000, robby.NumberOfGenerations);
            Assert.AreEqual(0.5, robby.MutationRate);
            Assert.AreEqual(0.05, robby.EliteRate);
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

        // Test the GeneratePossibleSolutions function, testing if the files get generated
        [TestMethod]
        public void TestGeneratePossibleSolutionsFiles()
        { 
            // Generating the solutions from genetic algorithm that will be written to the files
           // robby.GeneratePossibleSolutions("C:/Users/amina/Downloads/Generations");

            // check if the first generation file exists
            bool file1Exists = false;
            if (File.Exists("C:/Users/amina/Downloads/Generations/Generation1")) {
                file1Exists = true;
            }
            Assert.IsTrue(file1Exists);

            // check if the 20th generation file exists
            bool file2Exists = false;
            if (File.Exists("C:/Users/amina/Downloads/Generations/Generation20")) {
                file2Exists = true;
            }
            Assert.IsTrue(file2Exists);

            // check if the 100th generation file exists
            bool file3Exists = false;
            if (File.Exists("C:/Users/amina/Downloads/Generations/Generation100")) {
                file3Exists = true;
            }
            Assert.IsTrue(file3Exists);

            // check if the 200th generation file exists
            bool file4Exists = false;
            if (File.Exists("C:/Users/amina/Downloads/Generations/Generation200")) {
                file4Exists = true;
            }
            Assert.IsTrue(file4Exists);

            // check if the 500th generation file exists
            bool file5Exists = false;
            if (File.Exists("C:/Users/amina/Downloads/Generations/Generation500")) {
                file5Exists = true;
            }
            Assert.IsTrue(file5Exists);

            // check if the 1000th generation file exists
            bool file6Exists = false;
            if (File.Exists("C:/Users/amina/Downloads/Generations/Generation1000")) {
                file6Exists = true;
            }
            Assert.IsTrue(file6Exists);
        }

        
        // Test the GeneratePossibleSolutions function, testing if the files that were generated have the proper data
        [TestMethod]
        public void TestGeneratedFilesData()
        { 

            // Testing if the first line has the maximum score 
            String firstLine = System.IO.File.ReadLines("C:/Users/amina/Downloads/Generations/Generation1").Skip(0).Take(1).First();
            StringAssert.Contains(firstLine, "Max score");

            // Testing if the first line has the number of moves 
            String secondLine = System.IO.File.ReadLines("C:/Users/amina/Downloads/Generations/Generation1").Skip(1).Take(1).First();
            StringAssert.Contains(secondLine, "Number of moves");

            List <int> moves = new List<int>();
            String thirdLine = System.IO.File.ReadLines("C:/Users/amina/Downloads/Generations/Generation1").Skip(2).Take(1).First();
            // Testing that the 3rd lines has Robby's actions
            StringAssert.Contains(thirdLine, "Robby's actions");
            char[] lines= thirdLine.ToArray();
                for (int i=0; i<lines.Length; i++)
                {
                    if(Char.IsNumber(lines[i]))
                    {
                        moves.Add(thirdLine[i] - '0');
                    }
                    
                }
            // testing if the file contains the whole array of 200 moves 
            Assert.AreEqual(243, moves.Count);
        }
    }
}
