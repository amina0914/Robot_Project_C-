using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace RobbyVisualizer
{
    public sealed class FileExplorer: DrawableGameComponent
    {
        private static FileExplorer _instance = null;
        private static readonly object _padlock = new object();
        private RobbyVisualizerGame _game;

        private FileExplorer(RobbyVisualizerGame robbyGame) : base(robbyGame)
        {
            this._game = robbyGame;
        }

        public static FileExplorer Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new FileExplorer(_instance._game);
                    }
                    return _instance;
                }
            }
        }

        public void GetFile()
        {
           string path;
           OpenFileDialog ofdSelectLayout = new OpenFileDialog();
            if(ofdSelectLayout.ShowDialog() == DialogResult.OK)
            {
                path = ofdSelectLayout.FileName;
            }
            //some code
        }

    }
}