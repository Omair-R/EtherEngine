using EtherUtils.Pattern;

namespace EtherUtils.Random
{
    public enum RandomMethods
    {
        PCG,
        SplitMix64,
        Xoroshiro64Star,
        Xoroshiro64StarStar,
        Xoroshiro128Plus,
        Xoroshiro128PlusPlus,
        Xoroshiro128StarStar,
        Xorshift64Star,
        Xorshift128Plus,
        Xorwow,
    }

    public class RandomSinglton : LazySingleton<RandomSinglton>
    {
        public ulong Seed { get => Randomizer.Seed; set => Randomizer.Seed = value; }

        public AbstractRandom Randomizer { get; private set; }

        private RandomSinglton()
        {
            Randomizer = new PCG();
        }

        public void ReselectRandomizer(RandomMethods randomMethods, ulong seed)
        {
            Randomizer = randomMethods switch
            {
                RandomMethods.PCG => new PCG(seed),
                RandomMethods.SplitMix64 => new SplitMix64(seed),
                RandomMethods.Xoroshiro64Star => new Xoroshiro64Star(seed),
                RandomMethods.Xoroshiro64StarStar => new Xoroshiro128Plus(seed),
                RandomMethods.Xoroshiro128Plus => new Xoroshiro128Plus(seed),
                RandomMethods.Xoroshiro128PlusPlus => new Xoroshiro128PlusPlus(seed),
                RandomMethods.Xoroshiro128StarStar => new Xoroshiro128StarStar(seed),
                RandomMethods.Xorshift64Star => new Xorshift64Star(seed),
                RandomMethods.Xorshift128Plus => new Xorshift128Plus(seed),
                _ => Randomizer,
            };
        }
    }
}
