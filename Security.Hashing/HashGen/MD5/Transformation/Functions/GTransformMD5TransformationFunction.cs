using System.Collections;

namespace Security.Hashing.HashGen.MD5.Transformation
{
    public class GTransformMD5TransformationFunction : IMD5TransformationFunction
    {
        public BitArray Transform(BitArray b, BitArray c, BitArray d)
        {
            BitArray result = (b.And(d)).Or(c.And(d.Not()));

            return result;
        }
    }
}
