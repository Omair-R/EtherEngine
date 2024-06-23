using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherUtils.Random
{
    #region XoroshiroLong
    public abstract class XoroshiroLong : AbstractRandom
    {
        protected ulong _x, _y;

        public XoroshiroLong(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            SplitMix64 splitMix64 = new SplitMix64(_seed);
            _x = splitMix64.NextULong();
            _y = splitMix64.NextULong();
        }
    }
    public class Xoroshiro128Plus : XoroshiroLong
    {

        public Xoroshiro128Plus(ulong? seed = null) : base(seed) { }

        public override ulong NextULong()
        {

            ulong tx = _x;
            ulong ty = _y;
            ulong result = tx + ty;

            ty ^= tx;
            _x = RandomUtils._Rotl64(tx, 24) ^ ty ^ ty << 16;
            _y = RandomUtils._Rotl64(ty, 37);

            return result;
        }
    }

    public class Xoroshiro128PlusPlus : XoroshiroLong
    {

        public Xoroshiro128PlusPlus(ulong? seed = null) : base(seed) { }


        public override ulong NextULong()
        {
            ulong tx = _x;
            ulong ty = _y;

            ulong result = RandomUtils._Rotl64(tx + ty, 17) + tx;

            ty ^= tx;
            _x = RandomUtils._Rotl64(tx, 49) ^ ty ^ ty << 21;
            _y = RandomUtils._Rotl64(ty, 28);

            return result;
        }
    }

    public class Xoroshiro128StarStar : XoroshiroLong
    {

        public Xoroshiro128StarStar(ulong? seed = null) : base(seed) { }


        public override ulong NextULong()
        {
            ulong tx = _x;
            ulong ty = _y;

            ulong result = RandomUtils._Rotl64(tx * 5, 7) * 9;

            ty ^= tx;
            _x = RandomUtils._Rotl64(tx, 24) ^ ty ^ ty << 16;
            _y = RandomUtils._Rotl64(ty, 37);

            return result;
        }
    }
    #endregion

    #region XoroshiroShort

    public abstract class XoroshiroShort : AbstractRandom
    {
        protected uint _x, _y;

        public XoroshiroShort(ulong? seed = null) : base(seed) { }

        public override void ResetInternalState()
        {
            SplitMix64 splitMix64 = new SplitMix64(_seed);
            _x = splitMix64.NextUInt();
            _y = splitMix64.NextUInt();
        }
    }
    public class Xoroshiro64Star : XoroshiroShort
    {

        public Xoroshiro64Star(ulong? seed = null) : base(seed) { }

        public override uint NextUInt()
        {
            uint tx = _x;
            uint ty = _y;
            uint result = tx * 0x9E3779BB;

            ty ^= tx;
            _x = RandomUtils._Rotl32(tx, 26) ^ ty ^ ty << 9;
            _y = RandomUtils._Rotl32(ty, 12);

            return result;
        }
    }

    public class Xoroshiro64StarStar : XoroshiroShort
    {
        public Xoroshiro64StarStar(ulong? seed = null) : base(seed) { }

        public override uint NextUInt()
        {
            uint tx = _x;
            uint ty = _y;
            uint result = RandomUtils._Rotl32(tx * 0x9E3779BB, 5) * 5;

            ty ^= tx;
            _x = RandomUtils._Rotl32(tx, 26) ^ ty ^ ty << 9;
            _y = RandomUtils._Rotl32(ty, 12);

            return result;
        }
    }
    #endregion
}
