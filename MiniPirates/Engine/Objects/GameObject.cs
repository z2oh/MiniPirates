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
                if(typeof(DrawableComponent).IsAssignableFrom(c.GetType()))
                {
                    (c as DrawableComponent).Draw(spriteBatch);      
                }
            }
        }

        public T AddNewComponent<T>() where T : Component, new()
        {
            T a = new T();
            a.gameObject = this;
            a.Initialize();
            components.Add(a);
            return a;
        }

        //public void AddComponent(Component c)
        //{
        //    c.gameObject = this;
        //    components.Add(c);
        //}

        public T GetComponent<T>() where T : Component
        {
            foreach(Component c in components)
            {
                if(c.GetType() == typeof(T))
                {
                    //return (T) Convert.ChangeType(c, typeof(T));
                    return c as T;
                }
            }
            return default(T);
        }
    }
}
