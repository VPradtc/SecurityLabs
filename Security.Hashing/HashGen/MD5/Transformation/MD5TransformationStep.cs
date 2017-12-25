using System;
using System.Collections;
using System.Collections.Generic;

namespace Security.Hashing.HashGen.MD5.Transformation
{
    public class MD5TransformationStep
    {
        private readonly LinkedList<Tuple<IMD5TransformationFunction, Func<int, int>>> _functions;

        private LinkedListNode<Tuple<IMD5TransformationFunction, Func<int, int>>> _currentFunction;
        private int _currentFunctionInvocationCount;

        public MD5TransformationStep()
        {
            var functionList = new Tuple<IMD5TransformationFunction, Func<int, int>>[]
            {
                new Tuple<IMD5TransformationFunction, Func<int, int>>(
                    new FTransformMD5TransformationFunction(),
                    (int i) => i
                ),
                new Tuple<IMD5TransformationFunction, Func<int, int>>(
                    new GTransformMD5TransformationFunction(),
                    (int i) => (5 * i + 1) % 16
                ),
                new Tuple<IMD5TransformationFunction, Func<int, int>>(
                    new HTransformMD5TransformationFunction(),
                    (int i) => (3 * i + 5) % 16
                ),
                new Tuple<IMD5TransformationFunction, Func<int, int>>(
                    new ITransformMD5TransformationFunction(),
                    (int i) => (7 * i) % 16
                ),
            };

            _functions = new LinkedList<Tuple<IMD5TransformationFunction, Func<int, int>>>(functionList);

            _currentFunction = _functions.First;
            _currentFunctionInvocationCount = 0;
        }

        public MD5TransformationStepResult Transform(int index, BitArray b, BitArray c, BitArray d)
        {
            var transformedArray = _currentFunction.Value.Item1.Transform(b, c, d);
            var chunkWordIndex = _currentFunction.Value.Item2.Invoke(index);

            _currentFunctionInvocationCount++;

            if (_currentFunctionInvocationCount % 16 == 0)
            {
                _currentFunction = _currentFunction.Next ?? _functions.First;

                if (_currentFunctionInvocationCount == 64)
                {
                    _currentFunctionInvocationCount = 0;
                }
            }

            var result = new MD5TransformationStepResult
            {
                TransformedArray = transformedArray,
                ChunkWordIndex = chunkWordIndex,
            };

            return result;
        }
    }
}
