using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.WorldSpace
{
    public class World
    {
        List<GameObject> gameObjects;
        List<GameObject> deadObjects;

        public CollisionManager collisionManager;

        GameObject camera;
        public GameObject Camera
        {
            get
            {
                return camera;
            }
        }

        public World()
        {
            gameObjects = new List<GameObject>();
            deadObjects = new List<GameObject>();
            collisionManager = new CollisionManager();
            camera = new GameObject();
            camera.AddNewComponent<Transform>();
            AddGameObject(camera);
        }

        public void Update(GameTime gameTime)
        {
            for(int i = 0; i < gameObjects.Count; i++)
            {
                GameObject go = gameObjects[i];
                if (!go.IsDead)
                    go.Update(gameTime);
                else
                    deadObjects.Add(go);
            }
            collisionManager.CheckForCollisions();
            foreach (GameObject go in deadObjects)
            {
                gameObjects.Remove(go);
            }
            deadObjects.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach(GameObject go in gameObjects)
            {
                if (!go.IsDead)
                    go.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObject.World = this;
            gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObject.IsDead = true;
            deadObjects.Add(gameObject);
        }
    }
}
