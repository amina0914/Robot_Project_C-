/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 
@description: This is the sprite class that is responsible for creating a new sprite representing the tile in the grid and drawing it on the screen.
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RobbyVisualizer
{
    public class SimulationSprite : DrawableGameComponent
    {
        private RobbyVisualizerGame _game;
        private int _posX;
        private int _posY;
        private int _sizeX;
        private int _sizeY;
        private Color _color; 
        public SimulationSprite(RobbyVisualizerGame robbyGame, int posX, int posY): base(robbyGame)
        {
            this._game = robbyGame;
            this._color = Color.White;
            this._posX = posX;
            this._posY = posY;
            this._sizeX = 80; 
            this._sizeY = 80;
        }

        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_game.Texture,  new Rectangle(_posX, _posY, _sizeX, _sizeY), _color);
            _game.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}