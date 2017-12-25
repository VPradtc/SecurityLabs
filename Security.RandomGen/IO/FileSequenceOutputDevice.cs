using System.Collections.Generic;
using System.IO;

namespace Security.RandomGen.IO
{
    public class FileSequenceOutputDevice : ISequenceOutputDevice
    {
        private readonly string _fileName = "output.txt";

        public void Output(IEnumerable<int> sequence)
        {
            using (var fileStream = File.Open(_fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    foreach (var element in sequence)
                    {
                        writer.Write("{0} ", element);
                    }

                    writer.WriteLine();
                }
            }
        }
    }
}
