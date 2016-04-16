using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Shapeshift
{
    abstract class GameObject
    {
        protected Rectangle aabb;
        public Rectangle AABB
        {
            get { return aabb;}

            set { aabb = value; }
        }

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
            if (gameObjects.Contains(go))
                return;
            gameObjects.Add(go);
            go.LoadContent();
        }

        public bool CheckCollision(Point pos, GameObject source)
        {
            foreach(var go in gameObjects)
            {
                if (go == source)
                    continue;
                if (go.AABB.Contains(pos))
                    return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var go in gameObjects)
                go.Update(gameTime);
        }

        public void Draw()
        {
            gameObjects.Sort((a, b) => { return a.AABB.Bottom.CompareTo(b.AABB.Bottom); });
            foreach (var go in gameObjects)
                go.Draw();
        }
    }
}
