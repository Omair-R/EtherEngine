using Arch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Event
{
    public struct KillParticleComponent : IEvent
    {
        public Entity Sender { get; set; }
    }
    public class DestroyParticlesEventSystem : EventSystem
    {
        public DestroyParticlesEventSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<KillParticleComponent>();
        }

        public override void Handle()
        {
            _scene.EntityManager.Registry.Destroy(queryDescription);
        }
    }
}
