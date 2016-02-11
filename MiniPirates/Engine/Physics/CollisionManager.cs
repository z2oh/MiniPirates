using Microsoft.Xna.Framework;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Physics
{
    public class CollisionManager
    {
        List<Collider> dynamicColliders;
        List<Collider> staticColliders;

        public CollisionManager()
        {
            dynamicColliders = new List<Collider>();
            staticColliders = new List<Collider>();
        }

        public void CheckForCollisions()
        {
            for(int i = 0; i < dynamicColliders.Count; i++)
            {
                for(int j = i+1; j < dynamicColliders.Count; j++)
                {
                    Collision c;
                    if (dynamicColliders[i] is CircleCollider)
                    {
                        c = CheckForCollision(dynamicColliders[i] as CircleCollider, dynamicColliders[j] as CircleCollider);
                    }
                    else
                    {
                        c = CheckForCollision(dynamicColliders[i] as MultiCircleCollider, dynamicColliders[j] as CircleCollider);
                    }
                    if (c != null)
                    {
                        dynamicColliders[i].newCollisions.Add(c);
                        dynamicColliders[j].newCollisions.Add(c);
                    }
                }
                
                for(int j = 0; j < staticColliders.Count; j++)
                {
                    Collision c;
                    if(dynamicColliders[i] is CircleCollider)
                    {
                        c = CheckForCollision(dynamicColliders[i] as CircleCollider, staticColliders[j] as CircleCollider);
                    }
                    else
                    {
                        c = CheckForCollision(dynamicColliders[i] as MultiCircleCollider, staticColliders[j] as CircleCollider);
                    }
                    if(c != null)
                    {
                        dynamicColliders[i].newCollisions.Add(c);
                        staticColliders[j].newCollisions.Add(c);
                    }
                }
            }
            for(int i = 0; i < dynamicColliders.Count; i++)
            {
                dynamicColliders[i].collisions = dynamicColliders[i].newCollisions.ToList<Collision>();
                dynamicColliders[i].newCollisions.Clear();
                dynamicColliders[i].IsColliding = (dynamicColliders[i].collisions.Count > 0);
            }
            for(int i = 0; i < staticColliders.Count; i++)
            {
                staticColliders[i].collisions = staticColliders[i].newCollisions.ToList<Collision>();
                staticColliders[i].newCollisions.Clear();
                staticColliders[i].IsColliding = (staticColliders[i].collisions.Count > 0);
            }
        }

        /// <summary>
        /// Checks if there is a collision between two circle colliders.
        /// </summary>
        /// <remarks>This should return a Collision object with more information that just true or false.</remarks>
        /// <param name="c1">The first circle collider.</param>
        /// <param name="c2">The second circle collider.</param>
        /// <returns>True if there is a collision, false if there is not.</returns>
        private Collision CheckForCollision(CircleCollider c1, CircleCollider c2)
        {
            float lengthSquared = (c1.Center - c2.Center).Length();
            float sumOfRadiiSquared = c1.Radius + c2.Radius;
            if (lengthSquared < sumOfRadiiSquared)
            {
                return new Collision(c1, c2);
            }
            return null;
        }

        private Collision CheckForCollision(MultiCircleCollider c1, CircleCollider c2)
        {
            foreach(Tuple<Vector2, float> circle in c1.Circles)
            {
                float lengthSquared = (circle.Item1 - c2.Center).Length();
                float sumOfRadiiSquared = circle.Item2 + c2.Radius;
                if (lengthSquared < sumOfRadiiSquared)
                {
                    return new Collision(c1, c2);
                }
            }
            return null;
        }

        public void AddStaticCollider(Collider c)
        {
            staticColliders.Add(c);
        }

        public void AddDynamicCollider(Collider c)
        {
            dynamicColliders.Add(c);
        }

        public void RemoveDynamicCollider(Collider c)
        {
            dynamicColliders.Remove(c);
        }
    }
}
