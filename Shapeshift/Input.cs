using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Shapeshift
{
    public enum Keymapping
    {
        Up = Keys.W,
        Left = Keys.A,
        Down = Keys.S,
        Right = Keys.D        
    }

    public class Input
    {
        private static Input instance;
        private Input() { }
        public static Input Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Input();
                }
                return instance;
            }
        }

        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;
        private MouseState previousMouseState;
        private MouseState currentMouseState;

        public void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public bool KeyPressed(Keymapping keymapping)
        {
            var key = (Keys)(keymapping);
            return (previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key));
        }

        public bool KeyDown(Keymapping keymapping)
        {
            var key = (Keys)(keymapping);
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool MouseDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool MouseClicked()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released);
        }

        public Point MousePosition { get { return currentMouseState.Position; } }
    }
}
