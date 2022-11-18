<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.IO;
using GeneticAlgorithm;
=======
﻿/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 19-11-2022
@description: This is the class for RobbyTheRobot, it creates his grid, generates his possible solutions, and computed his fitness.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeneticAlgorithm;

>>>>>>> main
namespace RobbyTheRobot
{
    internal class RobbyTheRobot : IRobbyTheRobot
    {
<<<<<<< HEAD
        private int _populationSize {get;}
=======
        private int _populationSize;
>>>>>>> main
        private int? _seed;
        private int _nbGenes;
        private IGeneticAlgorithm _geneticAlg;
        public int NumberOfActions {get;}
        public int NumberOfTestGrids {get;}
        public int GridSize {get;}
        public int NumberOfGenerations {get;}
        public double MutationRate {get;}
        public double EliteRate {get;}
<<<<<<< HEAD
        public IGeneticAlgorithm GeneticA{get;}//The workd around from the Interface

        public event FileEventHandler FileWritten;

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, double mutationRate, double eliteRate, int? seed=null){
=======

        // Event that is used in the console to notify the user that a file with the solutions has been written
        public event FileEventHandler FileWritten;

        public RobbyTheRobot (int nbGenerations, int populationSize, int nbTrials, double mutationRate, double eliteRate, int? seed=null){
            Debug.Assert(nbGenerations > 0, "Number of Generations must be a positive number");
            Debug.Assert(populationSize > 0, "Population size must be a positive number");
            Debug.Assert(nbTrials > 0, "Number of Trials must be a positive number");
            Debug.Assert(mutationRate > 0, "Mutation rate must be a positive number");
            Debug.Assert(eliteRate > 0, "Elite rate must be a positive number");

>>>>>>> main
            if(seed !=null){
                _seed = seed ;
            }
            NumberOfGenerations = nbGenerations;
            _populationSize = populationSize;
            GridSize = 100;
            NumberOfActions = 200;
            NumberOfTestGrids = nbTrials;
            MutationRate = mutationRate;
            EliteRate = eliteRate;
            _nbGenes = 243;
            _geneticAlg = GeneticLib.CreateGeneticAlgorithm(this._populationSize, this._nbGenes, Enum.GetNames(typeof(PossibleMoves)).Length, this.MutationRate, this.EliteRate, this.NumberOfTestGrids, ComputeFitness);
        }

<<<<<<< HEAD
        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            ContentsOfGrid[,] grid = new ContentsOfGrid[Convert.ToInt32(Math.Sqrt(GridSize)), Convert.ToInt32(Math.Sqrt(GridSize))];
            // sets the positions of the cans 
            List<int> randomCansPositions = generateRandomLocation();

            // sets all positions of the grid to start with empty
            for (int a=0; a<grid.GetLength(0); a++){
                for (int b=0; b<grid.GetLength(1); b++){
=======
        // This function generates the random grid for robby to run in. 
        // It uses a helper method GenerateRandomLocation() to set 50% to be empty and the other 50% filled with cans.
        public ContentsOfGrid[,] GenerateRandomTestGrid()
        {
            Debug.Assert(GridSize>0, "Invalid grid size" );
            ContentsOfGrid[,] grid = new ContentsOfGrid[Convert.ToInt32(Math.Sqrt(GridSize)), Convert.ToInt32(Math.Sqrt(GridSize))];
            // sets the positions of the cans 
            List<int> randomCansPositions = GenerateRandomLocation();

            // sets all positions of the grid to start with empty
            Debug.Assert(grid.GetLength(0) > 0, "Invalid grid row" );
            Debug.Assert(grid.GetLength(1) > 0, "Invalid grid column" );
            for (int a=0; a<grid.GetLength(0); a++)
            {
                for (int b=0; b<grid.GetLength(1); b++)
                {
>>>>>>> main
                    grid[a,b] = ContentsOfGrid.Empty;
                }
            }

            // sets the positions of the grid from the generateRandomLocation to have a can 
<<<<<<< HEAD
            for (int i=0; i<randomCansPositions.Count; i++)
            {
                // if the location contains 2 digits, it separates the digit and sets the grid at digit 1 and digit 2 to can
=======
            Debug.Assert(randomCansPositions.Count > 0, "Count of positions of the cans is not a positive number");
            for (int i=0; i<randomCansPositions.Count; i++)
            {
                // if the location contains 2 digits, it separates the digit and sets the grid at digit 1 and digit 2 to can
                Debug.Assert(randomCansPositions[i] >= 0, "Invalid can position");
>>>>>>> main
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

<<<<<<< HEAD
        // Method was tested, works!
        public void GeneratePossibleSolutions(string folderPath)
        {
=======
        // This is a helper method used by GenerateRandomTestGrid(), it creates a list of 50 random positions 
        // between 0 and 100 (the size of the grid). The method makes sure that all 50 positions to be filled with cans are unique and not repeated.
        private List<int> GenerateRandomLocation()
        {
            Random rnd = new Random();     
            int randomLocation;
            List<int> listRandomLocations = new List<int>();
            for (int i=0; i<50; i++){
                do {
                    randomLocation = rnd.Next(0, GridSize);
                    Debug.Assert(randomLocation >= 0, "Invalid random location, must be positive");
                    Debug.Assert(randomLocation < GridSize, "Invalid random location, must be smaller than the grid size");
                }
                while (listRandomLocations.Contains(randomLocation));
                listRandomLocations.Add(randomLocation);
            }
            return listRandomLocations;
        }

        // This method calls GenerateGeneration from the genetic algorithm and writes the information 
        // (max score, number of actions, moves) to the file using a helper method WriteToFile().
        public void GeneratePossibleSolutions(string folderPath)
        {
            Debug.Assert(folderPath != null, "Folder Path must be provided");
>>>>>>> main
            string fileName = "Generation";
            double maxScore = 0.0;
            int nbMoves = 200;
            int[] genes = null;

            IGeneration gen;
           
            for (int i=0; i<this.NumberOfGenerations; i++){
                gen = _geneticAlg.GenerateGeneration();
                maxScore = gen.MaxFitness;
                nbMoves = _nbGenes;
                genes = gen[0].Genes;
<<<<<<< HEAD
                // (if i%) save to file 1st, 20th, 100, 200, 500 and 1000th
                if (i == 0 || i==19 || i==99 || i==199 || i==499 || i==999)
                {        
                    int fileIndex = i + 1;
                    writeToFile(folderPath, fileName + fileIndex , maxScore, nbMoves, genes); 
                    //  not sure about the event param
                    // FileWritten?.Invoke(folderPath + maxScore + nbMoves + genes[i]);
                    FileWritten?.Invoke($"Generation {fileIndex}: MaxFitness {maxScore}, Number of Moves {nbMoves}");
                    
=======
                // Saves to file 1st, 20th, 100, 200, 500 and 1000th generations
                if (i == 0 || i==19 || i==99 || i==199 || i==499 || i==999)
                {        
                    int fileIndex = i+1;
                    try{
                        writeToFile(folderPath, fileName + fileIndex, maxScore, nbMoves, genes); 
                        FileWritten?.Invoke(" The max score, the number of moves and the genes were written to " + fileName);
                    } catch {
                        Console.WriteLine("An error occured then writing data to the file");
                    }
>>>>>>> main
                }
            }  
        }

<<<<<<< HEAD
        // This methods writes results of a generation from the genetic algorithm to a file
        // If the folder does not exist, it creates it
        private void writeToFile(string folderPath, string fileName, double maxScore, int nbMoves, int[] genes){
=======
        // This helper method writes results of a generation from the genetic algorithm to a file
        // If the folder does not exist, it creates it
        private void writeToFile(string folderPath, string fileName, double maxScore, int nbMoves, int[] genes){
            Debug.Assert(folderPath != null, "Folder Path must be provided");
            Debug.Assert(fileName != null, "File name must be provided");
            Debug.Assert(nbMoves > 0, "Number of moves must be a positive number");
            Debug.Assert(genes != null, "Genes array must be provided");

>>>>>>> main
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);
            
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Max score " + maxScore);
                writer.WriteLine("Number of moves " + nbMoves);
                writer.Write("Robby's actions ");
                foreach (int gene in genes)
                {
                    writer.Write(gene + " ");
                }
            }
        }

<<<<<<< HEAD
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
        public double ComputeFitness(IChromosome chromosome, IGeneration gen){
=======

        // This method calculates the fitness of a chromosome looping through the number of actions.
        // calls generateRandomGrid, runs Robby through grid, scoring moves 
        private double ComputeFitness(IChromosome chromosome, IGeneration gen){
            Debug.Assert(chromosome != null, "Chromosome must be provided");
            Debug.Assert(gen != null, "Generation must be provided");
>>>>>>> main
            // calls the generate grid
            ContentsOfGrid[,] grid = GenerateRandomTestGrid();
            // generates Robby's moves from geneticAlgorithm
            int [] moves = chromosome.Genes;
            // generating random for the first position at x and y (position between 0 and 10, since width and height are 10)
            Random rnd = _seed != null ? new Random((int)_seed) : new Random();
            int xPos = rnd.Next(0, 10);
            int yPos = rnd.Next(0, 10);
            double fitness = 0;
            for (int i=0; i<this.NumberOfActions; i++){
                fitness = fitness + RobbyHelper.ScoreForAllele(moves, grid, rnd, ref xPos, ref yPos);
            }
            return fitness;
        }
<<<<<<< HEAD
    
=======

>>>>>>> main

    }
}
