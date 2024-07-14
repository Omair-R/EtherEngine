using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtherEngine.Components;

namespace EtherEngine.Managers
{
    //public sealed class EventManager //TODO: Dispose
    //{
    //    private Dictionary<Type, IEventBus> _buses = new Dictionary<Type, IEventBus>();

    //    public EventBus<T> RegisterEventBus<T>() where T : struct, IEvent  { 

    //        if (!_buses.ContainsKey(typeof(T))) 
    //            _buses.Add(typeof(T), new EventBus<T>());

    //        return (EventBus<T>)_buses[typeof(T)];
    //    }

    //    public EventBus<T> HandleEvent<T>() where T : struct, IEvent 
    //    {
    //        EventBus<T> bus = (EventBus <T>)_buses[typeof(T)];
    //        //_buses.Remove(typeof(T));
    //        return bus;
    //    }
    //}
}
