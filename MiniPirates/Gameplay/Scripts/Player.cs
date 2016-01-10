using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine.Objects;

namespace MiniPirates.Gameplay.Scripts
{
    public class Player : Component
    {
        Transform objectTransform;
        PhysicsBody body;

        public override void Initialize()
        {
            Transform t = this.gameObject.GetComponent<Transform>();
            if (null != t)
            {
                objectTransform = t;
            }
            PhysicsBody b = gameObject.GetComponent<PhysicsBody>();
            if (null != b)
            {
                body = b;
            }
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if(Input.KeyDown(Keys.W))
            {
                body.Accelerate(elapsedTime * 40);
            }
            if(Input.KeyDown(Keys.A))
            {
                body.Rotate((float)Math.PI * .00005f * body.Speed);
            }
            if(Input.KeyDown(Keys.D))
            {
                body.Rotate((float)Math.PI * -.00005f * body.Speed);
            }
            if(Input.KeyDown(Keys.Space) && timeSinceLastShot >= 1000f)
            {
                timeSinceLastShot = 0.0f;
                Shoot(gameTime);
            }
            else
            {
                timeSinceLastShot += gameTime.ElapsedGameTime.Milliseconds;
            }
            if(Input.KeyDown(Keys.Q))
            {

            }
            base.Update(gameTime);
        }

        float timeSinceLastShot = 1.0f;

        public void Shoot(GameTime gameTime)
        {
            Vector3 z = Vector3.UnitZ;
            Vector3 forward = new Vector3(objectTransform.Forward, 0);
            Vector3 result = Vector3.Cross(forward, z);
            Vector2 resultantDirection = new Vector2(result.X, result.Y);
            resultantDirection.Normalize();

            GameObject cannonBall = new GameObject();
            Transform t = cannonBall.AddNewComponent<Transform>();
            
            SpriteRenderer r = cannonBall.AddNewComponent<SpriteRenderer>();
            r.Sprite = Game1.cannonBall;
            t.InitializeValues(Game1.cannonBall);
            t.Position = objectTransform.Position;
            t.Forward = resultantDirection;
            PhysicsBody b = cannonBall.AddNewComponent<PhysicsBody>();
            b.Accelerate(2000);
            b.AddVelocity(body.Velocity);
            r.InitializeValues(Game1.camera);
            Game1.cannonballs.Add(cannonBall);
        }
    }
}
