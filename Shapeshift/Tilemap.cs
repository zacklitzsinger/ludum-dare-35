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
    class Tile
    {
        public int index;
        public bool solid;
    }

    class Tilemap
    {
        TextureAtlas atlas = new TextureAtlas("Tilemap");
        Dictionary<Point, Tile> tilemap = new Dictionary<Point, Tile>();

        public Tilemap()
        {
            LoadTilemap();
        }

        public void LoadTilemap()
        {
            int y = 0;
            string line;
            StreamReader file = new StreamReader("Content\\map.txt");
            while ((line = file.ReadLine()) != null)
            {
                var tiles = line.Split(' ');
                for (int x = 0; x < tiles.Length; x++)
                {
                    int tileType = int.Parse(tiles[x]);
                    tilemap.Add(new Point(x, y), new Tile() { index = tileType, solid = (tileType > 0) });
                }
                y++;
            }
        }

        public void LoadContent()
        {
            atlas.LoadContent();
        }

        public Point GetTilePoint(Point position)
        {
            return new Point(position.X / Constants.TILE_SIZE, position.Y / Constants.TILE_SIZE);
        }

        public Tile GetTile(Point position)
        {
            Point point = GetTilePoint(position);
            if (tilemap.ContainsKey(point))
                return tilemap[point];
            return null;
        }

        public void SetTile(Point tilePosition, Tile newTile)
        {
            tilemap[tilePosition] = newTile;
        }

        public void Draw()
        {
            foreach(KeyValuePair<Point, Tile> kvp in tilemap)
            {
                var loc = kvp.Key;
                var tile = kvp.Value;
                atlas.Draw(new Vector2(loc.X * Constants.TILE_SIZE, loc.Y * Constants.TILE_SIZE), tile.index);
            }
        }
    }
}
