using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.RandomGen.Generation.Sequence;
using Security.RandomGen.IO;

namespace Security.RandomGen.UI.Facade
{
    public class RandomGenExecutionFacade
    {
        private IRandomNumberSequenceGenerator _sequenceGenerator;
        private ISequenceOutputDevice _fileOutputDevice;
        private ISequenceOutputDevice _consoleOutputDevice;

        public RandomGenExecutionFacade(
            IRandomNumberSequenceGenerator sequenceGenerator
            , ISequenceOutputDevice consoleOutputDevice
            , ISequenceOutputDevice fileOutputDevice
            )
        {
            _sequenceGenerator = sequenceGenerator;
            _consoleOutputDevice = consoleOutputDevice;
            _fileOutputDevice = fileOutputDevice;
        }

        public void Execute()
        {
            Console.WriteLine("Enter sequence length:");

            var lengthString = Console.ReadLine();
            
            int length;
            if (!int.TryParse(lengthString, out length))
            {
                Console.WriteLine("Invalid input. Press any key to exit...");
                Console.ReadKey();
                return;
            }

            var sequence = _sequenceGenerator.Generate(length);

            _consoleOutputDevice.Output(sequence);
            _fileOutputDevice.Output(sequence);

            Console.WriteLine("Sequence terminated.");
            Console.ReadKey();
        }
    }
}
