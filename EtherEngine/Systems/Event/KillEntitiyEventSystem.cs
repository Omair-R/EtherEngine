using Arch.Core;
using EtherEngine.Entities;

namespace EtherEngine.Systems.Event
{
    public struct KillComponent : IEvent
    {
        public Entity Sender { get; set; }
    }

    public class KillEntitiyEventSystem : EventSystem
    {
        public KillEntitiyEventSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<KillComponent>();
        }

        public override void Handle()
        {
            _scene.EntityManager.DestroyEntities(queryDescription);
        }
    }
}
