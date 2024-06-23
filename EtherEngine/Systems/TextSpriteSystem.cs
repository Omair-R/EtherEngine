using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Components.Graphics;
using EtherEngine.Core.DrawBatch;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace EtherEngine.Systems
{
    public class TextSpriteSystem : DrawableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<TextSpriteComponent, TransformComponent, ColorComponent>();
        public TextSpriteSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            Query query = _scene._world.Query(in queryDescription);

            _scene.spriteBatch.Begin();
            foreach (ref Chunk chunk in query)
            {
                chunk.GetSpan<TextSpriteComponent, TransformComponent, ColorComponent>(out var textSprites, out var transforms, out var colors);

                foreach (int index in chunk)
                {
                    ref var sprite = ref textSprites[index];
                    ref var transform = ref transforms[index];
                    ref var color = ref colors[index];

                    //sprite.Writer.Write(_scene._spriteBatch,
                    //                    sprite.Text,
                    //                    transform.Position,
                    //                    color.GetActualColor(),
                    //                    transform.Rotation,
                    //                    scale: transform.Scale.X,
                    //                    layerDepth: sprite.LayerDepth);
                }


            }
            _scene.spriteBatch.End();
        }
    }
}
