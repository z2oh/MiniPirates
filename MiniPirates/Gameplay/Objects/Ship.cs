using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Gameplay.Objects
{
    public class Ship : GameObject
    {
        Transform transform;
        SpriteRenderer spriteRenderer;
        PhysicsBody body;

        public Ship()
            : base()
        {
            transform = AddNewComponent<Transform>();
            spriteRenderer = AddNewComponent<SpriteRenderer>();
            body = AddNewComponent<PhysicsBody>();
        }
    }
}
