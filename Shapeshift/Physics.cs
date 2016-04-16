using Microsoft.Xna.Framework;
using System;

namespace Shapeshift
{
    [Flags]
    public enum Direction
    {
        NONE = 0,
        LEFT = 1,
        UP = 2,
        RIGHT = 4,
        DOWN = 8
    }

    public struct CollisionData
    {
        public Rectangle newPosition;
        public Direction collisions;
    }


    class Physics
    {        
        public Tilemap tilemap;

        private static Physics instance;
        private Physics() { }
        public static Physics Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Physics();
                }
                return instance;
            }
        }

        private bool GetCollision(Point p)
        {
            var tile = tilemap.GetTile(p);
            if (tile == null) { return false; }
            return tile.solid;
        }

        private bool GetCollision(int x, int y)
        {
            return GetCollision(new Point(x, y));
        }

        public CollisionData Move(Rectangle aabb, Point delta)
        {
            var collisions = Direction.NONE;

            // FIXME This code is somewhat inefficient and only works for things <= 3 tiles wide/tall
            if (delta.X < 0)
            {
                int left = aabb.Left;
                while (delta.X++ < 0)
                {
                    if (GetCollision(left - 1, aabb.Center.Y) ||
                        GetCollision(left - 1, aabb.Top) ||
                        GetCollision(left - 1, aabb.Bottom))
                    {
                        collisions |= Direction.LEFT;
                        break;
                    }
                    left--;
                }
                delta.X = left - aabb.Left;
            }
            else if (delta.X > 0)
            {
                int right = aabb.Right;
                while (delta.X-- > 0)
                {
                    if (GetCollision(right + 1, aabb.Center.Y) ||
                        GetCollision(right + 1, aabb.Top) ||
                        GetCollision(right + 1, aabb.Bottom))
                    {
                        collisions |= Direction.RIGHT;
                        break;
                    }
                    right++;
                }
                delta.X = right - aabb.Right;
            }

            if (delta.Y < 0)
            {
                int top = aabb.Top;
                while (delta.Y++ < 0)
                {
                    if (GetCollision(aabb.Center.X, top - 1) ||
                        GetCollision(aabb.Left, top - 1) ||
                        GetCollision(aabb.Right, top - 1))
                    {
                        collisions |= Direction.UP;
                        break;
                    }
                    top--;
                }
                delta.Y = top - aabb.Top;
            }
            else if (delta.Y > 0)
            {
                int bottom = aabb.Bottom;
                while (delta.Y-- > 0)
                {
                    if (GetCollision(aabb.Center.X, bottom + 1) ||
                        GetCollision(aabb.Left, bottom + 1) ||
                        GetCollision(aabb.Right, bottom + 1))
                    {
                        collisions |= Direction.DOWN;
                        break;
                    }
                    bottom++;
                }
                delta.Y = bottom - aabb.Bottom;
            }
            var newPosition = new Rectangle(new Point(aabb.Left + delta.X, aabb.Top + delta.Y), aabb.Size);
            return new CollisionData() { collisions = collisions, newPosition = newPosition };
        }
    }
}
