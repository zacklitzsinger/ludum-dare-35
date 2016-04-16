using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Shapeshift
{
    class Button
    {
        string text;
        Rectangle area;
        Texture2D pixel;
        int border = 1;

        public Button(Rectangle area, string text = "")
        {
            this.area = area;
            this.text = text;
        }

        public void LoadContent()
        {
            pixel = new Texture2D(Renderer.Instance.Graphics, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData<Color>(new[] { Color.LightGray });
        }

        public bool CheckCollision(Point p)
        {
            return area.Contains(p);
        }

        public void Draw()
        {
            var sb = Renderer.Instance.SpriteBatch;
            sb.Draw(pixel, area, Color.Black);
            sb.Draw(pixel, new Rectangle(area.X + border, area.Y + border, area.Width - border, area.Height - border), Color.White);
            Renderer.Instance.Text(text, new Vector2(area.Center.X, area.Center.Y));
        }
    }

    class UserInterface
    {
        List<Button> buttons = new List<Button>();

        //Singleton framework
        private static UserInterface instance;
        public static UserInterface Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserInterface();
                return instance;
            }
        }

        public void LoadContent()
        {
            foreach (var button in buttons)
            {
                button.LoadContent();
            }
        }

        public void AddButton(Button b)
        {
            buttons.Add(b);
        }

        public void Update()
        {
        }

        public void Draw()
        {
            foreach(var button in buttons)
            {
                button.Draw();
            }
        }

    }
}
