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
        internal MiniPirates gameReference;

        public ScreenManager(MiniPirates gameReference)
        {
            screens = new Stack<Screen>();
            this.gameReference = gameReference;
        }

        public void Update(GameTime gameTime)
        {
            screens.Peek().Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Screen screen in screens)
            {
                if (screen.Visible)
                    screen.Draw(spriteBatch);
            }
        }

        public void PushScreen(Screen screen)
        {
            if (screens.Count > 0)
            {
                screens.Peek().Enabled = false;
                screens.Peek().Visible = false;
            }
            screen.Visible = true;
            screen.Enabled = true;
            screens.Push(screen);
        }
    }
}
