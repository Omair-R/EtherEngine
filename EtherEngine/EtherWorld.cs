using Arch.Core;
using System;
using System.Collections.Generic;

namespace EtherEngine
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

    public class EtherWorld
    {
        Dictionary<Type, Returner> _globalComponents = new Dictionary<Type, Returner>();

        public World Inner { get; protected set; }
        
        public EtherWorld() //TODO: Change to Create?
        {
            Inner = World.Create();
        }

        public static implicit operator World(EtherWorld etherWorld) => etherWorld.Inner;

        public T? GetGlobal<T>(Option<T> option) where T : struct
        {
            if (!_globalComponents.TryGetValue(typeof(T), out Returner value)) return null;
            return (T)value;
        }

        public T GetGlobal<T>(Must<T> must)
        {
            if (!_globalComponents.ContainsKey(typeof(T))) throw new ArgumentNullException("A must wrapped global component needs to exist but: '" 
                                                                                            + nameof(must) + "' was not found.");
            return (T)_globalComponents[typeof(T)].Get();
        }

        public bool SetGlobal<T>(T obj) where T : struct
        {
            return _globalComponents.TryAdd(typeof(T), new Returner<T>(obj));
        }

    }
}
