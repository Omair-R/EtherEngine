using Arch.Core;
using EtherEngine.Entities;
using System.Collections.Generic;

namespace EtherEngine.Components.Relations
{
    public struct ReverseRelation<T> where T : struct
    {
        internal Dictionary<EtherEntity,T> Sources;


        public void Add(EtherEntity source, T component)
        {
            if (Sources == null) { Sources = new Dictionary<EtherEntity,T>(); }
            Sources.Add(source, component);
        }

        public T Get(EtherEntity target)
        {
            return Sources[target];
        }

        public bool TryGet(EtherEntity target, out T component)
        {
            return Sources.TryGetValue(target, out component);
        }
    }
}
