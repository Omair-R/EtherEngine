using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public class XorshirtStar64 : AbstractRandom
    {
        private ulong x = 1;
        public override ulong NextUInt64()
        {
            x ^= x >> 12;
            x ^= x >> 25;
            x ^= x >> 26;
            return x * 0x2545F4914F6CDD1DUL;    
        }
    }
}
