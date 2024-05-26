using Arch.Core;
using Arch.Core.Extensions;
using EtherEngine.Components;
using EtherEngine.Utils;
using System;

namespace EtherEngine
{
    public class EtherEntity //TODO: make disposable
    {
        private readonly Entity _entityHandle;

        public event EventHandler<object> ComponentAdded;
        public bool IsAlive { get; private set; }

        internal EtherEntity(Entity entity, Guid? id=null, string tag = null)
        {
            _entityHandle = entity;
            IsAlive = true;
            AddComponent(new IdComponent(id ?? (Guid)id));
            AddComponent(new TagComponent(tag ?? tag));
        }

        public static implicit operator Entity(EtherEntity e) => e._entityHandle;

        public Guid GetUid() => GetComponent<IdComponent>().Id;
        public string GetTag() => GetComponent<TagComponent>().Tag;

        internal Entity GetHandle() => _entityHandle;

        public void AddComponent<T>(T component){
            _entityHandle.Add<T>(component);
            EventUtils.Invoke(ComponentAdded, this, component);
        }

        public void ReplaceComponent<T>(T component) { 
            _entityHandle.Set<T>(component);
            EventUtils.Invoke(ComponentAdded, this, component);
        }

        public void RemoveComponent<T>() => _entityHandle.Remove<T>();

        public T GetComponent<T>() =>_entityHandle.Get<T>();

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
