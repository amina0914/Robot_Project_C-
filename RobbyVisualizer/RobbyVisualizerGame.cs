/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 19-11-2022
@description: This is the main visualizer class, it displays the grid and displays robby moving. 
*/
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
        private Stopwatch _timer; 
        private int _totalNumberMoves;
        private ContentsOfGrid[,] _robbyGrid;
        private int _offset;

        private int _moveCount;
        private double _score;
        private List<int> _moves;    
        private int[] _arrayMoves;
        private IRobbyTheRobot _robby;
        private SpriteFont _font;
        private Random _rand = new Random();
        private string[] myfiles;
        private int _generation = 0;

        private String generationNumber;

        private List<SimulationSprite> _displayedGrid = new List<SimulationSprite>();
        private List<SimulationSprite> _pickedCansGrid = new List<SimulationSprite>();

        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _robbyPosX = _rand.Next(0,10);
            _robbyPosY= _rand.Next(0,10);
            _robbySprite = new RobbySprite(this,  (_robbyPosX*60)+200, (_robbyPosY*60)+10);
            _timer = new Stopwatch();
            _offset = 100;
            _moveCount = 0;
            _totalNumberMoves = 200;
            _score = 0;
            _robby = Robby.CreateRobby(1000, 200, 100, 0.05, 0.05);
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
            }

            _robbyGrid = _robby.GenerateRandomTestGrid();
            DrawGrid();

            Components.Add(_robbySprite);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Texture = Content.Load<Texture2D>("square");
            this._backgroundTexture = Content.Load<Texture2D>("background");
            this._folderTexture = Content.Load<Texture2D>("folder");
            _font = Content.Load<SpriteFont>("font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            

            if (_generation<myfiles.Length)
            {
                
                    _moves = GetMoves(myfiles[_generation]);
                    _arrayMoves = _moves.ToArray();

                     if(_moveCount < _totalNumberMoves)
                    {
                        _timer.Start();
                        if(_timer.ElapsedMilliseconds >= _offset) 
                        {                                
                            MoveRobby();
                            _moveCount++; 
                            _timer.Reset();
                         } 
                    }   
                    else 
                    {
                    }
                
                    
                   
                    if (_moveCount==200){
                        _moves = null;
                        _generation ++;
                        _moveCount = 0;
                        // removes the grid
                        foreach(SimulationSprite gridSquare in _displayedGrid){
                            Components.Remove(gridSquare);
                        }
                        // removes the pciked cans blue squares
                        foreach(SimulationSprite gridCan in _pickedCansGrid){
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
            SpriteBatch.DrawString(_font, "Generation: " + generationNumber, new Vector2(0, 0), Color.Black);
            SpriteBatch.DrawString(_font, "Move number: " + _moveCount + "/"+_totalNumberMoves, new Vector2(0, 20), Color.Black);
            SpriteBatch.DrawString(_font, "Current score: " + _score, new Vector2(0, 40), Color.Black);
            SpriteBatch.End();
            base.Draw(gameTime);
        }


        // Reads provided file, gets the moves list
        private List<int> GetMoves(String filePath){
           generationNumber = Path.GetFileName(filePath);
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
            double previousScore = _score;
            _score += RobbyHelper.ScoreForAllele(_arrayMoves, _robbyGrid, _rand, ref _robbyPosX, ref _robbyPosY);
            _robbySprite.PosX = (_robbyPosX*60)+200;
            _robbySprite.PosY = (_robbyPosY*60)+10;
            if (_score == (previousScore + 10)){
                SimulationSprite GridSquare = new SimulationSprite(this, _robbySprite.PosX,  _robbySprite.PosY, false, true);
                _pickedCansGrid.Add(GridSquare);
                Components.Add(GridSquare);
            }
        }

        private void DrawGrid(){
            _robbyGrid = _robby.GenerateRandomTestGrid();
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
                    if (_robbyGrid[b,a] == ContentsOfGrid.Can)
                    {
                        isEmpty = false;
                    } else {
                        isEmpty = true;
                    }
                    SimulationSprite newGridSquare= new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
                    _displayedGrid.Add(newGridSquare);
                    Components.Add(newGridSquare);
                    posX = posX + 60; 
                }
                posX = initialPosX;
                posY = posY + 60;
            }

        }

    }
}
