using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public class Transform : Component
    {
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

        Vector2 topLeft;
        public Vector2 TopLeft
        {
            get
            {
                return topLeft;
            }

            set
            {
                topLeft = value;
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

        public Transform(Texture2D sprite)
        {
            position = Vector2.Zero;
            rotation = 0f;
            rectangle = new Vector4(position.X - (sprite.Width / 2f), position.Y - (sprite.Height / 2f), sprite.Width, sprite.Height);
            topLeft = new Vector2(rectangle.X, rectangle.Y);
        }
    }
}
