using Arch.Core;
using Arch.Core.Extensions;
using EtherEngine.Components;
using EtherEngine.Components.Relations;
using EtherUtils;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;

namespace EtherEngine.Entities
{
    public partial class EtherEntity //TODO: make disposable
    {
        protected readonly EtherScene _scene;
        private readonly Entity _entityHandle;
        public event EventHandler<object> ComponentAdded;


        public bool IsAlive { get; private set; }

        public static EtherEntity Wrap(EtherScene scene, Entity entityHandle)
        {
            ref var id = ref entityHandle.Get<IdComponent>();

            if (scene.EntityManager.HasEntity(id.Id))
                return scene.EntityManager.GetEntity(id.Id);

            else throw new Exception("Entity does not exist, please create entities through the entity manager.");
        }

        private EtherEntity(EtherScene scene, Entity entityHandle)
        {
            _entityHandle = entityHandle;
            _scene = scene;
            if (!entityHandle.Has<IdComponent>()) AddComponent(new IdComponent());
            if (!entityHandle.Has<TagComponent>()) AddComponent(new TagComponent());
        }

        internal EtherEntity(EtherScene scene, Entity entity, Guid? id = null, string tag = null)
        {
            _entityHandle = entity;
            IsAlive = true;
            AddComponent(id == null ? new IdComponent() : new IdComponent((Guid)id));
            AddComponent(tag == null ? new TagComponent() : new TagComponent(tag));
        }

        public static implicit operator Entity(EtherEntity e) => e._entityHandle;

        public Guid GetUid() => GetComponent<IdComponent>().Id;
        public string GetTag() => GetComponent<TagComponent>().Tag;

        internal Entity GetHandle() => _entityHandle;

        public void AddComponent<T>(T component)
        {
            if (HasComponent<T>()) return;

            _entityHandle.Add(component);
            EventUtils.Invoke(ComponentAdded, this, component);
        }

        public void SetComponent<T>(T component)
        {
            if (!_entityHandle.Has<T>())
                _entityHandle.Add(component);
            else
            {
                _entityHandle.Set(component);
                EventUtils.Invoke(ComponentAdded, this, component);
            }
        }

        public void RemoveComponent<T>() => _entityHandle.Remove<T>();

        public ref T GetComponent<T>() => ref _entityHandle.Get<T>();

        public bool HasComponent<T>() => _entityHandle.Has<T>();

        public object[] GetAllComponenet() => _entityHandle.GetAllComponents();


        public override bool Equals(object obj)
        {
            return obj is EtherEntity entity &&
                   GetUid() == entity.GetUid();
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(GetUid);
            return hash.ToHashCode();
        }
    }
}
