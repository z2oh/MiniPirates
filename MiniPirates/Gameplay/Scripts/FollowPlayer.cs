using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MiniPirates.Gameplay.Scripts
{
    public class FollowPlayer : Component
    {
        //GameObject player;
        Transform playerTransform;
        Transform cameraTransform;

        public void InitializeValues(GameObject player)
        {
            //this.player = player;
            this.playerTransform = player.GetComponent<Transform>();
            this.cameraTransform = gameObject.GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            cameraTransform.Position = playerTransform.Position;
            cameraTransform.Position -= Game1.centerOfScreen;
            base.Update(gameTime);
        }
    }
}
