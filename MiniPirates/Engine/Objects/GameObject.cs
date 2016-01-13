using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.WorldSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects
{
    public class GameObject
    {
        List<Component> components;
        World world;

        public bool IsDead = false;

        public World World
        {
            get
            {
                return world;
            }

            set
            {
                world = value;
            }
        }

        public GameObject()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            components = new List<Component>();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach(Component c in components)
            {
                c.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
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

        public T GetComponent<T>(int number) where T : Component
        {
            int found = 0;
            foreach (Component c in components)
            {
                if (c.GetType() == typeof(T))
                {
                    if(found == number)
                        //return (T) Convert.ChangeType(c, typeof(T));
                        return c as T;
                    found++;
                }
            }
            return default(T);
        }

        public void Destroy()
        {
            world.RemoveGameObject(this);
        }
    }
}
