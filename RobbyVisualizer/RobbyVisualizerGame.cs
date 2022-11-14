using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
//for now to test using RobbyTheRobot app, later when console app done will need to use RobbyIterationGenerator instead
using RobbyTheRobot;
using GeneticAlgorithm;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;

        public Texture2D Texture;
        private Texture2D _backgroundTexture;
        private Texture2D _folderTexture;

      //  private FileExplorer _fileExplorer = FileExplorer.Instance;

        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
          //  _fileExplorer = FileExplorer.Instance;

            _graphics.PreferredBackBufferWidth = 1000; 
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();     

            //creating robby obj, so can create a grid with either empty or cans, will need to use console later
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
                    // hardcoded robby position
                    if (a==0 && b ==0)
                    {
                        isRobbyHere = true;
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();
            SpriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            SpriteBatch.Draw(_folderTexture, new Rectangle(450, 600, 150, 120), Color.CornflowerBlue);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
