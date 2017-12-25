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

        private readonly int[] _sinuses;
        private readonly int[] _shiftMap;

        public MD5Hasher()
        {
            _transformationStep = new MD5TransformationStep();

            _sinuses = Enumerable.Range(0, 64)
                .Select(i => (int)Math.Floor(Math.Pow(2, 32) * Math.Abs(Math.Sin(i + 1))))
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

                    var f = new BitArray(BitConverter.GetBytes(
                            stepResult.TransformedArray.ToInt()
                            + currentChunkBuffer.A.ToInt()
                            + _sinuses[i]
                            + currentChuck.GetWord(stepResult.ChunkWordIndex).ToInt()
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
            var messageLengthBits = BitConverter.GetBytes(inputBits.Length);

            var paddedInput = inputBits.Append(new BitArray(padding)).Append(new BitArray(messageLengthBits));

            return paddedInput;
        }
    }
}
