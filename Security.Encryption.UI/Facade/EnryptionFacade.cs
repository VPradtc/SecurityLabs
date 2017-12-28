using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Hashing.HashGen.MD5;

namespace Security.Encryption.UI.Facade
{
    public class EnryptionFacade
    {
        private MD5Hasher _hasher;

        public EnryptionFacade()
        {
            _hasher = new MD5Hasher();
        }

        public void Encrypt(string password, string inputFilePath, string encryptedFilePath)
        {
            var passwordHash = _hasher.Hash(password);

            var encryptor = new Rc5(passwordHash, 8);

            var inputFileStream = File.OpenRead(inputFilePath);
            var outputFileStream = File.OpenWrite(encryptedFilePath);

            encryptor.Encrypt(inputFileStream, outputFileStream);
        }

        public void Decrypt(string password, string encryptedFilePath, string decryptedFilePath)
        {
            var passwordHash = _hasher.Hash(password);

            var encryptor = new Rc5(passwordHash, 8);

            var inputFileStream = File.OpenRead(encryptedFilePath);
            var outputFileStream = File.OpenWrite(decryptedFilePath);

            encryptor.Decrypt(inputFileStream, outputFileStream);
        }
    }
}
