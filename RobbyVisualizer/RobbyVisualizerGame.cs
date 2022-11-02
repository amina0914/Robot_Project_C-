﻿using Microsoft.Xna.Framework;
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
        private Texture2D _backgroundTexture;

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
            bool isRobbyHere = false;
            for (int a=0; a<grid.GetLength(0); a++)
            {
                for (int b=0; b<grid.GetLength(1); b++)
                {
                    if (robbyGrid[a,b] == ContentsOfGrid.Can)
                    {
                        isEmpty = false;
                    }
                    if (a==0 && b ==0)
                    {
                        isRobbyHere = true;
                    }
                    SimulationSprite newGridSquare = new SimulationSprite(this, posX, posY, isEmpty, isRobbyHere);
                    Components.Add(newGridSquare);
                    posX = posX + 80; 
                    isEmpty = true;
                    isRobbyHere = false;
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
            this._backgroundTexture = Content.Load<Texture2D>("background");
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
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
