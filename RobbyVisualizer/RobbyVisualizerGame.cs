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
        private int maxX = 500;

        private int maxY = 500;

        private Stopwatch timer; 

        private int offset;

        private int moveCount;

        private List<int> moves;    
        // private FileExplorer _fileExplorer;


        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _robbyPosX =200;
            _robbyPosY=10;
            _robbySprite = new RobbySprite(this, _robbyPosX, _robbyPosY);
            timer = new Stopwatch();
            offset = 2000;
            moveCount = 0;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000; 
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            // FileExplorer 
            // List<int> moves = new List<int>();
            System.Windows.Forms.MessageBox.Show("Select Folder");
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath)) 
            {
                string[] myfiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                Array.Sort(myfiles);
                foreach(string generationFile in myfiles)
                {
                    // read data, run Robby          
                    moves = GetMoves(generationFile);
                }
                
            }

            IRobbyTheRobot robby = Robby.CreateRobby(300, 400, 50, 0.5, 1, 4);
            ContentsOfGrid[,] robbyGrid = robby.GenerateRandomTestGrid();

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
                    // // hardcoded robby position
                    // if (a==0 && b ==0)
                    // {
                    //     isRobbyHere = true;
                    // }
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
            this._backgroundTexture = Content.Load<Texture2D>("background");
            this._folderTexture = Content.Load<Texture2D>("folder");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(moveCount > 10) 
            {
                
            } 
            else 
            {
                timer.Start();
                if(timer.ElapsedMilliseconds >= offset) 
                {
                                    // System.Windows.Forms.MessageBox.Show("inside if");
                   
                    MoveRobby(moves[moveCount]);
                    //  foreach (int move in moves){
                    // }
                    moveCount++;                                
                    timer.Reset();
                } 
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            SpriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            // SpriteBatch.Draw(_folderTexture, new Rectangle(450, 600, 150, 120), Color.CornflowerBlue);
            SpriteBatch.End();
            base.Draw(gameTime);
        }


        // Reads provided file, gets the moves list
        // * For now, assumes that the moves are at the 3rd line, will need to be fixed
        // * Ask Dirk if can assume where, in file, are the moves YES we can
        private List<int> GetMoves(String filePath){
            List <int> moves = new List<int>();
            // foreach (string line in System.IO.File.ReadLines(filePath))
            // {  
                String line = System.IO.File.ReadLines(filePath).Skip(2).Take(1).First();
                Console.WriteLine(line); 
                for (int i=0; i<line.Length; i++){
                    // converts to int and pushes to list
                    moves.Add(line[i] - '0');
                }
            // }  
            return moves;
        }

        // This method has switch cases that will tell where to display robby based on his move
        private void MoveRobby(int move){
            if (move == 0){
                _robbySprite.PosY-= 60;
            } else if (move == 1){
                _robbySprite.PosY= _robbySprite.PosY+60;
            } else if (move == 2){
                _robbySprite.PosX+= 60;
            }
             else if (move == 3){
                _robbySprite.PosX= _robbySprite.PosX-60;;
            } else if (move == 4){
                _robbySprite.PosX+= 60;
            } else {
                  _robbySprite.PosX= _robbySprite.PosX;
                  _robbySprite.PosY= _robbySprite.PosY;
            }
           // use score for allele
             
        }

    }
}
