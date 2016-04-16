using Microsoft.Xna.Framework;

namespace Shapeshift
{
    class Player : GameObject
    {
        Sprite sprite = new Sprite("Jimbo", 32, 64);
        Point delta;
        int acceleration = 1;
        int maxXSpeed = 4;
        int maxYSpeed = 4;
        CollisionData lastCollisions;

        public Player() : base()
        {
            this.aabb = new Rectangle(0, 0, 32, 32);
            CreateUserInterface();
        }

        void CreateUserInterface()
        {
            UserInterface.Instance.AddButton(new Button(new Rectangle(32, 8, 128, 32), "Wall"));
        }

        public override void LoadContent()
        {
            sprite.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Basic movement
            if (Input.Instance.KeyDown(Keymapping.Right))
            {
                sprite.SetAnimation("WalkingRight");
                delta.X += acceleration;
            }
            else if (Input.Instance.KeyDown(Keymapping.Left))
            {
                sprite.SetAnimation("WalkingLeft");
                delta.X -= acceleration;
            }
            else if (delta.X > 0)
            {
                delta.X--;
            }
            else if (delta.X < 0)
            {
                delta.X++;
            }
            if (Input.Instance.KeyDown(Keymapping.Up))
            {
                sprite.SetAnimation("WalkingUp");
                delta.Y -= acceleration;
            }
            else if (Input.Instance.KeyDown(Keymapping.Down))
            {
                sprite.SetAnimation("WalkingDown");
                delta.Y += acceleration;
            }
            else if (delta.Y > 0)
            {
                delta.Y--;
            }
            else if (delta.Y < 0)
            {
                delta.Y++;
            }

            // Building
            if (Input.Instance.MouseDown())
                BuildObject(Input.Instance.MousePosition, 1);

            // Limit speed
            delta.X = MathHelper.Clamp(delta.X, -maxXSpeed, maxXSpeed);
            delta.Y = MathHelper.Clamp(delta.Y, -maxYSpeed, maxYSpeed);

            // Physics
            var cd = Physics.Instance.Move(this, delta);
            aabb = cd.newPosition;
            if ((Direction.LEFT & cd.collisions) == Direction.LEFT)
                delta.X = MathHelper.Max(0, delta.X);
            if ((Direction.RIGHT & cd.collisions) == Direction.RIGHT)
                delta.X = MathHelper.Min(0, delta.X);
            if ((Direction.UP & cd.collisions) == Direction.UP)
                delta.Y = MathHelper.Max(0, delta.Y);
            if ((Direction.DOWN & cd.collisions) == Direction.DOWN)
                delta.Y = MathHelper.Min(0, delta.Y);
            lastCollisions = cd;
        }

        public void BuildObject(Point screenPos, int index)
        {
            Tile t = new Tile(index, true);
            Physics.Instance.tilemap.SetTile(Physics.Instance.tilemap.GetTilePoint(screenPos), t);
        }

        public override void Draw()
        {
            sprite.Draw(new Vector2(aabb.Left, aabb.Top - 32));
        }
    }
}
