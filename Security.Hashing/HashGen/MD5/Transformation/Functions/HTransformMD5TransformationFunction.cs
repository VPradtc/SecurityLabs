using System.Collections;

namespace Security.Hashing.HashGen.MD5.Transformation
{
    public class HTransformMD5TransformationFunction : IMD5TransformationFunction
    {
        public BitArray Transform(BitArray b, BitArray c, BitArray d)
        {
            BitArray result = b.Xor(c).Xor(d);

            return result;
        }
    }
}
