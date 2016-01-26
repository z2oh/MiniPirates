using MiniPirates.Engine.ScreenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.GUI;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine;

namespace MiniPirates.Gameplay.Screens
{
    public class MainMenuScreen : Screen
    {
        Vector2 centerOfButton = new Vector2(200, 100);
        GUIObject playButtonObject;

        SpriteFont pirateFont72;

        public MainMenuScreen(ScreenManager screenManager)
            : base (screenManager)
        {

        }

        public override void LoadContent()
        {
            pirateFont72 = ScreenManager.gameReference.Content.Load<SpriteFont>("Fonts/PirateFont72");
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            playButtonObject = new GUIObject();

            Transform t = playButtonObject.GetComponent<Transform>();

            StringRenderer s = playButtonObject.AddNewComponent<StringRenderer>();
            s.Text = "Play!";
            s.SpriteFont = pirateFont72;

            t.InitializeValues(s);
            t.Position = centerOfButton;

            world.AddGameObject(playButtonObject);
        }

        public override void Update(GameTime gameTime)
        {
            Vector4 rect = playButtonObject.GetComponent<Transform>().Rectangle;
            Vector2 mouse = Input.GetMousePosition();
            if(Input.WasLMBPressed() && mouse.X > rect.X && mouse.Y > rect.Y && mouse.X < rect.X + rect.Z && mouse.Y < rect.Y + rect.W)
            {
                GameScreen screen = new GameScreen(ScreenManager);
                ScreenManager.PushScreen(screen);
                this.Enabled = false;
                this.Visible = false;
            }
            base.Update(gameTime);
        }
    }
}
