using System;
using System.Collections.Generic;

namespace Security.RandomGen.IO
{
    public interface ISequenceOutputDevice
    {
        void Output(IEnumerable<int> sequence);
    }
}
