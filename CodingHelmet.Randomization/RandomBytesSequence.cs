using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace CodingHelmet.Randomization
{
    public class RandomBytesSequence : IEnumerable<byte>
    {
        private readonly int BufferLength = 16;
        private byte[] Buffer { get; }
        private int Remaining = 0;
        private Random RandomNumbers { get; }

        public RandomBytesSequence()
        {
            this.Buffer = new byte[BufferLength];
            int seed = this.GenerateSeed();
            this.RandomNumbers = new Random(seed);
        }

        private int GenerateSeed() =>
            this.GenerateSeedBytes(new byte[4])
                .Aggregate(0, (seed, cur) => (seed << 8) | cur);

        private byte[] GenerateSeedBytes(byte[] buffer)
        {
            RandomNumberGenerator.Create().GetBytes(buffer);
            return buffer;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            while (true)
            {
                if (this.Remaining == 0)
                {
                    this.RandomNumbers.NextBytes(this.Buffer);
                    this.Remaining = this.Buffer.Length;
                }

                this.Remaining -= 1;
                yield return this.Buffer[this.Remaining];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
