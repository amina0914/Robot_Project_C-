/**
@author: Amina Turdalieva 
@student id: 2035572
<<<<<<< HEAD
@date: 
=======
@date: 19-11-2022
>>>>>>> main
@description: This is the sprite class that is responsible for creating a new sprite representing the tile in the grid and drawing it on the screen.
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
<<<<<<< HEAD
=======
using System.IO;
>>>>>>> main

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
<<<<<<< HEAD
        private bool _isRobbyHere;
        private Texture2D _robbyTexture;
=======
        public bool IsRobbyHere{get;set;}
        private Texture2D _robbyTexture;
        private Texture2D _texture2;

        public SpriteBatch SpriteBatchTile;
>>>>>>> main
        public SimulationSprite(RobbyVisualizerGame robbyGame, int posX, int posY, bool isEmpty, bool isRobbyHere): base(robbyGame)
        {
            this._game = robbyGame;
            this._color = Color.CornflowerBlue;
            this._posX = posX;
            this._posY = posY;
            this._sizeX = 60; 
            this._sizeY = 60;
            this._isEmpty = isEmpty;
<<<<<<< HEAD
            this._isRobbyHere = isRobbyHere;
        }
        protected override void LoadContent()
        {
            this._canTexture = this._game.Content.Load<Texture2D>("can");
            this._robbyTexture = this._game.Content.Load<Texture2D>("robby");
=======
            this.IsRobbyHere = isRobbyHere;
        }

        protected override void LoadContent()
        {
            this.SpriteBatchTile = new SpriteBatch(GraphicsDevice);
            this._canTexture = this._game.Content.Load<Texture2D>("can");
            this._robbyTexture = this._game.Content.Load<Texture2D>("robby");
            // blue rectangle gets displayed on the spot where Robby picked a can
            this._texture2 = this._game.Content.Load<Texture2D>("blue_rectangle");
>>>>>>> main
        }


        public override void Draw(GameTime gameTime)
        {
<<<<<<< HEAD
            _game.SpriteBatch.Begin();
            _game.SpriteBatch.Draw(_game.Texture,  new Rectangle(_posX, _posY, _sizeX, _sizeY), _color);
            if (!_isEmpty){
                _game.SpriteBatch.Draw(this._canTexture,  new Rectangle(_posX+5, _posY+5, _sizeX-10, _sizeY-10), Color.White);  
            }
            if (_isRobbyHere){
                _game.SpriteBatch.Draw(this._robbyTexture,  new Rectangle(_posX+5, _posY+5, _sizeX-10, _sizeY-10), Color.White);  
            }
            _game.SpriteBatch.End();
=======
            SpriteBatchTile.Begin();
            SpriteBatchTile.Draw(_game.Texture,  new Rectangle(_posX, _posY, _sizeX, _sizeY), _color);
            // draws a can if the tile is not empty, the drawn can is a little smaller than the tile 
            if (!_isEmpty){
                SpriteBatchTile.Draw(this._canTexture,  new Rectangle(_posX+5, _posY+5, _sizeX-10, _sizeY-10), Color.White);  
            }
            // draws a different light blue tile when robby picks a can, the drawn tile is a little smaller than the grid tile 
            if (IsRobbyHere){
                SpriteBatchTile.Draw(this._texture2,  new Rectangle(_posX+5, _posY+5, _sizeX-10, _sizeY-10), Color.White);  
            }
            SpriteBatchTile.End();
>>>>>>> main
            base.Draw(gameTime);
        }
    }
}