using System;
using System.Collections;
using System.Collections.Generic;

namespace CodingHelmet.Random
{
    public class RandomNumbersSequence : IEnumerable<int>
    {
        private int NoLessThan { get; }
        private int NoGreaterThan { get; }
        private ulong Range { get; }
        private IEnumerator<uint> Bits { get; }

        public RandomNumbersSequence(int lowerInclusive, int upperInclusive)
        {
            if (lowerInclusive > upperInclusive)
                throw new ArgumentException("Lower inclusive boundary must not exceed upper inclusive boundary.");

            this.NoLessThan = lowerInclusive;
            this.NoGreaterThan = upperInclusive;
            this.Range = (ulong)((long)upperInclusive - lowerInclusive + 1);

            this.Bits = new RandomBitsSequence(this.RequiredBitsCount).GetEnumerator();
        }

        private int RequiredBitsCount
        {
            get
            {
                int bitsPerChunk = 0;
                while ((1UL << bitsPerChunk) < this.Range)
                {
                    bitsPerChunk += 1;
                }
                return bitsPerChunk;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            while (true)
            {
                this.Bits.MoveNext();
                if (this.Bits.Current >= this.Range)
                    continue;

                yield return (int)((long)this.Bits.Current + this.NoLessThan);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
