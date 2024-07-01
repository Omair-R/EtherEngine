using Arch.Core;
using System;


namespace EtherEngine.Entities
{
    public abstract class EntityWrapper
    {
        protected EtherEntity _entity;
        protected EtherScene _scene;

        public event EventHandler<object> ComponentAdded 
        {
            add
            {
                _entity.ComponentAdded += value;
            }
            remove
            {
                _entity.ComponentAdded -= value;
            }
        }

        protected EntityWrapper(EtherEntity entity, EtherScene scene)
        {
            _entity = entity;
            _scene = scene;

        }

        public static implicit operator EtherEntity(EntityWrapper entityWrapper) => entityWrapper._entity;

        public Guid GetUid() => _entity.GetUid();

        public string GetTag() => _entity.GetTag();

        internal Entity GetHandle() => _entity.GetHandle();

        public void AddComponent<T>(T component) => _entity.AddComponent(component);

        public void ReplaceComponent<T>(T component) => _entity.SetComponent(component);

        public void RemoveComponent<T>() => _entity.RemoveComponent<T>();

        public ref T GetComponent<T>() => ref _entity.GetComponent<T>();

        public bool HasComponent<T>() => _entity.HasComponent<T>();

        public object[] GetAllComponenet() => _entity.GetAllComponenet();

        public override bool Equals(object obj)
        {
            if (obj is EtherEntity entity)
            {
                return _entity.GetUid() == entity.GetUid();
            }

            if (obj is EntityWrapper entityWrapper)
            {
                return _entity.GetUid() == entityWrapper._entity.GetUid();
            }

            return false;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(_entity.GetUid);
            return hash.ToHashCode();
        }

    }
}
