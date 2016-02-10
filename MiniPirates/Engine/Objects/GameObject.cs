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
        
        /// <summary>
        /// Returns the first occurence of a component of Type T.
        /// </summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <returns>The first occurence of the component of type T.</returns>
        public T GetComponent<T>() where T : Component
        {
            foreach(Component c in components)
            {
                if(c.GetType() == typeof(T))
                {
                    return c as T;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Returns a list of all the components of type T.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <returns>A list of all of the components of type T.</returns>
        public List<T> GetAllComponents<T>() where T : Component
        {
            List<T> comp = new List<T>();
            foreach (Component c in components)
            {
                if (c.GetType() == typeof(T))
                {
                    comp.Add(c as T);
                }
            }
            return comp;
        }

        /// <summary>
        /// Returns the i'th component found on the object of Type T.
        /// </summary>
        /// <typeparam name="T">The Type of component to get.</typeparam>
        /// <param name="number">Which component of type T you want to return (first, second, etc.).</param>
        /// <returns>The i'th component found on the object of Type T.</returns>
        public T GetComponent<T>(int number) where T : Component
        {
            int found = 0;
            foreach (Component c in components)
            {
                if (c.GetType() == typeof(T))
                {
                    if(found == number)
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
