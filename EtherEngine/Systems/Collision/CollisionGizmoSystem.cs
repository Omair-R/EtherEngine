using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Core.DrawBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Collision
{
    public class CollisionGizmoSystem : DrawableSystem
    {
        public CollisionGizmoSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<CollisionGizmoComponent, ColliderShapeComponent>();
        }

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            var query = _scene.EntityManager.Registry.Query(queryDescription);

            if (_scene.MainCamera != null)
                shapeBatch.Begin(transformMatrix: _scene.MainCamera.GetTransform());
            else
                shapeBatch.Begin();

            foreach (ref Chunk chunk in query)
            {
                chunk.GetSpan<CollisionGizmoComponent, ColliderShapeComponent>(out var gizmos, out var shapes);
                foreach(int i in chunk)
                {
                    ref CollisionGizmoComponent gizmo = ref gizmos[i];
                    ref ColliderShapeComponent shape = ref shapes[i];

                    shapeBatch.DrawShape(shape.Shape, new Color(gizmo.Color, gizmo.Alpha));
                }
            }
            shapeBatch.End();
        }
    }
}
