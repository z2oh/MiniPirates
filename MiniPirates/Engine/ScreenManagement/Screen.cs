﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.ScreenManagement
{
    public abstract class Screen
    {
        ScreenManager screenManager;
        public ScreenManager ScreenManager
        {
            get
            {
                return screenManager;
            }

            set
            {
                screenManager = value;
            }
        }

        bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                enabled = value;
            }
        }

        bool visible;
        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }



        public Screen(ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            screenManager.PushScreen(this);
            LoadContent();
            Initialize();
        }

        public virtual void LoadContent()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
