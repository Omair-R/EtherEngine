using EtherEngine.Collision.Models;
using EtherEngine.Shapes;
using EtherEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace EtherEngine.Collision.Utils
{
    static public class CollisionUtils
    {

        #region SimilarShapes
        // Tested
        static public bool CircleOnCircleCollision(Circle circleA, Circle circleB, out Contact contact)
        {
            float combinedRadius = circleA.Radius + circleB.Radius;

            Vector2 distanceVector = circleB.Center - circleA.Center;
            float distance = distanceVector.Length();

            if (distance < combinedRadius)
            {
                contact = new Contact
                {
                    penetration = combinedRadius - distance,
                    collisionDirection = distanceVector / distance,
                }; //temporary

                return true;
            }

            contact = null;
            return false;
        }

        //Tested
        static public bool StaticQuadOnStaticQuadCollision(StaticQuad quadA, StaticQuad quadB, out Contact contact)
        {
            //Skips all the expensive projections.
            Vector2 minA = quadA.GetMin();
            Vector2 maxA = quadA.GetMax();

            Vector2 minB = quadB.GetMin();
            Vector2 maxB = quadB.GetMax();


            if (minA.Y >= maxB.Y || minB.Y >= maxA.Y || minA.X >= maxB.X || minB.X >= maxA.X)
            {
                contact = null;
                return false;
            }

            float penetrationX = MathF.Min(maxA.X - minB.X, maxB.X - minA.X);
            float penetrationY = MathF.Min(maxA.Y - minB.Y, maxB.Y - minA.Y);

            contact = new Contact();
            if (MathF.Abs(penetrationX) < MathF.Abs(penetrationY))
            {
                contact.penetration = penetrationX;
                contact.collisionDirection = Vector2.UnitX * MathF.Sign(quadB.X - quadA.X);
            }
            else
            {
                contact.penetration = penetrationY;
                contact.collisionDirection = Vector2.UnitY * MathF.Sign(quadB.Y - quadA.Y);
            }

            return true;
        }

        static public bool RotatableQuadOnRotatableQuadCollision(RotatableQuad quadA, RotatableQuad quadB, out Contact contact)
        {
            Vector2[] normals = new Vector2[4];

            normals[0] = quadA.Normals[0];
            normals[1] = quadA.Normals[1];
            normals[2] = quadA.Normals[0];
            normals[3] = quadA.Normals[1];

            bool collision = SPA.SeparatingAxisTheorem(normals, quadA.Vertices, quadB.Vertices, out contact);

            if (!collision)
                return false;

            contact.collisionDirection = SPA.TreatCollisionDirection(quadA.GetCenter(), quadB.GetCenter(), contact.collisionDirection, false);

            return true;
        }

        static public bool PolygonOnPolygonCollision(Polygon polygonA, Polygon polygonB, out Contact contact)
        {
            Vector2[] normals = new Vector2[polygonA.Vertices.Length + polygonB.Vertices.Length];

            Array.Copy(polygonA.GetNormals(), normals, polygonA.Vertices.Length);
            Array.Copy(polygonB.GetNormals(), 0, normals, polygonA.Vertices.Length, polygonB.Vertices.Length);

            bool collision = SPA.SeparatingAxisTheorem(normals, polygonA.Vertices, polygonB.Vertices, out contact);

            if (!collision)
                return false;


            contact.collisionDirection = SPA.TreatCollisionDirection(polygonA.GetCenter(), polygonB.GetCenter(), contact.collisionDirection, false);

            return true;
        }

        #endregion

        #region NonSimilarShapes
        //Tested
        static public bool CircleOnStaticQuadCollision(Circle circle, StaticQuad quad, out Contact contact, bool isOpposite = false)
        {
            Vector2 DistanceVector = circle.Center - quad.GetCenter();
            Vector2 HalfWidthHeight = new Vector2(quad.Width / 2, quad.Height / 2);

            Vector2 Clamped = Vector2.Clamp(DistanceVector, -HalfWidthHeight, HalfWidthHeight);
            //from the center of the circle
            Vector2 shortestPoint = quad.GetCenter() + Clamped;

            if (isOpposite)
                DistanceVector = circle.Center - shortestPoint;
            else
                DistanceVector = shortestPoint - circle.Center;

            if (DistanceVector.Length() > circle.Radius)
            {
                contact = null;
                return false;
            }

            contact = new Contact()
            {
                penetration = circle.Radius - DistanceVector.Length(),
                collisionDirection = DistanceVector == Vector2.Zero ? Vector2.UnitY : MathUtils.Normalize(DistanceVector)
            };

            return true;
        }

        static public bool CircleOnRotatableQuadCollision(Circle circle, RotatableQuad quad, out Contact contact, bool isOpposite = false)
        {
            Vector2[] normals = new Vector2[3];
            normals[0] = SPA.ClosestVector(quad.Vertices, circle.Center);
            normals[1] = quad.Normals[0];
            normals[2] = quad.Normals[1];

            bool collision = SPA.SeparatingAxisTheoremCircle(normals, quad.Vertices, circle, out contact);

            if (!collision)
                return false;

            contact.collisionDirection = SPA.TreatCollisionDirection(circle.Center, quad.GetCenter(), contact.collisionDirection, isOpposite);

            return true;
        }

        //Tested
        static public bool StaticQuadOnRotatableQuadCollision(StaticQuad staticQuad, RotatableQuad rotatableQuad, out Contact contact, bool isOpposite = false)
        {
            Vector2[] normals = new Vector2[4];

            normals[0] = Vector2.UnitX;
            normals[1] = Vector2.UnitY;
            normals[2] = rotatableQuad.Normals[0];
            normals[3] = rotatableQuad.Normals[1];


            bool collision = SPA.SeparatingAxisTheorem(normals, staticQuad.Vertices, rotatableQuad.Vertices, out contact);

            if (!collision)
                return false;

            contact.collisionDirection = SPA.TreatCollisionDirection(staticQuad.GetCenter(), rotatableQuad.GetCenter(), contact.collisionDirection, isOpposite);

            return true;
        }

        // Polygons -- Leaving this to later
        static public bool CircleOnPolygonCollision(Circle circle, Polygon polygon, out Contact contact, bool isOpposite = false)
        {
            Vector2[] normals = new Vector2[polygon.Vertices.Length + 1];

            Array.Copy(polygon.GetNormals(), normals, polygon.Vertices.Length);
            normals[polygon.Vertices.Length] = SPA.ClosestVector(polygon.Vertices, circle.Center);

            bool collision = SPA.SeparatingAxisTheoremCircle(normals, polygon.Vertices, circle, out contact);

            if (!collision)
                return false;

            contact.collisionDirection = SPA.TreatCollisionDirection(circle.Center, polygon.GetCenter(), contact.collisionDirection, isOpposite);

            return true;
        }

        static public bool StaticQuadOnPolygonCollision(StaticQuad quad, Polygon polygon, out Contact contact, bool isOpposite = false)
        {
            Vector2[] normals = new Vector2[polygon.Vertices.Length + 2];

            Array.Copy(polygon.GetNormals(), normals, polygon.Vertices.Length);
            normals[polygon.Vertices.Length] = Vector2.UnitX;
            normals[polygon.Vertices.Length + 1] = Vector2.UnitY;

            bool collision = SPA.SeparatingAxisTheorem(normals, polygon.Vertices, quad.Vertices, out contact);

            if (!collision)
                return false;

            contact.collisionDirection = SPA.TreatCollisionDirection(quad.GetCenter(), polygon.GetCenter(), contact.collisionDirection, isOpposite);

            return true;
        }

        static public bool RotatableQuadOnPolygonCollision(RotatableQuad quad, Polygon polygon, out Contact contact, bool isOpposite = false)
        {
            Vector2[] normals = new Vector2[polygon.Vertices.Length + 2];

            Array.Copy(polygon.GetNormals(), normals, polygon.Vertices.Length);
            Array.Copy(quad.Normals, 0, normals, polygon.Vertices.Length, 2);

            bool collision = SPA.SeparatingAxisTheorem(normals, polygon.Vertices, quad.Vertices, out contact);

            if (!collision)
                return false;

            contact.collisionDirection = SPA.TreatCollisionDirection(quad.GetCenter(), polygon.GetCenter(), contact.collisionDirection, isOpposite);

            return true;
        }

        #endregion


    }
}
