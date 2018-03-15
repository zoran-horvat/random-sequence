using System;
using System.Collections;
using System.Collections.Generic;

namespace CodingHelmet.Random
{
    public class RandomNumbersSequence : IEnumerable<int>
    {
        private int NoLessThan { get; }
        private ulong Range { get; }
        private int BitsPerChunk { get; }
        private RandomBits Bits { get; }

        public RandomNumbersSequence(int lowerInclusive, int upperInclusive)
        {
            if (lowerInclusive > upperInclusive)
                throw new ArgumentException("Lower inclusive boundary must not exceed upper inclusive boundary.");

            this.NoLessThan = lowerInclusive;

            this.Bits = new RandomBits();

            this.Range = (ulong)((long)upperInclusive - lowerInclusive + 1);
            this.BitsPerChunk = this.Bits.RequiredBitsFor(this.Range);
        }

        public IEnumerator<int> GetEnumerator()
        {
            while (true)
            {
                this.Bits.MoveBits(this.BitsPerChunk);
                if (this.Bits.Current >= this.Range)
                    continue;

                yield return (int)((long)this.Bits.Current + this.NoLessThan);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
