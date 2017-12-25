using System;
using System.Collections;
using System.Text;

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

        public static byte[] ToByteArray(this BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

        public static string ToDebugHexString(this BitArray bits)
        {
            StringBuilder sb = new StringBuilder(bits.Length / 4);

            for (int i = 0; i < bits.Length; i += 4)
            {
                int v = (bits[i] ? 8 : 0) |
                        (bits[i + 1] ? 4 : 0) |
                        (bits[i + 2] ? 2 : 0) |
                        (bits[i + 3] ? 1 : 0);

                if (i % 16 == 0)
                {
                    sb.Append(" ");
                }

                sb.Append(v.ToString("X1"));
            }

           return sb.ToString();
        }
    }
}
