using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.ScreenManagement;
using MiniPirates.Engine.WorldSpace;
using MiniPirates.Gameplay.Objects;
using MiniPirates.Gameplay.Screens;
using MiniPirates.Gameplay.Scripts;
using System;
using System.Collections.Generic;

namespace MiniPirates
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MiniPirates : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager screenManager;

        public static Random rand;

        public static Vector2 centerOfScreen;

        public MiniPirates()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            rand = new Random();
            screenManager = new ScreenManager(this);

            GameScreen main = new GameScreen(screenManager);
            

            Input.Initialize();

            base.Initialize();

            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            //graphics.ToggleFullScreen();
            //graphics.ApplyChanges();

            centerOfScreen = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        static Color ocean = new Color(44, 107, 215);

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ocean);
            screenManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
