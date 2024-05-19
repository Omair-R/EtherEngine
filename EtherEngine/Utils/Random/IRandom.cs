using System;
using System.Diagnostics;

namespace EtherEngine.Utils.Random
{
    public interface IRandom
    {
        public uint NextUInt32();
        public ulong NextUInt64();
        public int Next();
        public long NextInt64();
        public uint NextUInt32(uint maxValue);
        public ulong NextUInt64(ulong maxValue);
    }

    // Heavily adapted from the system Library.
    public abstract class AbstractRandom : IRandom
    {

        public abstract ulong NextUInt64();

        public virtual uint NextUInt32() => (uint)(NextUInt64() >> 32);

        public int Next()
        {
            ulong result;
            do
            {
                result = NextUInt64() >> 33;
            } while (result == int.MaxValue);

            return (int)result;
        }

        public long NextInt64()
        {
            ulong result;
            do
            {
                result = NextUInt64() >> 1;
            } while (result == int.MaxValue);

            return (long)result;
        }

        // Note from System library.
        // NextUInt32/64 algorithms based on https://arxiv.org/pdf/1805.10941.pdf and https://github.com/lemire/fastrange.
        public uint NextUInt32(uint maxValue)
        {
            ulong randomProduct = (ulong)maxValue * NextUInt32();
            uint lowPart = (uint)randomProduct;

            if (lowPart < maxValue)
            {
                uint remainder = (0u - maxValue) % maxValue;

                while (lowPart < remainder)
                {
                    randomProduct = (ulong)maxValue * NextUInt32();
                    lowPart = (uint)randomProduct;
                }
            }

            return (uint)(randomProduct >> 32);
        }

        public ulong NextUInt64(ulong maxValue)
        {
            ulong randomProduct = Math.BigMul(maxValue, NextUInt64(), out ulong lowPart);

            if (lowPart < maxValue)
            {
                ulong remainder = (0ul - maxValue) % maxValue;

                while (lowPart < remainder)
                    randomProduct = Math.BigMul(maxValue, NextUInt64(), out lowPart);
            }

            return randomProduct;
        }

        public int Next(int maxValue)
        {
            Debug.Assert(maxValue >= 0);

            return (int)NextUInt32((uint)maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            Debug.Assert(minValue <= maxValue);

            return (int)NextUInt32((uint)(maxValue - minValue)) + minValue;
        }

        public long NextInt64(long maxValue)
        {
            Debug.Assert(maxValue >= 0);

            return (long)NextUInt64((ulong)maxValue);
        }

        public long NextInt64(long minValue, long maxValue)
        {
            Debug.Assert(minValue <= maxValue);

            return (long)NextUInt64((ulong)(maxValue - minValue)) + minValue;
        }

        public double NextDouble() => (NextUInt64() >> 11) * (1.0 / (1ul << 53));

        public  float NextSingle() => (NextUInt64() >> 40) * (1.0f / (1u << 24));

        public double NextDouble(double maxValue)
        {
            Debug.Assert(maxValue >= 0d);

            return NextDouble() * maxValue;
        }

        public float NextSingle(float maxValue)
        {
            Debug.Assert(maxValue >= 0f);

            return NextSingle() * maxValue;
        }


    }
}
