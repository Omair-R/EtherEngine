﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils.Random
{
    public class SplitMix64 : AbstractRandom
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

        public SplitMix64(ulong? seed=null)
        {
            if (seed != null)
                Seed = (ulong)seed;
            else
                Seed = (ulong)DateTime.Now.Ticks;
        }

        public override void ResetInternalState() => _x = _seed;

        public override uint NextUInt() => (uint)(NextULong() >> 32);

        public override ulong NextULong()
        {
            ulong tx = (_x += 0x9e3779b97f4a7c15);
            tx = (tx ^ (tx >> 30)) * 0xbf58476d1ce4e5b9;
            tx = (tx ^ (tx >> 27)) * 0x94d049bb133111eb;
            return tx ^ (tx >> 31);
        }

        
    }
}
