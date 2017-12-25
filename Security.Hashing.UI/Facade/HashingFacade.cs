using System.IO;
using Security.Hashing.HashGen.MD5Static;

namespace Security.Hashing.UI.Facade
{
    public class HashingFacade
    {
        private MD5 _hasher;

        public HashingFacade()
        {
            _hasher = new MD5();
        }

        public string GetHash(string value)
        {
            _hasher.Value = value;

            return _hasher.FingerPrint;
        }

        public string GetHashForFile(string path)
        {
            var input = ReadFile(path);

            return GetHash(input);
        }
        
        public string ReadFile(string path)
        {

            using (var file = File.OpenRead(path))
            {
                using (var reader = new StreamReader(file))
                {
                    var input = reader.ReadToEnd();

                    return input;
                }
            }
        }

        public bool VerifyIntegrity(string pathToFile, string pathToHashFile)
        {
            var input = ReadFile(pathToFile);
            var hash = ReadFile(pathToHashFile);

            var inputHash = GetHash(input);

            return hash == inputHash;
        }

        public void SaveToFile(string path, string hash)
        {
            using (var file = File.OpenWrite(path))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.Write(hash);
                }
            }
        }
    }
}
