using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.RandomGen.IO
{
    public class ConsoleSequenceOutputDevice : ISequenceOutputDevice
    {
        public void Output(IEnumerable<int> sequence)
        {
            foreach (var element in sequence)
            {
                Console.Write("{0} ", element);
            }

            Console.WriteLine();
        }
    }
}
