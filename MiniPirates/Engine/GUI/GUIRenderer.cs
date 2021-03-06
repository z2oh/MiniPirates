﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.GUI
{
    public class GUIRenderer : DrawableComponent
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

        Transform objectTransform;

        public override void Initialize()
        {
            color = Color.White;
            Transform t = gameObject.GetComponent<Transform>();
            if (null != t)
            {
                objectTransform = t;
            }
            base.Initialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, objectTransform.Position, null, color, objectTransform.Rotation, objectTransform.Origin, objectTransform.Scale, SpriteEffects.None, objectTransform.Layer);

            base.Draw(spriteBatch);
        }
    }
}
