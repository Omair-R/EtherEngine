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
        private ulong _x, _y;
        private ulong _seed;
        public override ulong Seed
        {
            get => _seed;
            set
            {
                _seed = value;
                ResetInternalState();
            }
        }

        public Xorshift128Plus(ulong? seed = null)
        {
            if (seed != null)
                Seed = (ulong)seed;
            else
                Seed = (ulong)DateTime.Now.Ticks;
        }

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

            return tx+ty;
        }

    }
}
