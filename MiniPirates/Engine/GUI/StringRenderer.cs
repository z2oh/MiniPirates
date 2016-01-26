using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.GUI
{
    public class StringRenderer : DrawableComponent
    {
        string text;
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        Color color;
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        SpriteFont spriteFont;
        public SpriteFont SpriteFont
        {
            get
            {
                return spriteFont;
            }

            set
            {
                spriteFont = value;
            }
        }

        Transform objectTransform;

        public override void Initialize()
        {
            color = Color.Black;
            Transform t = gameObject.GetComponent<Transform>();
            if (null != t)
            {
                objectTransform = t;
            }
            base.Initialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, text, objectTransform.Position - objectTransform.Origin, color);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
