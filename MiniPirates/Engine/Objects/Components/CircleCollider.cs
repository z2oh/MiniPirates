using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;

namespace MiniPirates.Engine.Objects.Components
{
    public class CircleCollider : Collider
    {
        Vector2 center;
        public Vector2 Center
        {
            get
            {
                return center;
            }

            set
            {
                center = value;
            }
        }

        float radius;
        public float Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            Center = objectTransform.Position;
            Radius = objectTransform.Rectangle.Z / 2f - 8;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Center = objectTransform.Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(MiniPirates.drawColliders)
            {
                if(!isColliding)
                    spriteBatch.DrawCircle(Center - cameraTransform.Position, Radius, 50, Color.Pink);
                else
                    spriteBatch.DrawCircle(Center - cameraTransform.Position, Radius, 50, Color.Red);
            }
            base.Draw(spriteBatch);
        }
    }
}
