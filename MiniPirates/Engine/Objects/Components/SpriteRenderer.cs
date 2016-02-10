using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public class SpriteRenderer : DrawableComponent
    {
        Texture2D sprite;
        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        Transform objectTransform;
        Transform cameraTransform;

        public override void Initialize()
        {
            Transform t = gameObject.GetComponent<Transform>();
            if (null != t)
            {
                objectTransform = t;
            }
            base.Initialize();
        }

        public void InitializeValues(GameObject camera)
        {
            cameraTransform = camera.GetComponent<Transform>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, objectTransform.Position - cameraTransform.Position, null , Color.White, objectTransform.Rotation, objectTransform.Origin, objectTransform.Scale, SpriteEffects.None, objectTransform.Layer);

            base.Draw(spriteBatch);
        }
    }
}
