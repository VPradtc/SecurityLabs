using System;

namespace Security.RandomGen.Initialization
{
    public interface IRandomNumberGeneratorFactory
    {
        IRandomNumberGenerator Create();
    }
}
