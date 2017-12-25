using System;

namespace Security.RandomGen
{
    public class LemerRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly int _modulo;
        private readonly int _multiplier;
        private readonly int _increment;
        private readonly int _seed;

        private int _lastSeedValue;

        public LemerRandomNumberGenerator(
            int modulo
            , int multiplier
            , int increment
            , int seed
            )
        {
            _modulo = modulo;
            _multiplier = multiplier;
            _increment = increment;
            _seed = seed;

            _lastSeedValue = seed;
        }

        public int Generate()
        {
            var newSeedValue = (_multiplier * _lastSeedValue + _increment) % _modulo;

            _lastSeedValue = newSeedValue;

            return newSeedValue;
        }
    }
}
