using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public class Transform : Component
    {
        Vector2 forward;
        public Vector2 Forward
        {
            get
            {
                return forward;
            }

            set
            {
                forward = value;
            }
        }

        Vector2 right;
        public Vector2 Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }

        Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        Vector4 rectangle;
        public Vector4 Rectangle
        {
            get
            {
                return rectangle;
            }

            set
            {
                rectangle = value;
            }
        }

        Vector2 origin;
        public Vector2 Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
            }
        }

        Vector2 scale;
        public Vector2 Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }

        float layer;
        public float Layer
        {
            get
            {
                return layer;
            }

            set
            {
                layer = value;
            }
        }



        public Transform()
        {

        }

        public void InitializeValues(Texture2D sprite)
        {
            position = Vector2.Zero;
            rotation = 0f;
            rectangle = new Vector4(position.X - (sprite.Width / 2f), position.Y - (sprite.Height / 2f), sprite.Width, sprite.Height);
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            scale = Vector2.One;
            forward = new Vector2(0, 1);
            right = new Vector2(-1, 0);
        }

        public void Move(Vector2 vector)
        {
            position += vector;
        }

        public void MoveForward(float distance)
        {
            position += (forward * distance);
        }

        public void Rotate(float angle)
        {
            rotation += angle;
            rotation %= 2f * (float)Math.PI;

            forward = Math2.RotateVector(forward, angle);
            forward.Normalize();

            var temp = Vector3.Cross(Vector3.UnitZ, new Vector3(forward, 0));
            right.X = temp.X;
            right.Y = temp.Y;
            right.Normalize();
        }

        public void SetRotation(float angle)
        {
            rotation = 0;
            Rotate(angle);
        }
    }
}
