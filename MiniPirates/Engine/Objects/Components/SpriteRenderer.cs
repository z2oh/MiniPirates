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

        public new void Initialize()
        {
            objectTransform = 
            
            base.Initialize();
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            spriteBatch.End();

            base.Draw(spriteBatch);
        }
    }
}
