using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.ScreenManagement
{
    public class ScreenManager
    {
        Stack<Screen> screens;
        Queue<Screen> newScreens;
        Queue<Screen> deadScreens;

        internal MiniPirates gameReference;

        public ScreenManager(MiniPirates gameReference)
        {
            screens = new Stack<Screen>();
            newScreens = new Queue<Screen>();
            deadScreens = new Queue<Screen>();
            this.gameReference = gameReference;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Screen screen in screens)
            {
                if (screen.Enabled)
                    screen.Update(gameTime);
            }
            while(deadScreens.Count > 0)
            {
                screens.Pop();
                deadScreens.Dequeue();
                screens.Peek().Enabled = true;
                screens.Peek().Visible = true;
            }
            while(newScreens.Count > 0)
            {
                screens.Push(newScreens.Dequeue());
                screens.Peek().Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Screen screen in screens.Reverse())
            {
                if (screen.Visible)
                    screen.Draw(spriteBatch);
            }
        }

        public void PopScreen(Screen screen)
        {
            deadScreens.Enqueue(screen);
        }

        public void PushScreen(Screen screen)
        {
            screen.Visible = true;
            screen.Enabled = true;
            newScreens.Enqueue(screen);
        }
    }
}
