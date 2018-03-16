﻿namespace CodingHelmet.Random
{
    public class RandomNumbers
    {
        public int Current { get; private set; }
        private RandomBits Bits { get; } = new RandomBits();

        public void MoveNext(int lowerInclusive, int upperInclusive)
        {
            ulong range = (ulong)((long)upperInclusive - lowerInclusive + 1);
            do
            {
                this.Bits.MoveNext(range);
            }
            while (this.Bits.Current >= range);

            this.Current = (int)(this.Bits.Current + lowerInclusive);
        }
    }
}
