using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapeshift
{

    class TextureAtlas
    {
        string filename;
        Texture2D texture;
        int TilesWide { get { return texture.Width / tileWidth;  } }
        int tileWidth;
        int tileHeight;

        public TextureAtlas(string filename, int tileWidth = Constants.TILE_SIZE, int tileHeight = Constants.TILE_SIZE)
        {
            this.filename = filename;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public void LoadContent()
        {
            if (filename == null)
            {
                throw new Exception("No filename to load from!");
            }
            texture = Renderer.Instance.Load<Texture2D>(filename);
        }

        Rectangle FindSourceRectangle(int index, int xWidth = 1, int yWidth = 1)
        {
            var x = index % TilesWide;
            var y = index / TilesWide;
            return new Rectangle(x * tileWidth, y * tileHeight, xWidth * tileWidth, yWidth * tileHeight);
        }

        public void Draw(Vector2 pos, int index, int xWidth = 1, int yWidth = 1)
        {
            var sourceRect = FindSourceRectangle(index, xWidth, yWidth);
            Renderer.Instance.Draw(texture, pos, sourceRect, Color.White);
        }
    }
}
