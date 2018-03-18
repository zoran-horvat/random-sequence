using System;
using System.Linq;
using System.Security.Cryptography;

namespace CodingHelmet.Randomization
{
    public class RandomNumbers
    {
        public static Random SeedRandom() => new Random(GenerateSeed());

        private static int GenerateSeed() =>
            GenerateSeedBytes(new byte[4])
                .Aggregate(0, (seed, cur) => (seed << 8) | cur);

        private static byte[] GenerateSeedBytes(byte[] buffer)
        {
            RandomNumberGenerator.Create().GetBytes(buffer);
            return buffer;
        }
    }
}
