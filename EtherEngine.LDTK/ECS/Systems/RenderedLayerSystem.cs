using Arch.Core;
using EtherEngine.Core.DrawBatch;
using EtherEngine.LDTK.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace EtherEngine.LDTK.ECS.Systems
{
    public class RenderedLayerSystem : DrawableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<RenderedLayerComponent>();
        public RenderedLayerSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            Query query = _scene._world.Query(queryDescription);

            if (_scene.MainCamera != null)
                spriteBatch.Begin(transformMatrix: _scene.MainCamera.GetTransform(), samplerState: SamplerState.PointClamp);
            else
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var chunk in query)
            {
                var renderedLayers = chunk.GetArray<RenderedLayerComponent>();

                foreach (var index in chunk)
                {
                    ref var renderedLayer = ref renderedLayers[index];

                    spriteBatch.Draw(renderedLayer.Texture,
                                     Vector2.Zero,
                                     new Rectangle(0, 0, renderedLayer.Texture.Width, renderedLayer.Texture.Height),
                                     Color.White,
                                     0,
                                     Vector2.Zero,
                                     renderedLayer.Scale == Vector2.Zero ? Vector2.One : renderedLayer.Scale,
                                     SpriteEffects.None,
                                     0);
                }
            }

            spriteBatch.End();
        }
    }
}
