using Microsoft.Xna.Framework;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MiniPirates.Engine.Utility.Enum;

namespace MiniPirates.Engine.Physics
{
    public class CollisionManager
    {
        List<Collider> dynamicColliders;
        List<Collider> staticColliders;

        HashSet<Collision> previousCollisions;
        HashSet<Collision> activeCollisions;

        public CollisionManager()
        {
            dynamicColliders = new List<Collider>();
            staticColliders = new List<Collider>();

            previousCollisions = new HashSet<Collision>();
            activeCollisions = new HashSet<Collision>();
        }

        public void CheckForCollisions()
        {
            previousCollisions = Copyset<Collision>(activeCollisions);
            activeCollisions.Clear();

            // For each dynamic collider we check if it is colliding with any other dynamic colliders or any static colliders.
            for(int i = 0; i < dynamicColliders.Count; i++)
            {
                for (int j = i+1; j < dynamicColliders.Count; j++)
                {
                    Collision c = CheckForCollision(dynamicColliders[i], dynamicColliders[j]);
                    if (c != null)
                    {
                        activeCollisions.Add(c);
                    }
                }
                
                for(int j = 0; j < staticColliders.Count; j++)
                {
                    Collision c = CheckForCollision(dynamicColliders[i], staticColliders[j]);
                    if (c != null)
                    {
                        activeCollisions.Add(c);
                    }
                }
            }

            HashSet<Collision> newCollisions = Copyset<Collision>(activeCollisions);
            newCollisions.ExceptWith(previousCollisions);

            HashSet<Collision> continuedCollisions = Copyset<Collision>(activeCollisions);
            continuedCollisions.IntersectWith(previousCollisions);

            HashSet<Collision> exitedCollisions = Copyset<Collision>(previousCollisions);
            exitedCollisions.ExceptWith(continuedCollisions);

            foreach (Collision c in newCollisions)
            {
                c.C1.EnteredCollision(c);
                if(!c.IsDead())
                    c.C2.EnteredCollision(c);
            }

            foreach (Collision c in continuedCollisions)
            {
                c.C1.ContinuedCollision(c);
                c.C2.ContinuedCollision(c);
            }

            foreach (Collision c in exitedCollisions)
            {
                c.C1.ExitedCollision(c);
                c.C2.ExitedCollision(c);
            }
        }

        private Collision CheckForCollision(Collider c1, Collider c2)
        {
            if(c1 is CircleCollider)
            {
                if(c2 is CircleCollider)
                {
                    return CheckForCollision(c1 as CircleCollider, c2 as CircleCollider);
                }
                else if(c2 is MultiCircleCollider)
                {
                    return CheckForCollision(c2 as MultiCircleCollider, c1 as CircleCollider);
                }
            }
            else if(c1 is MultiCircleCollider)
            {
                if (c2 is CircleCollider)
                {
                    return CheckForCollision(c1 as MultiCircleCollider, c2 as CircleCollider);
                }
                else if (c2 is MultiCircleCollider)
                {
                    return CheckForCollision(c1 as MultiCircleCollider, c2 as MultiCircleCollider);
                }
            }
            return null;
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

        private Collision CheckForCollision(MultiCircleCollider c1, MultiCircleCollider c2)
        {
            foreach (Tuple<Vector2, float> circle in c1.Circles)
            {
                foreach (Tuple<Vector2, float> circle2 in c2.Circles)
                {
                    float lengthSquared = (circle.Item1 - circle2.Item1).Length();
                    float sumOfRadiiSquared = circle.Item2 + circle2.Item2;
                    if (lengthSquared < sumOfRadiiSquared)
                    {
                        return new Collision(c1, c2);
                    }
                }
            }
            return null;
        }

        public void AddCollider(Collider c)
        {
            if(c.ColliderType == ColliderType.Undefined || c.ColliderType == ColliderType.Dynamic)
            {
                AddDynamicCollider(c);
            }
            else
            {
                AddStaticCollider(c);
            }
        }

        public void AddStaticCollider(Collider c)
        {
            c.ColliderType = ColliderType.Static;
            staticColliders.Add(c);
        }

        public void AddDynamicCollider(Collider c)
        {
            c.ColliderType = ColliderType.Dynamic;
            dynamicColliders.Add(c);
        }

        public void RemoveCollider(Collider c)
        {
            if(c.ColliderType == ColliderType.Dynamic)
            {
                RemoveDynamicCollider(c);
            }
            else if (c.ColliderType == ColliderType.Static)
            {
                RemoveStaticCollider(c);
            }
        }

        public void RemoveDynamicCollider(Collider c)
        {
            dynamicColliders.Remove(c);
        }

        public void RemoveStaticCollider(Collider c)
        {
            staticColliders.Remove(c);
        }

        static HashSet<T> Copyset<T>(HashSet<T> set)
        {
            HashSet<T> newSet = new HashSet<T>();
            foreach(T t in set)
            {
                newSet.Add(t);
            }
            return newSet;
        }
    }
}
