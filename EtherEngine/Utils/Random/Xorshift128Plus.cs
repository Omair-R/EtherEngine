using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public class Xorshift128Plus : AbstractRandom
    {
        private ulong x, y;

        // Implementation of the xorshift128+
        public override ulong NextUInt64()
        {
            ulong tx = x;
            ulong ty = y;

            x = ty;

            tx ^= tx << 23;
            tx ^= tx >> 18;
            tx ^= ty ^ (ty >> 5);

            y = tx;

            return tx+ty;
        }
    }
}
