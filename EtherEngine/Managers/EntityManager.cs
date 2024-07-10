using Arch.Core;
using Arch.Core.Extensions;
using EtherEngine.Components;
using EtherEngine.Entities;
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

        public EtherEntity MakeEntity(Guid? id = null, string tag = null) 
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
            return _entityMap.Values.ToArray();
        }

        public EtherEntity[] GetEntities<T>()
        {
            return GetEntities(new QueryDescription().WithAll<T>());
        }

        public EtherEntity[] GetEntities(QueryDescription queryDescription)
        {
            Span<Entity> span = new Span<Entity>();
            _scene._world.GetEntities(queryDescription, span);

            EtherEntity[] entities = new EtherEntity[span.Length];

            for (int i = 0; i < span.Length; i++)
            {
                entities[i] = new EtherEntity(_scene, span[i]);
            }

            return entities;
        }

        public void DestroyEntities(QueryDescription queryDescription)
        {
            Span<Entity> span = new Span<Entity>();
            _scene._world.GetEntities(queryDescription, span);

            for (int i = 0; i < span.Length; i++)
            {
                if(_entityMap.Remove(span[i].Get<IdComponent>().Id))
                    _scene._world.Destroy(span[i]);
            }
        }

        public void DestroyEntities<T>()
        {
            DestroyEntities(new QueryDescription().WithAll<T>()); ;
        }


    }
}
