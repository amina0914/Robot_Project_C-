using Microsoft.Xna.Framework;
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
            offset = 700;
            moveCount = 0;
            totalNumberMoves = 200;
            score = 0;
            robby = Robby.CreateRobby(200, 200, 100, 0.05, 0.05);
            robbyGrid = robby.GenerateRandomTestGrid();

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
                string[] myfiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                Array.Sort(myfiles);
                foreach(string generationFile in myfiles)
                {
                    moves = GetMoves(generationFile);
                    arrayMoves = moves.ToArray();
                }
                
            }

            

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
                    if (robbyGrid[a,b] == ContentsOfGrid.Can)
                    {
                        isEmpty = false;
                    }
                    SimulationSprite newGridSquare = new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
                    Components.Add(newGridSquare);
                    posX = posX + 60; 
                    isEmpty = true;
                    isRobbyHere = false;
                }
                posX = initialPosX;
                posY = posY + 60;
            }

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
            Random rand= new Random();
            
             
            
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
            //         if (robbyGrid[a,b] == ContentsOfGrid.Can)
            //         {
            //             isEmpty = false;
            //         }
            //         SimulationSprite newGridSquare = new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
            //         Components.Add(newGridSquare);
            //         posX = posX + 60; 
            //         isEmpty = true;
            //         isRobbyHere = false;
            //     }
            //     posX = initialPosX;
            //     posY = posY + 60;
            // }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            SpriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            // SpriteBatch.Draw(_folderTexture, new Rectangle(450, 600, 150, 120), Color.CornflowerBlue);
            SpriteBatch.DrawString(font, "Generation: " , new Vector2(0, 0), Color.Black);
            SpriteBatch.DrawString(font, "Move number: " + moveCount + "/"+totalNumberMoves, new Vector2(0, 20), Color.Black);
            SpriteBatch.DrawString(font, "Current score: " + score, new Vector2(0, 40), Color.Black);
        
            if(moveCount < totalNumberMoves)
            {
               timer.Start();
                if(timer.ElapsedMilliseconds >= offset) 
                {                                
                    MoveRobby2();
                    moveCount++; 
                    timer.Reset();
                    
                } 
            }
            else 
            {

            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }


        // Reads provided file, gets the moves list
        private List<int> GetMoves(String filePath){
            List <int> moves = new List<int>();
                String line = System.IO.File.ReadLines(filePath).Skip(2).Take(1).First();
                char[] lines= line.ToArray();
                Console.WriteLine(line); 
                for (int i=0; i<lines.Length; i++){
                    // converts to int and pushes to list
                    if(Char.IsNumber(lines[i])){
                        moves.Add(line[i] - '0');
                    }
                    
                }
            return moves;
        }

        // // This method has switch cases that will tell where to display robby based on his move
        private void MoveRobby(int move){
            if (move == 0){
               _robbySprite.PosY-= 60;
            } else if (move == 1){
                _robbySprite.PosY= _robbySprite.PosY+60;
            } else if (move == 2){
                _robbySprite.PosX+= 60;
            }
             else if (move == 3){
                _robbySprite.PosX= _robbySprite.PosX-60;
            } else if (move == 4){
                _robbySprite.PosX+= 60;
            }
            else if (move == 5){
                SimulationSprite newGridSquare = new SimulationSprite(this, _robbySprite.PosX, _robbySprite.PosY, true, true);
                Components.Add(newGridSquare);
            } else {
                  _robbySprite.PosX= _robbySprite.PosX;
                  _robbySprite.PosY= _robbySprite.PosY;
            }             
        }

        // using score for allele
        private void MoveRobby2(){
            double previousScore = score;
            score += RobbyHelper.ScoreForAllele(arrayMoves, robbyGrid, rand, ref _robbyPosX, ref _robbyPosY);
            _robbySprite.PosX = (_robbyPosX*60)+200;
            _robbySprite.PosY = (_robbyPosY*60)+10;
            if (score == (previousScore + 10)){
                Console.WriteLine("inside if of moveRobby, previous score " + previousScore + " new score " + score);
                SimulationSprite GridSquare = new SimulationSprite(this, _robbySprite.PosX,  _robbySprite.PosY, false, true);
                Components.Add(GridSquare);
            }
        }

    }
}
