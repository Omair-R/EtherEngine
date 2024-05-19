using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public class Xorwow : AbstractRandom
    {
        uint x = 123456789, y = 362436069, z = 521288629,
                   w = 88675123, v = 5783321;

        uint counter = 6615241;

        public override uint NextUInt32()
        {
            uint t = (x ^ (x >> 2));

            x = y;
            y = z;
            z = w;
            w = v;
            v = (v ^ (v << 4)) ^ (t ^ (t << 1));

            counter += 362437;
            return v + counter;
        }

        public override ulong NextUInt64() => (((ulong)NextUInt32()) << 32) | NextUInt32();
    }
}
