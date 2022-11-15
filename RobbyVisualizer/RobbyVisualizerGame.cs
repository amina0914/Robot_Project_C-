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

        // private FileExplorer _fileExplorer;


        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _robbyPosX =60;
            _robbyPosY=60;
            _robbySprite = new RobbySprite(this, _robbyPosX, _robbyPosY);
            timer = new Stopwatch();
            offset = 200;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000; 
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            timer.Start(); 


            // FileExplorer 
            List<int> moves = new List<int>();
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
            // RobbySprite robbySprite = new RobbySprite(this, 0, 0);

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

                    // hardcoded initial value
                    // Task t = Task.Run( () => {
                    // for(int x=0; x<moves.Count; x++){
                    //     // Task t = Task.Run( () => {
                    //         robbySprite = MoveRobby(moves[x], robbySprite);
                    //         Components.Add(robbySprite);
                    //     // });
                    //     // t.Wait(ts);
                    // }    });
                    // TimeSpan ts = TimeSpan.FromMilliseconds(3000);
                    //  t.Wait(ts);

                    SimulationSprite newGridSquare = new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
                    // Components.Add(robbySprite);
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

            // TODO: Add your update logic here
            if(_robbySprite.PosX > maxX || _robbyPosY > maxY) {
                
            } else {
            if(timer.ElapsedMilliseconds >= offset) {
                int[] moves = {0,1,2,3,4,5};    
                for(int x=0; x<moves.Length; x++)
                {      
                // Task t = Task.Run( () => {
                    MoveRobby(moves[x]);
                    // Components.Remove(_robbySprite);
                    //         Components.Add(_robbySprite);
                       
                // });
               // TimeSpan ts = TimeSpan.FromMilliseconds(100);
                // Thread.Sleep(ts);
              
                }
                timer.Reset();
                timer.Start();
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
        // * Ask Dirk if can assume where, in file, are the moves
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
            // int x=RobbySprite.PosX;
            // int y=RobbySprite.PosY;
            // RobbySprite.PosX = (x + 60);
            // RobbySprite.PosY = (y+ 60);
            // System.Windows.Forms.MessageBox.Show(RobbySprite.PosX.ToString() + RobbySprite.PosY.ToString());
          
            // _robbyPosX = _robbyPosX + 60;
            // _robbyPosY = _robbyPosY + 60;

            // System.Windows.Forms.MessageBox.Show(_robbyPosX.ToString());
            // RobbySprite robbySprite = new RobbySprite(this, _robbyPosX, _robbyPosY);
          
        //    _robbySprite = new RobbySprite(this, _robbyPosX, _robbyPosY);

            // return robbySprite;
            _robbySprite.PosX += 1;
            _robbySprite.PosY+= 1;
         
        }

    }
}
