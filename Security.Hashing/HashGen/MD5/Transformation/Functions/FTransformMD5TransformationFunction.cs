using System.Collections;

namespace Security.Hashing.HashGen.MD5.Transformation
{
    public class FTransformMD5TransformationFunction : IMD5TransformationFunction
    {
        public BitArray Transform(BitArray b, BitArray c, BitArray d)
        {
            BitArray result = (b.And(c)).Or(b.Not().And(d));

            return result;
        }
    }
}
