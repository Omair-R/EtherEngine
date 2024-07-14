using Arch.Core;
using Arch.Core.Extensions;
using EtherEngine.Components;
using EtherEngine.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Managers
{
    public interface Returner
    {
        object Get();
        void Set(in object value);
    }

    public sealed class Returner<T> : Returner where T : struct
    {
        T innerObj;

        public Returner(in T obj)
        {
            innerObj = obj;
        }

        public object Get() => innerObj;

        public void Set(in object obj) => innerObj = (T)obj;

    }

    public sealed class Option<T> { }
    public sealed class Must<T> { }

    public class EntityManager
    {
        private readonly EtherScene _scene;
        private Dictionary<Guid, EtherEntity> _entityMap;
        private Dictionary<Type, Returner> _resources = new Dictionary<Type, Returner>();

        public World Registry { get; protected set; }

        public EntityManager(EtherScene scene)
        {
            _scene = scene;
            Registry = World.Create();
            _entityMap = new Dictionary<Guid, EtherEntity>();
        }

        public EtherEntity MakeEntity(Guid? id = null, string tag = null) 
        {
            Entity entity = Registry.Create();
            EtherEntity etherEntity = new EtherEntity(_scene, entity, id, tag);

            _entityMap.Add(etherEntity.GetUid(), etherEntity);

            return etherEntity;
        }

        public void DestroyEntity(EtherEntity entity)
        {
            _entityMap.Remove(entity.GetUid());
            Registry.Destroy(entity);
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
            Registry.GetEntities(queryDescription, span);

            EtherEntity[] entities = new EtherEntity[span.Length];

            for (int i = 0; i < span.Length; i++)
            {
                entities[i] = new EtherEntity(_scene, span[i]);
            }

            return entities;
        }

        public void DestroyEntities(QueryDescription queryDescription)
        {
            List<Entity> list = new List<Entity>();
            

            Registry.GetEntities(queryDescription, list);
            
            for (int i = 0; i < list.Count; i++)
            {
                if(_entityMap.Remove(list[i].Get<IdComponent>().Id))
                    Registry.Destroy(list[i]);
            }
        }

        public void DestroyEntities<T>()
        {
            DestroyEntities(new QueryDescription().WithAll<T>()); ;
        }
        
        public T? GetResource<T>(Option<T> option) where T : struct
        {
            if (!_resources.TryGetValue(typeof(T), out Returner value)) return null;
            return (T)value;
        }

        public T GetResource<T>(Must<T> must)
        {
            if (!_resources.ContainsKey(typeof(T))) throw new ArgumentNullException("A must wrapped global component needs to exist but: '"
                                                                                            + nameof(must) + "' was not found.");
            return (T)_resources[typeof(T)].Get();
        }

        public bool SetResource<T>(T obj) where T : struct
        {
            return _resources.TryAdd(typeof(T), new Returner<T>(obj));
        }


    }
}
