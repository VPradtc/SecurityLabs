using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Hashing.HashGen.MD5
{
    public interface IMD5TransformationFunction
    {
        BitArray Transform(BitArray b, BitArray c, BitArray d);
    }
}
