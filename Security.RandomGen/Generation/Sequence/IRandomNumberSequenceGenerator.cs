using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.RandomGen.Generation.Sequence
{
    public interface IRandomNumberSequenceGenerator
    {
        IEnumerable<int> Generate(int count);
    }
}
