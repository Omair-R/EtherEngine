using System;
using System.Diagnostics;

namespace EtherUtils.Random
{
    public interface IRandom
    {
        public ulong Seed { get; set; }

        public void ResetInternalState();

        public uint NextUInt();
        public ulong NextULong();
        public uint NextUInt(uint maxValue);
        public ulong NextULong(ulong maxValue);
        public uint NextUInt(uint minValue, uint maxValue);
        public ulong NextULong(ulong minValue, ulong maxValue);


        public int NextInt();
        public long NextLong();
        public int NextInt(int maxValue);
        public long NextLong(long maxValue);
        public int NextInt(int minValue, int maxValue);
        public long NextLong(long minValue, long maxValue);


        public float NextFloat();
        public double NextDouble();
        public float NextFloat(float maxValue);
        public double NextDouble(double maxValue);
        public float NextFloat(float minValue, float maxValue);
        public double NextDouble(double minValue, double maxValue);

        public bool NextBool();

    }

    // Heavily adapted from the system Library.
    public abstract class AbstractRandom : IRandom
    {
        protected ulong _seed;
        public virtual ulong Seed
        {
            get => _seed;
            set
            {
                _seed = value;
                ResetInternalState();
            }
        }

        protected AbstractRandom(ulong? seed = null)
        {
            if (seed != null)
                Seed = (ulong)seed;
            else
                Seed = (ulong)DateTime.Now.Ticks;
        }

        public abstract void ResetInternalState();

        public virtual ulong NextULong() => (ulong)NextUInt() << 32 | NextUInt();

        public virtual uint NextUInt() => (uint)(NextULong() >> 32);

        #region Unsigned
        // Note from System library.
        // NextUInt32/64 algorithms based on https://arxiv.org/pdf/1805.10941.pdf and https://github.com/lemire/fastrange.
        public uint NextUInt(uint maxValue)
        {
            ulong randomProduct = (ulong)maxValue * NextUInt();
            uint lowPart = (uint)randomProduct;

            if (lowPart < maxValue)
            {
                uint remainder = (0u - maxValue) % maxValue;

                while (lowPart < remainder)
                {
                    randomProduct = (ulong)maxValue * NextUInt();
                    lowPart = (uint)randomProduct;
                }
            }

            return (uint)(randomProduct >> 32);
        }

        public uint NextUInt(uint minValue, uint maxValue)
        {
            Debug.Assert(minValue <= maxValue);

            return NextUInt(maxValue - minValue) + minValue;
        }

        public ulong NextULong(ulong maxValue)
        {
            ulong randomProduct = Math.BigMul(maxValue, NextULong(), out ulong lowPart);

            if (lowPart < maxValue)
            {
                ulong remainder = (0ul - maxValue) % maxValue;

                while (lowPart < remainder)
                    randomProduct = Math.BigMul(maxValue, NextULong(), out lowPart);
            }

            return randomProduct;
        }


        public ulong NextULong(ulong minValue, ulong maxValue)
        {
            Debug.Assert(minValue <= maxValue);

            return NextULong(maxValue - minValue) + minValue;
        }

        #endregion

        #region Signed
        public int NextInt()
        {
            ulong result;
            do
            {
                result = NextULong() >> 33;
            } while (result == int.MaxValue);

            return (int)result;
        }

        public int NextInt(int maxValue)
        {
            Debug.Assert(maxValue >= 0);

            return (int)NextUInt((uint)maxValue);
        }

        public int NextInt(int minValue, int maxValue)
        {
            Debug.Assert(minValue <= maxValue);

            return (int)NextUInt((uint)(maxValue - minValue)) + minValue;
        }

        public long NextLong()
        {
            ulong result;
            do
            {
                result = NextULong() >> 1;
            } while (result == int.MaxValue);

            return (long)result;
        }

        public long NextLong(long maxValue)
        {
            Debug.Assert(maxValue >= 0);

            return (long)NextULong((ulong)maxValue);
        }

        public long NextLong(long minValue, long maxValue)
        {
            Debug.Assert(minValue <= maxValue);

            return (long)NextULong((ulong)(maxValue - minValue)) + minValue;
        }

        #endregion

        #region Decimal
        public float NextFloat() => NextUInt() / (float)uint.MaxValue;

        public float NextFloat(float maxValue)
        {
            Debug.Assert(maxValue >= 0f);

            return NextFloat() * maxValue;
        }

        public float NextFloat(float minValue, float maxValue)
        {
            Debug.Assert(maxValue >= minValue);

            return NextFloat(maxValue - minValue) + minValue;
        }

        public double NextDouble() => NextULong() / (double)ulong.MaxValue;

        public double NextDouble(double maxValue)
        {
            Debug.Assert(maxValue >= 0d);

            return NextDouble() * maxValue;
        }

        public double NextDouble(double minValue, double maxValue)
        {
            Debug.Assert(maxValue >= minValue);

            return NextDouble(maxValue - minValue) + minValue;
        }

        #endregion

        public bool NextBool()
        {
            return NextULong() % 2 != 0;
        }


    }
}
