using Arch.Core;
using Arch.Core.Extensions;
using EtherEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Components.Relations
{
    public struct Relation<T> where T : struct
    {
        public Dictionary<EtherEntity, T> Targets = new Dictionary<EtherEntity, T>();

        public Relation() { }

        public void Add(EtherEntity target, T component)
        {
            if(Targets == null) Targets = new Dictionary<EtherEntity, T>();

            Targets.Add(target, component);
        }

        public T Get(EtherEntity target)
        {
            return Targets[target];
        }

        public bool TryGet(EtherEntity target, out T component)
        {
            return Targets.TryGetValue(target, out component);
        }

        public void Remove(EtherEntity source, EtherEntity target)
        {
            if (Targets == null) return;
            if (source == null) throw new ArgumentNullException();

            if (target.HasComponent<ReverseRelation<T>>())
            {
                target.GetComponent<ReverseRelation<T>>().Sources.Remove(source);
            }

            Targets.Remove(target);
        }

        public bool Has(EtherEntity target)
        {
            return Targets.ContainsKey(target);
        }
    }
}
