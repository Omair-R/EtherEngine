using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public class Xoroshiro128Plus : AbstractRandom
    {
        private ulong _x, _y;

        public Xoroshiro128Plus(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            SplitMix64 splitMix64 = new SplitMix64(_seed);
            _x = splitMix64.NextULong();
            _y = splitMix64.NextULong();
        }

        public override uint NextUInt() => (uint)(NextULong() >> 32);

        // Implementation of the xorshift128+
        public override ulong NextULong()
        {
            ulong tx = _x;
            ulong ty = _y;

            _x = ty;

            tx ^= tx << 23;
            tx ^= tx >> 18;
            tx ^= ty ^ (ty >> 5);

            _y = tx;

            const ulong tx = s[0];
            ulong ty = s[1];
            const ulong result = tx + ty;

            ty ^= tx;
            s[0] = rotl(tx, 24) ^ ty ^ (ty << 16); // a, b
            s[1] = rotl(ty, 37); // c

            return tx + ty;
        }
    }
}
