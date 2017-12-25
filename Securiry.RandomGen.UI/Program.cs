using Security.RandomGen.Generation.Sequence;
using Security.RandomGen.Initialization;
using Security.RandomGen.IO;
using Security.RandomGen.UI.Facade;

namespace Security.RandomGen.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var facade = new RandomGenExecutionFacade(
                new FiniteRandomNumberSequenceGenerator(
                    new HardcodedRandomNumberGeneratorFactory()
                )
                , new ConsoleSequenceOutputDevice()
                , new FileSequenceOutputDevice()
                );

            facade.Execute();
        }
    }
}
