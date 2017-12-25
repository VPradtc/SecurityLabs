using System;
using System.Collections.Generic;
using System.Linq;
using Security.RandomGen.Initialization;

namespace Security.RandomGen.Generation.Sequence
{
    public class FiniteRandomNumberSequenceGenerator : IRandomNumberSequenceGenerator
    {
        private readonly IRandomNumberGenerator _generator;

        public FiniteRandomNumberSequenceGenerator(IRandomNumberGeneratorFactory factory)
        {
            _generator = factory.Create();
        }

        public IEnumerable<int> Generate(int count)
        {
            return Enumerable.Repeat(0, count).Select(i => _generator.Generate());
        }
    }
}
