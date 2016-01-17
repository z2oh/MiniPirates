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
        Vector2 centerOfButton = new Vector2(400, 200);
        Texture2D playButton;
        GUIObject playButtonObject;

        public MainMenuScreen(ScreenManager screenManager)
            : base (screenManager)
        {

        }

        public override void LoadContent()
        {
            playButton = ScreenManager.gameReference.Content.Load<Texture2D>("playButton");
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
            playButtonObject = new GUIObject();
            Transform t = playButtonObject.GetComponent<Transform>();
            t.InitializeValues(playButton);
            t.Position = centerOfButton;
            GUIRenderer g = playButtonObject.GetComponent<GUIRenderer>();
            g.Sprite = playButton;
            world.AddGameObject(playButtonObject);
        }

        public override void Update(GameTime gameTime)
        {
            Vector4 rect = playButtonObject.GetComponent<Transform>().Rectangle;
            Vector2 mouse = Input.GetMousePosition();
            if(mouse.X > rect.X && mouse.Y > rect.Y && mouse.X < rect.X + rect.Z && mouse.Y < rect.Y + rect.W)
            {
                playButtonObject.GetComponent<Transform>().Scale = new Vector2(1.3f, 1.3f);
                if(Input.WasLMBPressed())
                {
                    GameScreen screen = new GameScreen(ScreenManager);
                    this.Enabled = false;
                    this.Visible = false;
                }
            }
            else
            {
                playButtonObject.GetComponent<Transform>().Scale = Vector2.One;
            }
            base.Update(gameTime);
        }
    }
}
