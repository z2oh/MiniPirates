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
        public Collider C1
        {
            get
            {
                return c1;
            }

            set
            {
                c1 = value;
            }
        }

        Collider c2;
        public Collider C2
        {
            get
            {
                return c2;
            }

            set
            {
                c2 = value;
            }
        }

        GameObject g1;
        public GameObject G1
        {
            get
            {
                return g1;
            }

            set
            {
                g1 = value;
            }
        }

        GameObject g2;
        public GameObject G2
        {
            get
            {
                return g2;
            }

            set
            {
                g2 = value;
            }
        }

        public Collision(Collider c1, Collider c2)
        {
            this.C1 = c1;
            this.c2 = c2;
            this.G1 = c1.gameObject;
            this.G2 = c2.gameObject;
        }
    }
}
