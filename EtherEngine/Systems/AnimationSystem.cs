using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Components.Graphics;
using Microsoft.Xna.Framework;

namespace EtherEngine.Systems
{

    public class AnimationSystem : UpdatableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<SpriteAnimationComponent, SpriteComponent>();

        public AnimationSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Query query = _scene._world.Query(in queryDescription);

            foreach (ref Chunk chunk in query)
            {
                chunk.GetSpan<SpriteAnimationComponent, SpriteComponent>(out var animations, out var sprites);

                foreach (int index in chunk)
                {
                    ref var animation = ref animations[index];
                    ref var sprite = ref sprites[index];

                    if (animation.IsPaused) continue;

                    animation.RemainingTime -= dt;
                    if (animation.RemainingTime <= 0)
                    {
                        animation.CurrentFrame = (animation.CurrentFrame + 1) % (animation.HFrameCount * animation.VFrameCount);
                        animation.RemainingTime += animation.FrameDuration;
                        int HframeSize = sprite.Texture.Width / animation.HFrameCount;
                        int VframeSize = sprite.Texture.Height / animation.VFrameCount;
                        sprite.SrcRect = new Rectangle((int)animation.FrameCoordinates.X + animation.CurrentFrame % animation.HFrameCount * HframeSize,
                                                        (int)animation.FrameCoordinates.Y + animation.CurrentFrame % animation.VFrameCount * VframeSize,
                                                        HframeSize, VframeSize); //TODO:optimize.
                    }
                }

            }

        }

    }
}
