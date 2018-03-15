using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodingHelmet.Random
{
    internal class RandomBits
    {
        public uint Current { get; set; }

        private IEnumerator<byte> BytesEnumerator { get; }
        private ulong Buffer { get; set; }
        private int BufferedBits { get; set; }

        public RandomBits()
        {
            this.BytesEnumerator = new RandomBytesSequence().GetEnumerator();
        }

        public void MoveNext(ulong range) => 
            MoveBits(this.RequiredBitsFor(range));

        public void MoveBits(int bitsCount)
        {
            this.PopulateBuffer(bitsCount);
            this.Current = this.GetChunk(bitsCount);
            this.PurgeBuffer(bitsCount);
        }

        public int RequiredBitsFor(ulong range) =>
            Enumerable.Range(0, 64).SkipWhile(i => (1UL << i) <= range).First();

        private void PopulateBuffer(int bitsCount)
        {
            while (this.BufferedBits < bitsCount)
            {
                this.BytesEnumerator.MoveNext();
                this.Buffer = ((ulong)this.BytesEnumerator.Current) << this.BufferedBits;
                this.BufferedBits += 8;
            }
        }

        private uint GetChunk(int bitsCount) =>
            this.GetChunkFromMask((1UL << bitsCount) - 1);

        private uint GetChunkFromMask(ulong mask) =>
            (uint)(this.Buffer & mask);

        private void PurgeBuffer(int bitsCount)
        {
            this.Buffer >>= bitsCount;
            this.BufferedBits -= bitsCount;
        }
    }
}
