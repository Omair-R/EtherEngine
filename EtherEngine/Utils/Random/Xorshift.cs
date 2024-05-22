using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public class XorshiftPlus128 : AbstractRandom
    {
        private ulong _x, _y;

        public XorshiftPlus128(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            SplitMix64 splitMix64 = new SplitMix64(_seed);
            _x = splitMix64.NextULong();
            _y = splitMix64.NextULong();
        }

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

            return tx+ty;
        }

    }


    public class XorshirtStar64 : AbstractRandom
    {
        private ulong _x;

        public XorshirtStar64(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            SplitMix64 splitMix64 = new SplitMix64(_seed);
            _x = splitMix64.NextULong();
        }

        public override ulong NextULong()
        {
            _x ^= _x >> 12;
            _x ^= _x >> 25;
            _x ^= _x >> 26;
            return unchecked(_x * 0x2545F4914F6CDD1DUL);
        }
    }



}
