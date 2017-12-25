using System.Collections;

namespace Security.Hashing.HashGen.MD5.Transformation
{
    public class ITransformMD5TransformationFunction : IMD5TransformationFunction
    {
        public BitArray Transform(BitArray b, BitArray c, BitArray d)
        {
            BitArray result = c.Xor((b.Or(d.Not())));

            return result;
        }
    }
}
