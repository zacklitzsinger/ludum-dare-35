using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Shapeshift
{
    abstract class GameObject
    {
        public GameObject()
        {
        }

        public void Manage()
        {
            GameObjectManager.Instance.Add(this);
        }

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw();
    }

    class GameObjectManager
    {
        List<GameObject> gameObjects = new List<GameObject>();

        private static GameObjectManager instance;
        private GameObjectManager() { }
        public static GameObjectManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObjectManager();
                }
                return instance;
            }
        }

        public void Add(GameObject go)
        {
            gameObjects.Add(go);
            go.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var go in gameObjects)
                go.Update(gameTime);
        }


        public void Draw()
        {
            foreach (var go in gameObjects)
                go.Draw();
        }
    }
}
