using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherUtils.Random
{
    public class Xorwow : AbstractRandom
    {
        uint _x, _y = 362436069, _z = 521288629,
                   _w = 88675123, _v = 5783321;

        uint counter = 6615241;

        public Xorwow(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            SplitMix64 splitMix64 = new SplitMix64(_seed);
            _x = (uint)(splitMix64.NextULong() >> 32);
            _x += _x % 2u == 0u ? 1u : 0u; //making sure the state is not even.
        }
        public override uint NextUInt()
        {
            uint t = _x ^ _x >> 2;

            _x = _y;
            _y = _z;
            _z = _w;
            _w = _v;
            _v = _v ^ _v << 4 ^ t ^ t << 1;

            counter += 362437;
            return _v + counter;
        }

    }
}
