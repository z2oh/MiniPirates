using Microsoft.Xna.Framework.Graphics;
using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MiniPirates.Gameplay.Objects
{
    public class Ship : GameObject
    {
        #region Ship textures
        public static Texture2D basicShip;
        #endregion

        public int health;

        Transform transform;
        SpriteRenderer spriteRenderer;
        PhysicsBody body;
        MultiCircleCollider collider;

        public Ship()
            : base()
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            transform = AddNewComponent<Transform>();
            spriteRenderer = AddNewComponent<SpriteRenderer>();
            body = AddNewComponent<PhysicsBody>();
            collider = AddNewComponent<MultiCircleCollider>();
            health = 100;
        }

        public void InitializeValues()
        {
            spriteRenderer.InitializeValues(World.Camera);
            collider.InitializeValues(World.Camera);
            World.collisionManager.AddDynamicCollider(collider);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(health <= 0)
            {
                Destroy();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void SetSprite(Texture2D sprite)
        {
            spriteRenderer.Sprite = sprite;
            transform.InitializeValues(sprite);
            collider.Initialize();
        }
    }
}
