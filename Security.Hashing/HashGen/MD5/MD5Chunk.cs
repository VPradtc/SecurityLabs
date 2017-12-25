using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Hashing.Extensions

namespace Security.Hashing.HashGen.MD5
{
    public class MD5Chunk
    {
        private BitArray _chunk;

        public MD5Chunk(BitArray chunk)
        {
            this._chunk = chunk;
        }

        public MD5Word GetWord(int index)
        {
            return new MD5Word(_chunk.SubSequence(index, index + 32));
        }
    }
}
