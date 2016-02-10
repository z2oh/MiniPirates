using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Physics
{
    public class Collision
    {
        Collider c1;
        Collider c2;
        GameObject g1;
        GameObject g2;

        public Collision(Collider c1, Collider c2)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.g1 = c1.gameObject;
            this.g2 = c2.gameObject;
        }
    }
}
