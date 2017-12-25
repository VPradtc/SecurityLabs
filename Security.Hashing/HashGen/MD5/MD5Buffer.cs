using System;
using System.Collections;
using Security.Hashing.Extensions;

namespace Security.Hashing.HashGen.MD5
{
    public class MD5Buffer
    {
        public BitArray A { get; set; }
        public BitArray B { get; set; }
        public BitArray C { get; set; }
        public BitArray D { get; set; }

        public MD5Buffer Add(MD5Buffer other)
        {
            var sum = new MD5Buffer
            {
                A = new BitArray(BitConverter.GetBytes(this.A.ToInt() + other.A.ToInt())),
                B = new BitArray(BitConverter.GetBytes(this.B.ToInt() + other.B.ToInt())),
                C = new BitArray(BitConverter.GetBytes(this.C.ToInt() + other.C.ToInt())),
                D = new BitArray(BitConverter.GetBytes(this.D.ToInt() + other.D.ToInt())),
            };

            return sum;
        }

        public string ToHexString()
        {
            return String.Concat(
                A.ToInt().ToString("X2"),
                B.ToInt().ToString("X2"),
                C.ToInt().ToString("X2"),
                D.ToInt().ToString("X2")
            );
        }
    }
}
