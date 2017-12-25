using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Security.Hashing.HashGen.MD5Static;

namespace Security.Encryption.UI.Facade
{
    public class EnryptionFacade
    {
        private MD5 _hasher;

        public EnryptionFacade()
        {
            _hasher = new MD5();
        }

        public void Encrypt(string password, string inputFilePath, string encryptedFilePath)
        {
            _hasher.Value = password;
            var passwordHash = _hasher.FingerPrint;

            var encryptor = new Rc5(passwordHash, 8);

            var inputFileStream = File.OpenRead(inputFilePath);
            var outputFileStream = File.OpenWrite(encryptedFilePath);

            encryptor.Encrypt(inputFileStream, outputFileStream);
        }

        public void Decrypt(string password, string encryptedFilePath, string decryptedFilePath)
        {
            _hasher.Value = password;
            var passwordHash = _hasher.FingerPrint;

            var encryptor = new Rc5(passwordHash, 8);

            var inputFileStream = File.OpenRead(encryptedFilePath);
            var outputFileStream = File.OpenWrite(decryptedFilePath);

            encryptor.Decrypt(inputFileStream, outputFileStream);
        }
    }
}
