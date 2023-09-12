using DataAccessLayer.Utils.Interfaces;
using DomainLayer.Utils.Interfaces;
using System.IO;
using System.Security.Cryptography;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer
{
    public class Cipher : ICipher
    {

        // Crypto Random number generator for use in EncryptFile		
        private static RandomNumberGenerator CryptoRandomNumber;

        private static string HASH_NAME = "SHA256";

        ICipherHelper _cipherHelper;
        IFileProvider _fileProvider;

        public Cipher(ICipherHelper cipherHelper, IFileProvider fileProvider)
        {
            CryptoRandomNumber = new RNGCryptoServiceProvider();
            _cipherHelper = cipherHelper;
            _fileProvider = fileProvider;
        }


        //Test
        public string EncryptFile(string inFile, string outFile, string password, ProgressCallback callback)
        {
            FileStream fileStreamIn = _fileProvider.FileOpenRead(inFile);
            FileStream fileStreamOut = _fileProvider.FileOpenWrite(outFile);

            // generate IV and Salt
            byte[] IV = GenerateRandomBytes(16);
            byte[] salt = GenerateRandomBytes(16);

            SymmetricAlgorithm sma = CreateRijndaelEncryptor(password, salt);
            sma.IV = IV;

            // write the IV and salt to the beginning of the file
            if (fileStreamOut != null)
            {
                fileStreamOut.Write(IV, 0, IV.Length);
                fileStreamOut.Write(salt, 0, salt.Length);
            }


            string fileEncryptionInfo = _cipherHelper.ResolveEncryption(fileStreamIn, fileStreamOut, sma, callback);
            return (string.IsNullOrEmpty(fileEncryptionInfo)) ? inFile : inFile + "|" + fileEncryptionInfo;
        }

        //Test
        public string DecryptFile(string inFile, string outFile, string password, ProgressCallback callback)
        {
            // create and open the file streams
            FileStream fileStreamIn = _fileProvider.FileOpenRead(inFile);
            FileStream fileStreamOut = _fileProvider.FileOpenWrite(outFile);

            // read off the IV and Salt
            byte[] IV = new byte[16];
            byte[] salt = new byte[16];
            if(fileStreamIn != null)
            {
                fileStreamIn.Read(IV, 0, 16);
                fileStreamIn.Read(salt, 0, 16);
            }

            // create the crypting stream
            SymmetricAlgorithm sma = CreateRijndaelEncryptor(password, salt);
            sma.IV = IV;

            string fileDecryptionInfo = _cipherHelper.ResolveDecryption(fileStreamIn, fileStreamOut, sma, callback);
            return (string.IsNullOrEmpty(fileDecryptionInfo)) ? inFile : inFile + "|" + fileDecryptionInfo;
        }


        #region Private Methods
        private static byte[] GenerateRandomBytes(int count)
        {
            byte[] bytes = new byte[count];
            CryptoRandomNumber.GetBytes(bytes);
            return bytes;
        }


        private static SymmetricAlgorithm CreateRijndaelEncryptor(string password, byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, HASH_NAME, 1000);

            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }


        #endregion Private Methods
    }
}
