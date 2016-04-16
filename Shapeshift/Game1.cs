using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shapeshift
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Tilemap tilemap;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            tilemap = new Tilemap();
            Physics.Instance.tilemap = tilemap;
            TargetElapsedTime = System.TimeSpan.FromSeconds(1f / 60f);
        }

        protected override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Renderer.Instance.SpriteBatch = spriteBatch;
            Renderer.Instance.Graphics = GraphicsDevice;
            Renderer.Instance.Content = Content;

            var player = new Player();
            player.Manage();
            var v = new Villager(new Point(100, 100));
            v.Manage();
            tilemap.LoadContent();

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Instance.Update();

            base.Update(gameTime);
            GameObjectManager.Instance.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            tilemap.Draw();

            GameObjectManager.Instance.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
