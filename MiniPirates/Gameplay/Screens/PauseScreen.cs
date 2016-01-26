using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine;
using MiniPirates.Engine.GUI;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.ScreenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Gameplay.Screens
{
    public class PauseScreen : Screen
    {
        Texture2D pixel;
        SpriteFont pirateFont72;

        bool aliveOneFrame;

        public PauseScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            
        }

        public override void LoadContent()
        {
            pixel = ScreenManager.gameReference.Content.Load<Texture2D>("pixel");
            pirateFont72 = ScreenManager.gameReference.Content.Load<SpriteFont>("Fonts/PirateFont72");
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
            GUIObject pauseTextObject = new GUIObject();
            Transform t = pauseTextObject.GetComponent<Transform>();
            StringRenderer s = pauseTextObject.AddNewComponent<StringRenderer>();
            s.Text = "Paused";
            s.SpriteFont = pirateFont72;

            t.InitializeValues(s);
            t.Position = MiniPirates.centerOfScreen;
            world.AddGameObject(pauseTextObject);
            aliveOneFrame = false;

            /*
            GUIObject shade = new GUIObject();
            Transform t2 = shade.GetComponent<Transform>();
            t2.InitializeValues(pixel);
            t2.Position = MiniPirates.centerOfScreen;
            t2.Scale = MiniPirates.centerOfScreen * 2;
            GUIRenderer g2 = shade.GetComponent<GUIRenderer>();
            g2.Sprite = pixel;
            g2.Color = new Color(255, 255, 255, 100);
            world.AddGameObject(shade);
            */
        }

        public override void Update(GameTime gameTime)
        {
            if (aliveOneFrame && Input.KeyPressed(Keys.Escape))
            {
                ScreenManager.PopScreen(this);
                Enabled = false;
                aliveOneFrame = false;
            }
            if(Enabled)
                aliveOneFrame = true;
            base.Update(gameTime);
        }
    }
}
