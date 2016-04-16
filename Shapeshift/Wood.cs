using Microsoft.Xna.Framework;

namespace Shapeshift
{
    class Wood : GameObject
    {
        Sprite sprite = new Sprite("Wood", 32, 32);
        Point delta;
        int acceleration = 1;
        int maxXSpeed = 4;
        int maxYSpeed = 4;
        CollisionData lastCollisions;

        public Wood(Point pos) : base()
        {
            this.aabb = new Rectangle(pos.X, pos.Y, 32, 32);
        }

        public override void LoadContent()
        {
            sprite.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw()
        {
            sprite.Draw(new Vector2(aabb.Left, aabb.Top));
        }
    }
}
