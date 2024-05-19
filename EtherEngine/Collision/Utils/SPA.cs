using EtherEngine.Collision.Models;
using EtherEngine.Shapes;
using EtherEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Collision.Utils
{
    public static class SPA
    {
        public static bool SeparatingAxisTheorem(Vector2[] normals, Vector2[] verticesA, Vector2[] verticesB, out Contact contact)
        {
            float depth = float.MaxValue;
            Vector2 contactDirection = Vector2.Zero;

            foreach (Vector2 normal in normals)
            {
                MinMax projectionA = Projection.ProjectVertices(verticesA, normal);
                MinMax projectionB = Projection.ProjectVertices(verticesB, normal);

                if (projectionA.Min >= projectionB.Max ||
                    projectionB.Min >= projectionA.Max)
                {
                    contact = null;
                    return false;
                }

                float depthAxis = MathF.Min(projectionB.Max - projectionA.Min,
                                            projectionA.Max - projectionB.Min);

                if (depthAxis < depth)
                {
                    depth = depthAxis;
                    contactDirection = normal;
                }

            }

            contact = new Contact();
            contact.penetration = depth;
            contact.collisionDirection = MathUtils.Normalize(contactDirection);

            return true;

        }

        public static bool SeparatingAxisTheoremCircle(Vector2[] normals, Vector2[] verticesA, Circle circle, out Contact contact)
        {
            float depth = float.MaxValue;
            Vector2 contactDirection = Vector2.Zero;

            foreach (Vector2 normal in normals)
            {
                MinMax projectionA = Projection.ProjectVertices(verticesA, normal);
                MinMax projectionB = Projection.ProjectCircle(circle.Center, circle.Radius, normal);

                if (projectionA.Min >= projectionB.Max ||
                    projectionB.Min >= projectionA.Max)
                {
                    contact = null;
                    return false;
                }

                float depthAxis = MathF.Min(projectionB.Max - projectionA.Min,
                                            projectionA.Max - projectionB.Min);

                if (depthAxis < depth)
                {
                    depth = depthAxis;
                    contactDirection = normal;
                }

            }

            contact = new Contact();
            contact.penetration = depth;
            contact.collisionDirection = MathUtils.Normalize(contactDirection);

            return true;

        }

        public static Vector2 ClosestVector(Vector2[] vectors, Vector2 target)
        {
            if (vectors.Length == 0) throw new Exception();

            float distance = float.MaxValue;
            Vector2 closestVector = Vector2.Zero;

            foreach (Vector2 vector in vectors)
            {
                float currentDistance = Vector2.Distance(vector, target);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closestVector = vector;
                }
            }

            closestVector = Vector2.Normalize(closestVector);

            return closestVector;
        }

        public static Vector2 TreatCollisionDirection(Vector2 CenterA, Vector2 CenterB, Vector2 collisionDirection, bool isOpposite)
        {
            Vector2 centralDifference = CenterA - CenterB;

            if (Vector2.Dot(centralDifference, collisionDirection) > 0)
                collisionDirection = -collisionDirection;

            if (isOpposite) //HACK: Maybe combine this.
                collisionDirection = -collisionDirection;

            return MathUtils.Normalize(collisionDirection);
        }
    }
}
