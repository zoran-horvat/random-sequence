using System;
using System.Collections;
using System.Collections.Generic;

namespace CodingHelmet.Random
{
    public class RandomBitsSequence : IEnumerable<uint>
    {
        private IEnumerator<byte> BytesEnumerator { get; }
        private ulong Buffer { get; set; }
        private int BufferedBits { get; set; }
        private int BitsPerChunk { get; }
        private ulong ChunkMask { get; }

        public RandomBitsSequence(int bitsPerChunk)
        {
            if (bitsPerChunk <= 0 || bitsPerChunk > 32)
                throw new ArgumentException("RandomBitsSequence requires 1-32 bits per chunk, inclusive.");

            this.BitsPerChunk = bitsPerChunk;
            this.ChunkMask = ((1UL) << bitsPerChunk) - 1;
            this.BytesEnumerator = new RandomBytesSequence().GetEnumerator();
        }

        public IEnumerator<uint> GetEnumerator()
        {
            while (true)
            {
                this.PopulateBuffer();
                yield return this.Chunk;
                this.PurgeBuffer();
            }
        }

        private void PopulateBuffer()
        {
            while (this.BufferedBits < this.BitsPerChunk)
            {
                this.BytesEnumerator.MoveNext();
                this.Buffer = ((ulong)this.BytesEnumerator.Current) << this.BufferedBits;
                this.BufferedBits += 8;
            }
        }

        private uint Chunk => (uint)(this.Buffer & this.ChunkMask);

        private void PurgeBuffer()
        {
            this.Buffer >>= this.BitsPerChunk;
            this.BufferedBits -= this.BitsPerChunk;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
