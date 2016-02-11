using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniPirates.Engine.Utility;
using Microsoft.Xna.Framework;
using static MiniPirates.Engine.Utility.Enum;
using MiniPirates.Gameplay.Screens;
using MiniPirates.Engine.WorldSpace;

namespace MiniPirates.Gameplay.Objects
{
    public class Cannonball : GameObject
    {
        public static Texture2D cannonballSprite;

        Transform transform;
        SpriteRenderer spriteRenderer;
        PhysicsBody physicsBody;
        CircleCollider coll;

        float distanceToTravel = 1000f;

        public Cannonball(World world)
            :base()
        {
            transform = AddNewComponent<Transform>();
            transform.InitializeValues(cannonballSprite);
            spriteRenderer = AddNewComponent<SpriteRenderer>();
            spriteRenderer.Sprite = cannonballSprite;
            spriteRenderer.InitializeValues(GameScreen.camera);
            physicsBody = AddNewComponent<PhysicsBody>();
            coll = AddNewComponent<CircleCollider>();
            coll.InitializeValues(GameScreen.camera);
            world.collisionManager.AddDynamicCollider(coll);
        }

        public override void Update(GameTime gameTime)
        {
            distanceToTravel -= physicsBody.Speed * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
            if(distanceToTravel <= 0)
            {
                World.collisionManager.RemoveDynamicCollider(coll); 
                this.Destroy();
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRenderer.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        //minkowski differences

        /// <summary>
        /// This function will generate a cannonball.
        /// </summary>
        /// <param name="ship">The ship that the cannonball is being fired from. This is used to get the direction of the cannonball, and to add the ship's relative velocity.</param>
        /// <param name="direction">This is the side of the ship that the cannonball is being fired from, either Left or Right, which are part of an enum defined in Enum.cs.</param>
        /// <param name="angleJitter">This is how many radians of arc to be used in determining the cannonball's direction. A random number in this range is added to the cannonball's direction. If Pi/4 is used, it will add less than Pi/8 going in either direction.</param>
        /// <param name="sideDisplacement">This is the distance from the center of the ship to the edge. This is added to the cannonball's position so the ship shoots from its side rather than its center.</param>
        /// <param name="shipLength">This is the length of the ship. This is used to calculate an offset so all the cannonballs do not come from the same place.</param>
        /// <returns></returns>
        public static Cannonball GenerateCannonball(GameObject ship, RelevantDirection direction, float angleJitter, float sideDisplacement, float shipLength)
        {
            // We get the ship's transform.
            Transform shipTransform = ship.GetComponent<Transform>();
            Cannonball cannonball = new Cannonball(ship.World);

            cannonball.transform.Position = shipTransform.Position;
            if (direction == RelevantDirection.Right)
            {
                cannonball.transform.Forward = shipTransform.Right;
                cannonball.transform.Position += shipTransform.Right * sideDisplacement;
            }
            else
            {
                cannonball.transform.Forward = -shipTransform.Right;
                cannonball.transform.Position += -shipTransform.Right * sideDisplacement;
            }

            

            float lengthDisplacement = (float)(MiniPirates.rand.NextDouble() * shipLength) - (shipLength * .5f);
            cannonball.transform.Position += shipTransform.Forward * lengthDisplacement;

            float jitter = (float)(MiniPirates.rand.NextDouble() * angleJitter) - (angleJitter * .5f);
            cannonball.transform.Forward = Math2.RotateVector(cannonball.transform.Forward, jitter);
            cannonball.physicsBody.Accelerate(2000);
            cannonball.physicsBody.AddVelocity(ship.GetComponent<PhysicsBody>().Velocity);
            return cannonball;
        }
    }
}
