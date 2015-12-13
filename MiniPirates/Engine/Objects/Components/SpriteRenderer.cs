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
        bool isAtlas;

        Transform objectTransform;



        public override void Initialize()
        {
            if (null != this.gameObject.GetComponent<Transform>())
            {
                objectTransform = this.gameObject.GetComponent<Transform>();
            }
            base.Initialize();
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(sprite,  objectTransform.TopLeft, null , Color.White, objectTransform.Rotation, objectTransform.Origin, objectTransform.Scale, SpriteEffects.None, objectTransform.Layer);
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
