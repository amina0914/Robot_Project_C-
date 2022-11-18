﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using RobbyTheRobot;
using GeneticAlgorithm;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;
        public Texture2D Texture;
        private Texture2D _backgroundTexture;
        private Texture2D _folderTexture;
        private RobbySprite _robbySprite;
        private int _robbyPosX;
        private int _robbyPosY;
        private Stopwatch timer; 
        private int totalNumberMoves;
        private ContentsOfGrid[,] robbyGrid;
        private int offset;

        private int moveCount;
        private double score;
        private List<int> moves;    
        private int[] arrayMoves;
        private IRobbyTheRobot robby;
        private SpriteFont font;
        private Random rand = new Random();

        private string[] myfiles;

        private int generation =0;

        private List<SimulationSprite> displayedGrid = new List<SimulationSprite>();
        private List<SimulationSprite> pickedCansGrid = new List<SimulationSprite>();

        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // _robbyPosX =200;
            // _robbyPosY=10;
            _robbyPosX = rand.Next(0,10);
            _robbyPosY= rand.Next(0,10);
            _robbySprite = new RobbySprite(this,  (_robbyPosX*60)+200, (_robbyPosY*60)+10);
            timer = new Stopwatch();
            offset = 100;
            moveCount = 0;
            totalNumberMoves = 200;
            score = 0;
            robby = Robby.CreateRobby(1000, 200, 100, 0.05, 0.05);

            //robby.GeneratePossibleSolutions("C:/Users/ganco/OneDrive/Desktop/robby");
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000; 
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            // FileExplorer 
            System.Windows.Forms.MessageBox.Show("Select Folder");
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath)) 
            {
               myfiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                Array.Sort(myfiles);
                // foreach(string generationFile in myfiles)
                // {
                //     moves = GetMoves(generationFile);
                //     arrayMoves = moves.ToArray();
                // }
                
            }

            robbyGrid = robby.GenerateRandomTestGrid();

            // SimulationSprite[,] grid = new SimulationSprite[10,10];
            // int initialPosX = 200;
            // int initialPosY = 10;
            // int posX=initialPosX;
            // int posY=initialPosY;
            // bool isEmpty = true;
            // bool isRobbyHere = false;

            // for (int a=0; a<grid.GetLength(0); a++)
            // {
            //     for (int b=0; b<grid.GetLength(1); b++)
            //     {
            //         if (robbyGrid[b,a] == ContentsOfGrid.Can)
            //         {
            //             isEmpty = false;
            //         } else {
            //             isEmpty = true;
            //         }
            //         SimulationSprite newGridSquare = new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
            //         Components.Add(newGridSquare);
            //         posX = posX + 60; 
            //     }
            //     posX = initialPosX;
            //     posY = posY + 60;
            // }
            DrawGrid();

            Components.Add(_robbySprite);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Texture = Content.Load<Texture2D>("square");
            // this.Texture = Content.Load<Texture2D>("blue_rectangle");
            this._backgroundTexture = Content.Load<Texture2D>("background");
            this._folderTexture = Content.Load<Texture2D>("folder");
            font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

            if (generation<myfiles.Length)
            {
                
                    moves = GetMoves(myfiles[generation]);
                    arrayMoves = moves.ToArray();

                     if(moveCount < totalNumberMoves)
                    {
                        timer.Start();
                        if(timer.ElapsedMilliseconds >= offset) 
                        {                                
                            MoveRobby();
                            moveCount++; 
                            timer.Reset();
                         } 
                    }   
                    else 
                    {
                    }
                
                    
                   
                    if (moveCount==200){
                        moves = null;
                        generation ++;
                        moveCount = 0;
                        // removes the grid
                        foreach(SimulationSprite gridSquare in displayedGrid){
                            Components.Remove(gridSquare);
                        }
                        // removes the pciked cans blue squares
                        foreach(SimulationSprite gridCan in pickedCansGrid){
                            Components.Remove(gridCan);
                        }
                        DrawGrid();
                    }
                }
                else {

                }
            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            SpriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            // SpriteBatch.Draw(_folderTexture, new Rectangle(450, 600, 150, 120), Color.CornflowerBlue);
            SpriteBatch.DrawString(font, "Generation: " + generation, new Vector2(0, 0), Color.Black);
            SpriteBatch.DrawString(font, "Move number: " + moveCount + "/"+totalNumberMoves, new Vector2(0, 20), Color.Black);
            SpriteBatch.DrawString(font, "Current score: " + score, new Vector2(0, 40), Color.Black);
        
            // if(moveCount < totalNumberMoves)
            // {
            //    timer.Start();
            //     if(timer.ElapsedMilliseconds >= offset) 
            //     {                                
            //         MoveRobby();
            //         moveCount++; 
            //         timer.Reset();
                    
            //     } 
            // }
            // else 
            // {

            // }
            SpriteBatch.End();
            base.Draw(gameTime);
        }


        // Reads provided file, gets the moves list
        private List<int> GetMoves(String filePath){
            List <int> moves = new List<int>();
                String line = System.IO.File.ReadLines(filePath).Skip(2).Take(1).First();
                char[] lines= line.ToArray();
                for (int i=0; i<lines.Length; i++){
                    // converts to int and pushes to list
                    if(Char.IsNumber(lines[i])){
                        moves.Add(line[i] - '0');
                    }
                    
                }
            return moves;
        }

        // using score for allele
        private void MoveRobby(){
            double previousScore = score;
            score += RobbyHelper.ScoreForAllele(arrayMoves, robbyGrid, rand, ref _robbyPosX, ref _robbyPosY);
            _robbySprite.PosX = (_robbyPosX*60)+200;
            _robbySprite.PosY = (_robbyPosY*60)+10;
            if (score == (previousScore + 10)){
                SimulationSprite GridSquare = new SimulationSprite(this, _robbySprite.PosX,  _robbySprite.PosY, false, true);
                pickedCansGrid.Add(GridSquare);
                Components.Add(GridSquare);
            }
        }

        private void DrawGrid(){
            robbyGrid = robby.GenerateRandomTestGrid();
            SimulationSprite[,] grid = new SimulationSprite[10,10];
            int initialPosX = 200;
            int initialPosY = 10;
            int posX=initialPosX;
            int posY=initialPosY;
            bool isEmpty = true;
            bool isRobbyHere = false;

            for (int a=0; a<grid.GetLength(0); a++)
            {
                for (int b=0; b<grid.GetLength(1); b++)
                {
                    if (robbyGrid[b,a] == ContentsOfGrid.Can)
                    {
                        isEmpty = false;
                    } else {
                        isEmpty = true;
                    }
                    SimulationSprite newGridSquare= new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
                    displayedGrid.Add(newGridSquare);
                    Components.Add(newGridSquare);
                    posX = posX + 60; 
                }
                posX = initialPosX;
                posY = posY + 60;
            }

        }

    }
}
