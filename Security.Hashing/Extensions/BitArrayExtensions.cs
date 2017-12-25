using System;
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

        public static BitArray LeftRotate(this BitArray array, int shiftAmount)
        {
            var intValue = array.ToInt();

            var shiftedInt = intValue << shiftAmount;

            return new BitArray(BitConverter.GetBytes(shiftedInt));
        }

        public static int ToInt(this BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }
    }
}
