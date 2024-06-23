﻿using Arch.Core;
using EtherEngine.Components;
using Microsoft.Xna.Framework;
using EtherUtils;
using Arch.Core.Extensions;
using System;

namespace EtherEngine.Systems
{
    public class FollowSystem : UpdatableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<TransformComponent, FollowComponent>();

        public FollowSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _scene._world.Query(queryDescription, (in Entity entity, 
                                                   ref TransformComponent transform,
                                                   ref FollowComponent follow) =>
            {
                var followTransform = _scene.entityManager.GetEntity(follow.EntityUID).GetComponent<TransformComponent>();

                switch (follow.FollowType)
                {
                    case (FollowType.Instant):
                        transform.Position = followTransform.Position;
                        break;
                    case (FollowType.PID):
                        if (entity.Has<PIDDriveComponent>())
                        {
                            var pid = entity.Get<PIDDriveComponent>();
                            Vector2 velocity = Vector2.Zero;

                            velocity.X = pid.PIDX.Update(transform.Position.X, followTransform.Position.X, dt);
                            velocity.Y = pid.PIDY.Update(transform.Position.Y, followTransform.Position.Y, dt);

                            velocity.X = MathHelper.Clamp(velocity.X, -pid.MaxVelocity, pid.MaxVelocity);
                            velocity.Y = MathHelper.Clamp(velocity.Y, -pid.MaxVelocity, pid.MaxVelocity);

                            transform.Position += velocity * dt ;
                            
                        }
                        break;
                    default:
                        break;
                }
            });

        }
    }
}
