using EtherEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Entities
{
    internal class ObjectEntity : EntityWrapper
    {
        public ObjectEntity(EtherScene scene, Guid? id = null, string tag = null) : base(scene.EntityManager.MakeEntity(id, tag), scene)
        {
            _entity.AddComponent(new TransformComponent());
        }

        public static ObjectEntity Wrap(EtherEntity entity, EtherScene scene)
        {
            if (entity.HasComponent<TransformComponent>())
            {
                return new ObjectEntity(scene, entity.GetUid(), entity.GetTag());
            }

            throw new Exception("Not a object");
        }

    }
}
