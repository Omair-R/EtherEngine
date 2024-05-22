using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    
    public class PCG : AbstractRandom
    {
        private ulong _x;

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

        public PCG(ulong? seed = null)
        {
            if (seed != null)
                Seed = (ulong)seed;
            else
                Seed = (ulong)DateTime.Now.Ticks;
        }

        public override void ResetInternalState()
        {
            _x = 0ul;
            NextUInt();
            _x += _seed;
            NextUInt();
        }

        private uint _Rotr32(uint x, int r)
        {
            return x >> r | x << (-r & 31);
        }

        public override uint NextUInt()
        {
            ulong t = _x;
            int s = (int)(_x >> 59);

            _x = unchecked(t * 6364136223846793005u + 1442695040888963407u);
            t ^= t >> 18;

            return _Rotr32((uint)(t >> 27), s);
        }

        public override ulong NextULong() => (((ulong)NextUInt()) << 32) | NextUInt();

    }
}
