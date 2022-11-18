/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 19-11-2022
@description: This Robby Controller class that is responsible for creating a Robby sprite
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace RobbyVisualizer
{
    public class RobbySprite : DrawableGameComponent
    {
        private RobbyVisualizerGame _game;
        public int PosX{get;set;}
        public int PosY{get;set;}
        private int _sizeX;
        private int _sizeY;
        private Color _color; 
    
        private Texture2D _robbyTexture;
        public RobbySprite(RobbyVisualizerGame robbyGame, int posX, int posY): base(robbyGame)
        {
            this._game = robbyGame;
            this._color = Color.White;
            PosX = posX;
            PosY = posY;
            this._sizeX = 60; 
            this._sizeY = 60;
        }

        protected override void LoadContent()
        {
            this._robbyTexture = this._game.Content.Load<Texture2D>("robby");
        }


        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_robbyTexture,  new Rectangle(PosX, PosY, _sizeX, _sizeY), _color);
            _game.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}