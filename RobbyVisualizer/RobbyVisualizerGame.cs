using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
//for now to test using RobbyTheRobot app, later when console app done will need to use RobbyIterationGenerator instead
using RobbyTheRobot;
using GeneticAlgortihmLib;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;

        public Texture2D Texture;

        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 900; 
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();     

            //creating robby obj, so can create a grid with either empty or cans, will need to use console later
            IRobbyTheRobot robby = Robby.CreateRobby(300, 400, 70, 50);
            ContentsOfGrid[,] robbyGrid = robby.GenerateRandomTestGrid();

            SimulationSprite[,] grid = new SimulationSprite[10,10];
            int initialPos = 50;
            int posX=initialPos;
            int posY=initialPos;
            bool isEmpty = true;
            for (int a=0; a<grid.GetLength(0); a++)
            {
                for (int b=0; b<grid.GetLength(1); b++)
                {
                    if (robbyGrid[a,b] == ContentsOfGrid.Can)
                    {
                        isEmpty = false;
                    }
                    SimulationSprite newGridSquare = new SimulationSprite(this, posX, posY, isEmpty);
                    Components.Add(newGridSquare);
                    posX = posX + 80; 
                    isEmpty = true;
                }
                posX = initialPos;
                posY = posY + 80;
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Texture = Content.Load<Texture2D>("square");
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
            base.Draw(gameTime);
        }
    }
}
