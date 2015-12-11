using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects
{
    public class GameObject
    {
        List<Component> components;

        public GameObject()
        {
            Initialize();
        }

        public void Initialize()
        {
            components = new List<Component>();
        }

        public void Update(GameTime gameTime)
        {
            foreach(Component c in components)
            {
                c.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Component c in components)
            {
                if(c.GetType() == typeof(DrawableComponent))
                {
                    (c as DrawableComponent).Draw(spriteBatch);
                }
            }
        }

        public void AddComponent(Component c)
        {
            components.Add(c);
        }

        public T GetComponent<T>()
        {
            foreach(Component c in components)
            {
                if(c.GetType() == T.GetType())
                {
                    return c;
                }
            }
            return default(T);
        }
    }
}
