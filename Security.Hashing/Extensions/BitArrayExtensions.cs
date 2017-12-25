using System.Collections;

namespace Security.Hashing.Extensions
{
    public static class BitArrayExtensions
    {
        public static BitArray Append(this BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }

        public static BitArray SubSequence(this BitArray input, int startIndex, int endIndex)
        {
            var chunk = new BitArray(endIndex - startIndex);
            var currentChunkElementIndex = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                chunk[currentChunkElementIndex] = input[i];
                currentChunkElementIndex++;
            }

            return chunk;
        }
    }
}
