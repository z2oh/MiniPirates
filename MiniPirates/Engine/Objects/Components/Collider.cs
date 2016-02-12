using MiniPirates.Engine.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Objects.Components
{
    public delegate void CollisionEnter(object sender, Collision c);
    public delegate void CollisionPersist(object sender, Collision c);
    public delegate void CollisionExit(object sender, Collision c);

    public abstract class Collider : DrawableComponent
    {
        internal Transform objectTransform;
        internal Transform cameraTransform;

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

        public event CollisionEnter onEnterCollision;
        public event CollisionPersist onContinueCollision;
        public event CollisionExit onExitCollision;

        public override void Initialize()
        {
            collisions = new List<Collision>();
            isColliding = false;
            Transform t = gameObject.GetComponent<Transform>();
            if (null != t)
            {
                objectTransform = t;
            }
        }

        internal virtual void EnteredCollision(Collision c)
        {
            collisions.Add(c);
            isColliding = true;
            if (onEnterCollision != null)
                onEnterCollision(this, c);
        }

        internal virtual void ContinuedCollision(Collision c)
        {
            if (onContinueCollision != null)
                onContinueCollision(this, c);
        }

        internal virtual void ExitedCollision(Collision c)
        {
            collisions.Remove(c);
            isColliding = collisions.Count > 0;
            if (onExitCollision != null)
                onExitCollision(this, c);
        }

        public virtual void InitializeValues(GameObject camera)
        {
            cameraTransform = camera.GetComponent<Transform>();
        }
    }
}
