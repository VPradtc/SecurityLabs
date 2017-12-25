using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Hashing.HashGen.MD5
{
    public class MD5TransformationStepResult
    {
        public int ChunkWordIndex { get; set; }
        public BitArray TransformedArray { get; set; }
    }
}
