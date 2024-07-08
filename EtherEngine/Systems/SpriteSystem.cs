using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Components.Graphics;
using EtherEngine.Core.DrawBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EtherEngine.Systems
{
    public class SpriteSystem : DrawableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<TransformComponent, SpriteComponent, ColorComponent>();

        public SpriteSystem(EtherScene scene) : base(scene)
        {
        }

        public Rectangle GetDestinationRectangle(in TransformComponent transform)
            => new Rectangle((int)transform.Position.X,
                            (int)transform.Position.Y,
                            (int)transform.Scale.X,
                            (int)transform.Scale.Y);

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            var query = _scene._world.Query(in queryDescription);

            if (_scene.MainCamera != null)
                spriteBatch.Begin(transformMatrix: _scene.MainCamera.GetTransform(), samplerState: SamplerState.PointWrap);
            else 
                spriteBatch.Begin(samplerState: SamplerState.PointWrap);

            foreach (ref var chunk in query)
            {
                ref var entityFirstElement = ref chunk.Entity(0);
                chunk.GetSpan<TransformComponent, SpriteComponent, ColorComponent>(out var transforms, out var sprites, out var colors); //TODO: encapsulate.

                foreach (var index in chunk)
                {
                    ref var transform = ref transforms[index];
                    ref var sprite = ref sprites[index];
                    ref var color = ref colors[index];

                    //spriteBatch.Draw(sprite.Texture,
                    //                GetDestinationRectangle(transform),
                    //                sprite.SrcRect,
                    //                color.Color,
                    //                transform.Rotation,
                    //                new Vector2(sprite.SrcRect.Width / 2, sprite.SrcRect.Height / 2),
                    //                sprite.Effect,
                    //                sprite.LayerDepth);

                    spriteBatch.Draw(sprite.Texture, 
                                    transform.Position, 
                                    sprite.SrcRect, color.Color, transform.Rotation,
                                    new Vector2(sprite.SrcRect.Width / 2, sprite.SrcRect.Height / 2),
                                    transform.Scale,
                                    sprite.Effect,
                                    sprite.LayerDepth);


                }
            }
            spriteBatch.End();
        }
    }
}
