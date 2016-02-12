using C3.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public class MultiCircleCollider : Collider
    {
        Vector2 prevObjectPosition;
        float prevObjectRotation;

        List<Tuple<Vector2, float>> circles;
        public List<Tuple<Vector2, float>> Circles
        {
            get
            {
                return circles;
            }

            set
            {
                circles = value;
            }
        }

        int numCircles;

        public override void Initialize()
        {
            base.Initialize();
            prevObjectPosition = objectTransform.Position;
            prevObjectRotation = objectTransform.Rotation;
            Circles = new List<Tuple<Vector2, float>>();
            numCircles = 5;

            float width = objectTransform.Rectangle.Z;
            float height = objectTransform.Rectangle.W;
            if(height > width)
            {
                float radius = objectTransform.Rectangle.Z / 2f - 8;
                float heightIncrese = (objectTransform.Rectangle.W - 2*radius) / numCircles;
                Vector2 topMiddle = new Vector2(objectTransform.Rectangle.X + objectTransform.Rectangle.Z / 2f, objectTransform.Rectangle.Y);
                for (int i = 0; i < numCircles; i++)
                {
                    Vector2 center = topMiddle;
                    center.Y += radius + (i * heightIncrese);
                    Circles.Add(new Tuple<Vector2, float>(center, radius));
                }
            }
            else
            {
                float radius = (objectTransform.Rectangle.W / numCircles) / 2f;
                Vector2 leftMiddle = new Vector2(objectTransform.Rectangle.Z, objectTransform.Rectangle.Y + objectTransform.Rectangle.Z / 2f);
                for (int i = 0; i < numCircles; i++)
                {
                    Vector2 center = leftMiddle;
                    center.X += i * radius * 2f;
                    Circles.Add(new Tuple<Vector2, float>(center, radius));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 deltaP = objectTransform.Position - prevObjectPosition;
            for (int i = 0; i < Circles.Count; i++)
            {
                Circles[i] = new Tuple<Vector2, float>(Circles[i].Item1 + deltaP, Circles[i].Item2);
            }
            prevObjectPosition = new Vector2(objectTransform.Position.X, objectTransform.Position.Y);

            float deltaR = objectTransform.Rotation - prevObjectRotation;
            if (deltaR != 0)
            {
                deltaR -= (float)Math.PI;
                for (int i = 0; i < numCircles; i++)
                {
                    Vector2 centerOffset = objectTransform.Position - Circles[i].Item1;
                    centerOffset = Math2.RotateVector(centerOffset, deltaR);
                    Circles[i] = new Tuple<Vector2, float>(objectTransform.Position + centerOffset, Circles[i].Item2);
                }
                prevObjectRotation = objectTransform.Rotation;
            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (MiniPirates.drawColliders)
            {
                foreach (Tuple<Vector2, float> circle in Circles)
                {
                    if(!isColliding)
                        spriteBatch.DrawCircle(circle.Item1 - cameraTransform.Position, circle.Item2, 50, Color.Orange);
                    else
                        spriteBatch.DrawCircle(circle.Item1 - cameraTransform.Position, circle.Item2, 50, Color.Red);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
