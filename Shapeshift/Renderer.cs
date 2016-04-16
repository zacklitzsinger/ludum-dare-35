using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shapeshift
{
    public class Renderer
    {
        const float MAX_LAYER = 100;

        //Singleton framework
        private static Renderer instance;
        public static Renderer Instance
        {
            get
            {
                if (instance == null)
                    instance = new Renderer();
                return instance;
            }
        }

        public ContentManager Content;
        public GraphicsDevice Graphics;
        public SpriteBatch SpriteBatch;
        public SpriteFont Font;

        public T Load<T>(string path)
        {
            return Content.Load<T>(path);
        }

        #region Draw methods
        public void Text(string s, Vector2 position)
        {
            SpriteBatch.DrawString(Font, s, position, Color.White);
        }

        public void Draw(Texture2D image, Vector2 position, Rectangle? sourceRect, Color c)
        {
            SpriteBatch.Draw(image, position, sourceRect, c);
        }

        public void Draw(Texture2D image, Vector2 position)
        {
            Draw(image, position, null, Color.White);
        }
        #endregion
    }
}
