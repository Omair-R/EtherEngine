using Arch.Core;
using Arch.Core.Extensions;
using EtherEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Managers
{
    public class EntityManager
    {
        private readonly EtherScene _scene;
        private Dictionary<Guid, EtherEntity> _entityMap;

        public EntityManager(EtherScene scene)
        {
            _scene = scene;
            _entityMap = new Dictionary<Guid, EtherEntity>();
        }

        public EtherEntity MakeEntity(Guid? id = null, string tag = null) //TODO: Add Tag
        {
            Entity entity = _scene._world.Create();
            EtherEntity etherEntity = new EtherEntity(_scene, entity, id, tag);

            _entityMap.Add(etherEntity.GetUid(), etherEntity);

            return etherEntity;
        }

        public void DestroyEntity(EtherEntity entity)
        {
            _entityMap.Remove(entity.GetUid());
            _scene._world.Destroy(entity);
        }

        public EtherEntity GetEntity(Guid guid)
        {
            return _entityMap[guid];
        }

        public bool HasEntity(Guid guid)
        {
            return _entityMap.ContainsKey(guid);
        }

        public EtherEntity[] GetAllEntities()
        {
            //Span<Entity> span = new Span<Entity>();
            //_scene._world.GetEntities(new QueryDescription(), span);

            //EtherEntity[] entities = new EtherEntity[span.Length];

            //for (int i = 0; i < span.Length; i++)
            //{
            //    entities[i] = new EtherEntity(_scene, span[i]);
            //}

            //return entities;

            return _entityMap.Values.ToArray();
        }


    }
}
