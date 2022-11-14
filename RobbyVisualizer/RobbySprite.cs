/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 
@description: This Robby Controller class that is responsible for moving Robby in the grid
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
        private int _posX;
        private int _posY;
        private int _sizeX;
        private int _sizeY;
        private Color _color; 
    
        private Texture2D _robbyTexture;
        public RobbySprite(RobbyVisualizerGame robbyGame, int posX, int posY): base(robbyGame)
        {
            this._game = robbyGame;
            this._color = Color.White;
            this._posX = posX;
            this._posY = posY;
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
            _game.SpriteBatch.Draw(_robbyTexture,  new Rectangle(_posX, _posY, _sizeX, _sizeY), _color);
            _game.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}