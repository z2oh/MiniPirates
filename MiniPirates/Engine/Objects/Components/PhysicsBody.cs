using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public class PhysicsBody : Component
    {
        Transform objectTransform;
        Vector2 velocity;
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public float Speed
        {
            get
            {
                return velocity.Length();
            }
        }
        float maxSpeed;

        public override void Initialize()
        {
            objectTransform = gameObject.GetComponent<Transform>();
            Velocity = Vector2.Zero;
            maxSpeed = 400;
            base.Initialize();
        }

        public void InitializeValues()
        {

        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            objectTransform.Move(Velocity * elapsedTime);
            base.Update(gameTime);
        }

        public void Accelerate(float acceleration)
        {
            velocity += objectTransform.Forward * acceleration;
            if(Speed > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }
        }

        public void AddVelocity(Vector2 velocity)
        {
            this.velocity += velocity;
        }

        public void Rotate(float angle)
        {
            objectTransform.Rotate(angle);
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            float x = velocity.X;
            float y = velocity.Y;

            velocity.X = x * cos - y * sin;
            velocity.Y = x * sin + y * cos;
        }
    }
}
