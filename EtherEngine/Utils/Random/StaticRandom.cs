using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public static class StaticRandom
    {
        static public AbstractRandom Random { get; set; } = new Xorshift128Plus();
    }
}
