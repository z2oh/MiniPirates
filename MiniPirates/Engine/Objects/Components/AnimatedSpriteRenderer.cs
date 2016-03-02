using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public class AnimatedSpriteRenderer : DrawableComponent
    {
        Texture2D spriteSheet;
        public Texture2D SpriteSheet
        {
            get
            {
                return spriteSheet;
            }

            set
            {
                spriteSheet = value;
            }
        }

        Rectangle spriteSize;
        public Rectangle SpriteSize
        {
            get
            {
                return spriteSize;
            }

            set
            {
                spriteSize = value;
                activeFrame = value;
                centerOfSprite = new Vector2(value.Width / 2, value.Height / 2);
            }
        }

        Rectangle activeFrame;
        Vector2 centerOfSprite;

        int timePerFrame;
        public int TimePerFrame
        {
            get
            {
                return timePerFrame;
            }

            set
            {
                timePerFrame = value;
            }
        }

        int numFrames;
        public int NumFrames
        {
            get
            {
                return numFrames;
            }

            set
            {
                numFrames = value;
            }
        }

        int currentFrame;
        int elapsedTimeOnCurrentFrame;

        Transform objectTransform;
        Transform cameraTransform;

        public override void Initialize()
        {
            currentFrame = 0;
            elapsedTimeOnCurrentFrame = 0;
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

        public override void Update(GameTime gameTime)
        {
            elapsedTimeOnCurrentFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(elapsedTimeOnCurrentFrame >= timePerFrame)
            {
                elapsedTimeOnCurrentFrame = 0;
                currentFrame = (currentFrame + 1) % NumFrames;
                activeFrame = new Rectangle(spriteSize.X + spriteSize.Width * currentFrame, spriteSize.Y, spriteSize.Width, spriteSize.Height);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, objectTransform.Position - cameraTransform.Position, activeFrame, Color.White, objectTransform.Rotation, centerOfSprite, objectTransform.Scale, SpriteEffects.None, objectTransform.Layer);

            base.Draw(spriteBatch);
        }
    }
}
