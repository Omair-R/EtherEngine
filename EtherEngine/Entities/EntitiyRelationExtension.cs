using EtherEngine.Components.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Entities
{
    public partial class EtherEntity
    {
        public Dictionary<EtherEntity, T> GetAllReverse<T>() where T : struct
        {
            return GetComponent<ReverseRelation<T>>().Sources;
        }

        public T GetReverse<T>(EtherEntity source) where T : struct
        {
            return source.GetComponent<Relation<T>>().Get(this);
        }

        public bool TryGetReverse<T>(EtherEntity source, out T component) where T : struct
        {
            return source.GetComponent<Relation<T>>().TryGet(this, out component); ;
        }

        public T GetRelation<T>(EtherEntity target) where T : struct
        {
            if (HasComponent<T>()) return GetComponent<T>();
            return GetComponent<Relation<T>>().Get(target);
        }

        public Dictionary<EtherEntity, T> GetAllRelations<T>() where T : struct
        {
            return GetComponent<Relation<T>>().Targets;
        }

        public bool TryGetRelation<T>(EtherEntity target, out T component) where T : struct
        {
            if (HasComponent<T>())
            {
                component = GetComponent<T>();
                return true;
            }

            return GetComponent<Relation<T>>().TryGet(target, out component); ;
        }


        public void AddTwoWayRelation<T>(EtherEntity target, T component, bool skipReverse = false) where T : struct //TODO: make set.
        {
            if (!HasComponent<Relation<T>>())
            {
                var relation = new Relation<T>();
                relation.Add(target, component);
                target.AddComponent(relation);
            }
            else
            {
                target.GetComponent<Relation<T>>().Add(this, component);
            }
            if (!skipReverse)
                CheckReverseAndAdd(target, component);
        }

        public void SetExclusiveRelation<T>(EtherEntity target, T component, bool skipReverse = false) where T : struct
        {
            SetComponent(component);
            if (!skipReverse)
                CheckReverseAndAdd(target, component);
        }

        public void RemoveRelation<T>(EtherEntity target) where T : struct
        {
            
            if (HasComponent<Relation<T>>())
                GetComponent<Relation<T>>().Remove(this, target);

            if (HasComponent<T>())
            {
                RemoveComponent<T>();
                if (target.HasComponent<ReverseRelation<T>>())
                {
                    target.GetComponent<ReverseRelation<T>>().Sources.Remove(this);
                }
            }

        }

        private void CheckReverseAndAdd<T>(EtherEntity target, T component) where T : struct
        {
            if (!target.HasComponent<ReverseRelation<T>>())
            {
                var reverse = new ReverseRelation<T>();
                reverse.Add(this, component);
                target.AddComponent(reverse);
            }
            else
            {
                target.GetComponent<ReverseRelation<T>>().Add(this, component);
            }
        }
    }
}
