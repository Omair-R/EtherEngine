using System;

namespace EtherUtils
{
    public static class EventUtils
    {
        public static void Invoke<TEventArgs>(EventHandler<TEventArgs> handler, object sender, TEventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void Invoke(EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
