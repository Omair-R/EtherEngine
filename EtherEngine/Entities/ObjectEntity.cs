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
        public static ObjectEntity Create(EtherScene scene, Guid? id = null, string tag = null)
        {
            var cameraEntity = new ObjectEntity(scene.entityManager.MakeEntity(id, tag), scene);
            cameraEntity.AddComponent(new TransformComponent());

            return cameraEntity;
        }

        public static ObjectEntity Wrap(EtherEntity entity, EtherScene scene)
        {
            if (entity.HasComponent<TransformComponent>())
            {
                return new ObjectEntity(entity, scene);
            }

            throw new Exception("Not a object");
        }
        internal ObjectEntity(EtherEntity entity, EtherScene scene) : base(entity, scene)
        {
        }
    }
}
