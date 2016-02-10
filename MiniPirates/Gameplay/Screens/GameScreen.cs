using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.ScreenManagement;
using MiniPirates.Gameplay.Scripts;
using MiniPirates.Gameplay.Objects;
using C3.XNA;
using MiniPirates.Engine.Physics;

namespace MiniPirates.Gameplay.Screens
{
    public class GameScreen : Screen
    {
        #region Assets for GameScreen
        Texture2D shipHull;
        Texture2D boulder;
        Texture2D cannonOutline;
        Texture2D cannonFilled;
        #endregion

        PauseScreen pauseScreen;

        public static GameObject camera;

        CollisionManager collisionManager;

        public GameScreen(ScreenManager screenManager)
            : base(screenManager)
        {

        }

        public override void Initialize()
        {
            collisionManager = new CollisionManager();
            pauseScreen = new PauseScreen(ScreenManager);
            pauseScreen.Initialize();

            base.Initialize();

            GameObject playerObject = new GameObject();
            GameObject boulderObject = new GameObject();
            GameObject boulderObject2 = new GameObject();
            camera = new GameObject();

            // Boulder
            Transform tb = boulderObject.AddNewComponent<Transform>();
            tb.InitializeValues(boulder);
            SpriteRenderer r1 = boulderObject.AddNewComponent<SpriteRenderer>();
            r1.Sprite = boulder;
            tb.Position = new Vector2(250, 250);
            CircleCollider collBoulder = boulderObject.AddNewComponent<CircleCollider>();

            // Boulder 2
            Transform tb2 = boulderObject2.AddNewComponent<Transform>();
            tb2.InitializeValues(boulder);
            SpriteRenderer r2 = boulderObject2.AddNewComponent<SpriteRenderer>();
            r2.Sprite = boulder;
            tb2.Position = new Vector2(350, 250);
            CircleCollider collBoulder2 = boulderObject2.AddNewComponent<CircleCollider>();

            // Player
            Transform t = playerObject.AddNewComponent<Transform>();
            t.InitializeValues(shipHull);
            SpriteRenderer r = playerObject.AddNewComponent<SpriteRenderer>();
            r.Sprite = shipHull;
            PhysicsBody b = playerObject.AddNewComponent<PhysicsBody>();
            Player player = playerObject.AddNewComponent<Player>();
            MultiCircleCollider collPlayer = playerObject.AddNewComponent<MultiCircleCollider>();

            // Camera
            Transform ct = camera.AddNewComponent<Transform>();
            FollowPlayer fp = camera.AddNewComponent<FollowPlayer>();
            fp.InitializeValues(playerObject);

            r.InitializeValues(camera);
            r1.InitializeValues(camera);
            r2.InitializeValues(camera);
            collBoulder.InitializeValues(camera);
            collBoulder2.InitializeValues(camera);
            collPlayer.InitializeValues(camera);

            collisionManager.AddStaticCollider(collBoulder);
            collisionManager.AddStaticCollider(collBoulder2);
            collisionManager.AddDynamicCollider(collPlayer);

            world.AddGameObject(camera);
            world.AddGameObject(playerObject);
            world.AddGameObject(boulderObject);
            world.AddGameObject(boulderObject2);

            playerReference = playerObject;
        }

        public override void LoadContent()
        {
            Cannonball.cannonballSprite = ScreenManager.gameReference.Content.Load<Texture2D>("cannonBall");

            shipHull = ScreenManager.gameReference.Content.Load<Texture2D>("player");
            boulder = ScreenManager.gameReference.Content.Load<Texture2D>("boulder");
            cannonOutline = ScreenManager.gameReference.Content.Load<Texture2D>("cannonOutline");
            cannonFilled = ScreenManager.gameReference.Content.Load<Texture2D>("cannonFilled");

            pauseScreen.LoadContent();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            collisionManager.CheckForCollisions();
            if(Input.KeyPressed(Keys.Escape))
            {
                ScreenManager.PushScreen(pauseScreen);
                Enabled = false;
            }
            base.Update(gameTime);
        }

        static Vector2 outlineLocation = new Vector2(4, 8);
        static Color slightlyTransparent = new Color(255, 255, 255, 180);
        GameObject playerReference;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.Draw(cannonOutline, outlineLocation, Color.White);
            spriteBatch.Draw(cannonFilled, outlineLocation, new Rectangle(0, 0, (int)(cannonFilled.Width * (MathHelper.Min(playerReference.GetComponent<Player>().timeSinceLastShot, 1000f) / 1000f)), cannonFilled.Height), Color.White);
            spriteBatch.End(); 
        }
    }
}
