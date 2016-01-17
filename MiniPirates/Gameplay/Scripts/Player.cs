using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MiniPirates.Engine.Objects.Components;
using MiniPirates.Engine;
using Microsoft.Xna.Framework.Input;
using MiniPirates.Engine.Objects;
using MiniPirates.Gameplay.Objects;
using static MiniPirates.Engine.Utility.Enum;

namespace MiniPirates.Gameplay.Scripts
{
    public class Player : Component
    {
        static int numCannonballs = 10;

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
            // Somewhat realistic physics, I guess
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if(Input.KeyDown(Keys.W))
            {
                body.Accelerate(elapsedTime * 80);
            }
            if(Input.KeyDown(Keys.S))
            {
                if(body.Speed > 0)
                {
                    body.Accelerate(elapsedTime * -40);
                }
            }
            if(Input.KeyDown(Keys.A))
            {
                body.Rotate((float)Math.PI * -.00005f * body.Speed);
            }
            if(Input.KeyDown(Keys.D))
            {
                body.Rotate((float)Math.PI * .00005f * body.Speed);
            }
            if(Input.KeyDown(Keys.Q) && timeSinceLastShot >= 1000f)
            {
                timeSinceLastShot = 0.0f;
                for (int i = 0; i < 8; i++)
                {
                    gameObject.World.AddGameObject(Cannonball.GenerateCannonball(gameObject, RelevantDirection.Left, (float)Math.PI / 8, 20, 80));
                }
            }
            else if(Input.KeyDown(Keys.E) && timeSinceLastShot >= 1000f)
            {
                timeSinceLastShot = 0.0f;
                for (int i = 0; i < 8; i++)
                {
                    gameObject.World.AddGameObject(Cannonball.GenerateCannonball(gameObject, RelevantDirection.Right, (float)Math.PI / 8, 20, 80));
                }
            }
            else
            {
                timeSinceLastShot += gameTime.ElapsedGameTime.Milliseconds;
            }
            
            base.Update(gameTime);
        }

        public float timeSinceLastShot = 1000f;
    }
}
