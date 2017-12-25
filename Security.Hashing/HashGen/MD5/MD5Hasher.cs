using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Hashing.HashGen.MD5.Transformation;
using Security.Hashing.Extensions;

namespace Security.Hashing.HashGen.MD5
{
    public class MD5Hasher : IHasher
    {
        private readonly MD5TransformationStep _transformationStep;

        private readonly uint[] _sinuses;
        private readonly int[] _shiftMap;

        public MD5Hasher()
        {
            _transformationStep = new MD5TransformationStep();

            _sinuses = new uint[] { 0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee }
                .Concat(new uint[] { 0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501 })
                .Concat(new uint[] { 0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be })
                .Concat(new uint[] { 0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821 })
                .Concat(new uint[] { 0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa })
                .Concat(new uint[] { 0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8 })
                .Concat(new uint[] { 0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed })
                .Concat(new uint[] { 0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a })
                .Concat(new uint[] { 0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c })
                .Concat(new uint[] { 0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70 })
                .Concat(new uint[] { 0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05 })
                .Concat(new uint[] { 0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665 })
                .Concat(new uint[] { 0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039 })
                .Concat(new uint[] { 0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1 })
                .Concat(new uint[] { 0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1 })
                .Concat(new uint[] { 0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391 })
                .ToArray();

            _shiftMap = new int[] { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22 }
                .Concat(new int[] { 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20 })
                .Concat(new int[] { 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23 })
                .Concat(new int[] { 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21 })
                .ToArray();
        }

        public string Hash(string inputString)
        {
            var inputBits = new BitArray(Encoding.ASCII.GetBytes(inputString));

            var paddedInput = CreatePaddedInput(inputBits);

            var buffer = CreateInitialBuffer();

            for (int chunkIndex = 0; chunkIndex < paddedInput.Length / 512; chunkIndex++)
            {
                var currentChuck = RetrieveBitChunk(paddedInput, chunkIndex);

                var currentChunkBuffer = buffer;

                for (int i = 0; i < 63; i++)
                {
                    var stepResult = _transformationStep.Transform(
                        i
                        , currentChunkBuffer.B
                        , currentChunkBuffer.C
                        , currentChunkBuffer.D);

                    var f = new BitArray(BitConverter.GetBytes((int)((
                            stepResult.TransformedArray.ToInt()
                            + currentChunkBuffer.A.ToInt()
                            + _sinuses[i]
                            + currentChuck.GetWord(stepResult.ChunkWordIndex).ToInt()
                            ) % (long)Math.Pow(2,32))
                        ));

                    var currentChunkBufferComputed = new MD5Buffer
                    {
                        A = currentChunkBuffer.D,
                        B = currentChunkBuffer.C,
                        C = new BitArray(BitConverter.GetBytes(
                                currentChunkBuffer.B.ToInt() + f.LeftRotate(_shiftMap[i]).ToInt()
                            )),
                        D = currentChunkBuffer.C,
                    };

                    buffer = buffer.Add(currentChunkBufferComputed);
                    currentChunkBuffer = currentChunkBufferComputed;
                }
            }

            return buffer.ToHexString();
        }

        private MD5Chunk RetrieveBitChunk(BitArray input, int chunkIndex)
        {
            return new MD5Chunk(input.SubSequence(chunkIndex * 512, (chunkIndex + 1) * 512));
        }

        private MD5Buffer CreateInitialBuffer()
        {
            return new MD5Buffer
            {
                A = new BitArray(BitConverter.GetBytes(0x67452301)),
                B = new BitArray(BitConverter.GetBytes(0xefcdab89)),
                C = new BitArray(BitConverter.GetBytes(0x98badcfe)),
                D = new BitArray(BitConverter.GetBytes(0x10325476)),
            };
        }

        private BitArray CreatePaddedInput(BitArray inputBits)
        {
            var paddingLength = 448 - (inputBits.Length % 512);

            if (paddingLength == 0)
            {
                paddingLength = 512;
            }

            var zeroSequence = Enumerable.Repeat(false, paddingLength - 1);

            var padding = new bool[] { true }.Concat(zeroSequence).ToArray();
            var messageLengthBits = new BitArray(BitConverter.GetBytes((long)inputBits.Length));

            var paddedInput = inputBits.Append(new BitArray(padding)).Append(messageLengthBits);

            return paddedInput;
        }
    }
}
