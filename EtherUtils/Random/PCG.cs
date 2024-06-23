using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherUtils.Random
{

    public class PCG : AbstractRandom
    {
        private ulong _x;

        public PCG(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            _x = 0ul;
            NextUInt();
            _x += _seed;
            NextUInt();
        }

        public override uint NextUInt()
        {
            ulong t = _x;
            int rot = (int)(_x >> 59);

            _x = unchecked(t * 6364136223846793005u + 1442695040888963407u);
            t ^= t >> 18;

            return RandomUtils._Rotr32((uint)(t >> 27), rot);
        }

    }
}
