using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine.ScreenManagement;
using MiniPirates.Gameplay.Scripts;
using MiniPirates.Gameplay.Objects;
using C3.MonoGame;
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

        public GameScreen(ScreenManager screenManager)
            : base(screenManager)
        {

        }

        public override void Initialize()
        {
            pauseScreen = new PauseScreen(ScreenManager);
            pauseScreen.Initialize();

            base.Initialize();

            Ship.basicShip = shipHull;

            FollowPlayer fp = world.Camera.AddNewComponent<FollowPlayer>();

            GameObject enemyObject = new GameObject();
            GameObject boulderObject = new GameObject();
            GameObject boulderObject2 = new GameObject();

            Ship playerShip = new Ship();
            playerShip.SetSprite(shipHull);
            world.AddGameObject(playerShip);
            playerShip.AddNewComponent<Player>();
            playerShip.InitializeValues();

            fp.InitializeValues(playerShip);

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

            // Enemy
            Transform t2 = enemyObject.AddNewComponent<Transform>();
            t2.InitializeValues(shipHull);
            t2.Position = new Vector2(300, -100);
            SpriteRenderer rp2 = enemyObject.AddNewComponent<SpriteRenderer>();
            rp2.Sprite = shipHull;
            PhysicsBody b2 = enemyObject.AddNewComponent<PhysicsBody>();
            MultiCircleCollider collEnemy = enemyObject.AddNewComponent<MultiCircleCollider>();

            r1.InitializeValues(world.Camera);
            r2.InitializeValues(world.Camera);
            rp2.InitializeValues(world.Camera);
            collBoulder.InitializeValues(world.Camera);
            collBoulder2.InitializeValues(world.Camera);
            collEnemy.InitializeValues(world.Camera);

            world.collisionManager.AddStaticCollider(collBoulder);
            world.collisionManager.AddStaticCollider(collBoulder2);
            world.collisionManager.AddDynamicCollider(collEnemy);

            world.AddGameObject(enemyObject);
            world.AddGameObject(boulderObject);
            world.AddGameObject(boulderObject2);

            playerReference = playerShip;
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
