using EtherEngine.Collision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils
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
