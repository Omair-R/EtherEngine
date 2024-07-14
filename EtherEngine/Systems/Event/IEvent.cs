using Arch.Core;
using EtherEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Event
{
    public interface IEvent
    {
        Entity Sender { get; }
    }

    //public interface IEventBus
    //{
    //}

    //public sealed class EventBus<T> : IEventBus where T : struct, IEvent
    //{
    //    internal Queue<T> Events { get; } = new Queue<T>();

    //    public void Sent(T eventComponent)
    //    {
    //        Events.Enqueue(eventComponent);
    //    }

    //    public IEnumerable<T> Recieve()
    //    {
    //        if (Events.Count == 0) yield break;
    //        yield return Events.Dequeue();
    //    }
    //}
}
