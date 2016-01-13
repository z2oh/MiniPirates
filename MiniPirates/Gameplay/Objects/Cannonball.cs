using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MiniPirates.Engine.Enum;
using Microsoft.Xna.Framework;

namespace MiniPirates.Gameplay.Objects
{
    public class Cannonball : GameObject
    {
        public static Texture2D cannonballSprite;

        Transform transform;
        SpriteRenderer spriteRenderer;
        PhysicsBody physicsBody;

        float distanceToTravel = 1000f;

        public Cannonball()
        {
            transform = AddNewComponent<Transform>();
            transform.InitializeValues(cannonballSprite);
            spriteRenderer = AddNewComponent<SpriteRenderer>();
            spriteRenderer.Sprite = cannonballSprite;
            spriteRenderer.InitializeValues(Game1.camera);
            physicsBody = AddNewComponent<PhysicsBody>();
        }

        public override void Update(GameTime gameTime)
        {
            distanceToTravel -= physicsBody.Speed * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            if(distanceToTravel <= 0)
            {
                this.Destroy();
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRenderer.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public static Cannonball GenerateCannonball(GameObject ship, RelevantDirection direction)
        {
            Transform shipTransform = ship.GetComponent<Transform>();
            Cannonball cannonball = new Cannonball();
            cannonball.transform.Position = shipTransform.Position;
            if(direction == RelevantDirection.Right)
            {
                cannonball.transform.Forward = shipTransform.Right;
            }
            else
            {
                cannonball.transform.Forward = -shipTransform.Right;
            }
            cannonball.physicsBody.Accelerate(2000);
            cannonball.physicsBody.AddVelocity(ship.GetComponent<PhysicsBody>().Velocity);
            return cannonball;
        }
    }
}
