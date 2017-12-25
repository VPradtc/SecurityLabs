using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Hashing.Extensions

namespace Security.Hashing.HashGen.MD5
{
    public class MD5Word
    {
        private BitArray _word;

        public MD5Word(BitArray word)
        {
            this._word = word;
        }

        public BitArray A
        {
            get
            {
                return _word.SubSequence(0, 16);
            }
        }

        public BitArray B
        {
            get
            {
                return _word.SubSequence(16, 32);
            }
        }

        public BitArray C
        {
            get
            {
                return _word.SubSequence(32, 48);
            }
        }

        public BitArray D
        {
            get
            {
                return _word.SubSequence(48, 64);
            }
        }
    }
}
