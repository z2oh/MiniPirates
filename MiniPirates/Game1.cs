using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.WorldSpace;
using MiniPirates.Gameplay.Objects;
using MiniPirates.Gameplay.Scripts;
using System;
using System.Collections.Generic;

namespace MiniPirates
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random rand;

        World world;

        GameObject playerObject;
        public static GameObject camera;
        GameObject boulderObject;

        Texture2D shipHull;
        Texture2D boulder;
        Texture2D cannonOutline;
        Texture2D cannonFilled;
        public static Texture2D cannonBall;

        public static List<GameObject> cannonballs;

        public static Vector2 centerOfScreen;

        public Game1()
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

            world = new World();

            cannonballs = new List<GameObject>();
            Input.Initialize();

            playerObject = new GameObject();
            camera = new GameObject();
            boulderObject = new GameObject();

            base.Initialize();

            Transform tb = boulderObject.AddNewComponent<Transform>();
            tb.InitializeValues(boulder);
            SpriteRenderer r1 = boulderObject.AddNewComponent<SpriteRenderer>();
            r1.Sprite = boulder;
            tb.Position = new Vector2(250, 250);
            //PhysicsBody b4 = boulderObject.AddNewComponent<PhysicsBody>();
           
            Transform t = playerObject.AddNewComponent<Transform>();
            t.InitializeValues(shipHull);
            SpriteRenderer r = playerObject.AddNewComponent<SpriteRenderer>();
            r.Sprite = shipHull;
            
            PhysicsBody b = playerObject.AddNewComponent<PhysicsBody>();

            Player player = playerObject.AddNewComponent<Player>();

            Transform ct = camera.AddNewComponent<Transform>();
            FollowPlayer fp = camera.AddNewComponent<FollowPlayer>();
            fp.InitializeValues(playerObject);

            r.InitializeValues(camera);
            r1.InitializeValues(camera);

            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            //graphics.ToggleFullScreen();
            //graphics.ApplyChanges();

            world.AddGameObject(camera);
            world.AddGameObject(playerObject);
            world.AddGameObject(boulderObject);

            centerOfScreen = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            shipHull = Content.Load<Texture2D>("player");
            boulder = Content.Load<Texture2D>("boulder");
            Cannonball.cannonballSprite = Content.Load<Texture2D>("cannonBall");
            cannonOutline = Content.Load<Texture2D>("cannonOutline");
            cannonFilled = Content.Load<Texture2D>("cannonFilled");
            // TODO: use this.Content to load your game content here
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
            world.Update(gameTime);
            foreach (GameObject g in cannonballs)
            {
                g.Update(gameTime);
            }
            base.Update(gameTime);
        }

        static Vector2 outlineLocation = new Vector2(4, 8);
        static Color slightlyTransparent = new Color(255, 255, 255, 180);

        static Color ocean = new Color(44, 107, 215);

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ocean);
            world.Draw(spriteBatch);
            foreach(GameObject g in cannonballs)
            {
                g.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(cannonOutline, outlineLocation, Color.White);
            int val = (int)(cannonFilled.Width * MathHelper.Min(playerObject.GetComponent<Player>().timeSinceLastShot, 1.0f));
            spriteBatch.Draw(cannonFilled, outlineLocation, new Rectangle(0, 0, (int)(cannonFilled.Width * (MathHelper.Min(playerObject.GetComponent<Player>().timeSinceLastShot, 1000f) / 1000f)), cannonFilled.Height), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
