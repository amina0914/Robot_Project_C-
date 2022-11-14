/**
@author: Amina Turdalieva 
@student id: 2035572
@date: 
@description: This is the sprite class that is responsible for creating a new sprite representing the tile in the grid and drawing it on the screen.
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

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
        private bool _isEmpty;
        private Texture2D _canTexture;
        private bool _isRobbyHere;
        private Texture2D _robbyTexture;
        public SimulationSprite(RobbyVisualizerGame robbyGame, int posX, int posY, bool isEmpty, bool isRobbyHere): base(robbyGame)
        {
            this._game = robbyGame;
            this._color = Color.CornflowerBlue;
            this._posX = posX;
            this._posY = posY;
            this._sizeX = 60; 
            this._sizeY = 60;
            this._isEmpty = isEmpty;
            this._isRobbyHere = isRobbyHere;
        }

        protected override void LoadContent()
        {
            this._canTexture = this._game.Content.Load<Texture2D>("can");
            this._robbyTexture = this._game.Content.Load<Texture2D>("robby");
        }


        public override void Draw(GameTime gameTime)
        {
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_game.Texture,  new Rectangle(_posX, _posY, _sizeX, _sizeY), _color);
            if (!_isEmpty){
                _game.SpriteBatch.Draw(this._canTexture,  new Rectangle(_posX+5, _posY+5, _sizeX-10, _sizeY-10), Color.White);  
            }
            if (_isRobbyHere){
                _game.SpriteBatch.Draw(this._robbyTexture,  new Rectangle(_posX+5, _posY+5, _sizeX-10, _sizeY-10), Color.White);  
            }
            _game.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}