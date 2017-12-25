using System.Collections;
using System.Collections.Generic;

namespace Security.Hashing.HashGen.MD5.Transformation
{
    public class CycleMD5TransformationFunction : IMD5TransformationFunction
    {
        private readonly LinkedList<IMD5TransformationFunction> _functions;
        private LinkedListNode<IMD5TransformationFunction> _currentFunction;
        private int _currentFunctionInvocationCount;

        public CycleMD5TransformationFunction()
        {
            var functionList = new IMD5TransformationFunction[]
            {
                new FTransformMD5TransformationFunction(),
                new GTransformMD5TransformationFunction(),
                new HTransformMD5TransformationFunction(),
                new ITransformMD5TransformationFunction(),
            };

            _functions = new LinkedList<IMD5TransformationFunction>(functionList);

            _currentFunction = _functions.First;
            _currentFunctionInvocationCount = 0;
        }

        public BitArray Transform(BitArray b, BitArray c, BitArray d)
        {
            var result = _currentFunction.Value.Transform(b, c, d);

            _currentFunctionInvocationCount++;

            if (_currentFunctionInvocationCount == 16)
            {
                _currentFunction = _currentFunction.Next ?? _functions.First;
                _currentFunctionInvocationCount = 0;
            }

            return result;
        }
    }
}
