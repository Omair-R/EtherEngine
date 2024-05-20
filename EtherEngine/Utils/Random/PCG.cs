using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    
    public class PCG : AbstractRandom
    {
        private ulong x = 0x4d595df4d0f33173;

        public uint rotr32(uint x, int r)
        {
            return x >> r | x << (-r & 31);
        }

        public override uint NextUInt()
        {
            ulong t = x;
            int s = (int)(x >> 59);

            x = t * 6364136223846793005u + 1442695040888963407u;
            t ^= t >> 18;

            return rotr32((uint)(t >> 27), s);
        }

        public override ulong NextULong() => (((ulong)NextUInt()) << 32) | NextUInt();
    }
}
