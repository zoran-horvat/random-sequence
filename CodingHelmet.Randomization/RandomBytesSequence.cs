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
        private Random Random{ get; }

        public RandomBytesSequence()
        {
            this.Buffer = new byte[BufferLength];
            this.Random =  RandomNumbers.SeedRandom();
        }
        public IEnumerator<byte> GetEnumerator()
        {
            while (true)
            {
                if (this.Remaining == 0)
                {
                    this.Random.NextBytes(this.Buffer);
                    this.Remaining = this.Buffer.Length;
                }

                this.Remaining -= 1;
                yield return this.Buffer[this.Remaining];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
