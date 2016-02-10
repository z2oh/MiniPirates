using MiniPirates.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public abstract class Collider : DrawableComponent
    {
        internal Transform objectTransform;
        internal Transform cameraTransform;

        internal int numCollisions;
        public int NumCollisions
        {
            get
            {
                return numCollisions;
            }

            set
            {
                numCollisions = value;
            }
        }

        internal bool isColliding;
        public bool IsColliding
        {
            get
            {
                return isColliding;
            }

            set
            {
                isColliding = value;
            }
        }

        public List<Collision> collisions;
        public List<Collision> newCollisions;

        internal delegate void behavior(Collision collision);

        public override void Initialize()
        {
            collisions = new List<Collision>();
            newCollisions = new List<Collision>();
            isColliding = false;
            Transform t = gameObject.GetComponent<Transform>();
            if (null != t)
            {
                objectTransform = t;
            }
        }

        public void SetBehavior(Delegate d)
        {
            behavior b1 = (behavior)d;
            b1(null);
        }

        public virtual void InitializeValues(GameObject camera)
        {
            cameraTransform = camera.GetComponent<Transform>();
        }
    }
}
