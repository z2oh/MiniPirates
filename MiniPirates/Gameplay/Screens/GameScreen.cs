using MiniPirates.Engine.ScreenManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.WorldSpace;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Gameplay.Scripts;
using MiniPirates.Gameplay.Objects;

namespace MiniPirates.Gameplay.Screens
{
    public class GameScreen : Screen
    {
        World world;

        Texture2D shipHull;
        Texture2D boulder;
        Texture2D cannonOutline;
        Texture2D cannonFilled;

        public static GameObject camera;

        public GameScreen(ScreenManager screenManager)
            : base(screenManager)
        {

        }

        public override void Initialize()
        {
            world = new World();

            GameObject playerObject = new GameObject();
            GameObject boulderObject = new GameObject();
            camera = new GameObject();

            Transform tb = boulderObject.AddNewComponent<Transform>();
            tb.InitializeValues(boulder);
            SpriteRenderer r1 = boulderObject.AddNewComponent<SpriteRenderer>();
            r1.Sprite = boulder;
            tb.Position = new Vector2(250, 250);

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

            world.AddGameObject(camera);
            world.AddGameObject(playerObject);
            world.AddGameObject(boulderObject);

            playerReference = playerObject;

            base.Initialize();
            //centerOfScreen = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }

        public override void LoadContent()
        {
            Cannonball.cannonballSprite = ScreenManager.gameReference.Content.Load<Texture2D>("cannonBall");

            shipHull = ScreenManager.gameReference.Content.Load<Texture2D>("player");
            boulder = ScreenManager.gameReference.Content.Load<Texture2D>("boulder");
            cannonOutline = ScreenManager.gameReference.Content.Load<Texture2D>("cannonOutline");
            cannonFilled = ScreenManager.gameReference.Content.Load<Texture2D>("cannonFilled");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            world.Update(gameTime);
            base.Update(gameTime);
        }

        static Vector2 outlineLocation = new Vector2(4, 8);
        static Color slightlyTransparent = new Color(255, 255, 255, 180);
        GameObject playerReference;

        public override void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.Draw(cannonOutline, outlineLocation, Color.White);
            spriteBatch.Draw(cannonFilled, outlineLocation, new Rectangle(0, 0, (int)(cannonFilled.Width * (MathHelper.Min(playerReference.GetComponent<Player>().timeSinceLastShot, 1000f) / 1000f)), cannonFilled.Height), Color.White);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
