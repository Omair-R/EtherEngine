using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public static class RandomUtils
    {
        public static uint _Rotr32(uint x, int r)
        {
            return x >> r | x << (-r & 31);
        }

        public static ulong _Rotr64(ulong x, int r)
        {
            return x >> r | x << (-r & 63);
        }

        public static uint _Rotl32(uint x, int r)
        {
            return (x << r) | (x >> (32 - r));
        }

        public static ulong _Rotl64(ulong x, int r) {
	        return (x << r) | (x >> (64 - r));
        }
    }
}
