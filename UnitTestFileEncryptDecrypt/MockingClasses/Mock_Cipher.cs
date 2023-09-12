using DomainLayer.Utils.Interfaces;
using ServiceLayer.DelegateTypess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFileEncryptDecrypt.MockingClasses
{
    public class Mock_Cipher : ICipher
    {
        public string DecryptFile(string inFile, string outFile, string password, CustomDelegates.ProgressCallback decryptionProgressCallback)
        {
            throw new NotImplementedException();
        }

        public string EncryptFile(string inFile, string outFile, string password, CustomDelegates.ProgressCallback callback)
        {
            throw new NotImplementedException();
        }
    }
}
