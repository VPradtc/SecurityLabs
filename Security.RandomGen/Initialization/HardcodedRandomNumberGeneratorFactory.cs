using System;

namespace Security.RandomGen.Initialization
{
    public class HardcodedRandomNumberGeneratorFactory : IRandomNumberGeneratorFactory
    {
        public IRandomNumberGenerator Create()
        {
            int m = (int)Math.Pow(2, 10) - 1;
            int a = (int)Math.Pow(2, 5);
            int c = 0;
            int x0 = 2;

            var generator = new LemerRandomNumberGenerator(
                    m
                    ,a
                    ,c
                    ,x0
                );

            return generator;
        }
    }
}
