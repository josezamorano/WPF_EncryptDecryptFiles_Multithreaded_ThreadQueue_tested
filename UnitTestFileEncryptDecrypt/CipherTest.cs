using Autofac.Extras.Moq;
using DataAccessLayer.Utils.Interfaces;
using DomainLayer;
using DomainLayer.Utils.Interfaces;
using System.Security.Cryptography;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace UnitTestFileEncryptDecrypt
{
    public class CipherTest
    {
        private void ReportProgressCallback(int progress) { }


        [Fact]
        public void EncryptFile_InputValidFiles_ReturnsEncryptedFileName()
        {
            FileStream fIn = null;
            FileStream fOut = null;
            SymmetricAlgorithm sma = Rijndael.Create();
            ProgressCallback callback = new ProgressCallback(ReportProgressCallback);
            string filePathRead = "test/Read";
            string filePathWrite = "test/Write";
            FileStream openReadFileStream = null;
            FileStream openWriteFileStream = null;


            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFileProvider>()
                    .Setup(a => a.FileOpenRead(filePathRead))
                    .Returns(openReadFileStream);

                mock.Mock<IFileProvider>()
                    .Setup(b => b.FileOpenWrite("test"))
                    .Returns(openWriteFileStream);

                var cipherHelperMock = mock.Mock<ICipherHelper>();
                cipherHelperMock.Setup(c => c.ResolveEncryption(fIn, fOut, sma, callback))
                    .Returns(string.Empty);

                var cipher = mock.Create<Cipher>();
                var inFile = "test";
                var expected = "test";

                var actual = cipher.EncryptFile(inFile, "test_Encrypt", "abc", callback);

                Assert.Equal(expected, actual);

            }
        }

        [Fact]
        public void DecryptFile_InputValidFiles_ReturnsDecryptedFileName()
        {
            FileStream fIn = null;
            FileStream fOut = null;
            SymmetricAlgorithm sma = Rijndael.Create();
            ProgressCallback callback = new ProgressCallback(ReportProgressCallback);
            string filePathRead = "test/Read";
            string filePathWrite = "test/Write";
            FileStream openReadFileStream = null;
            FileStream openWriteFileStream = null;


            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IFileProvider>()
                    .Setup(a => a.FileOpenRead(filePathRead))
                    .Returns(openReadFileStream);

                mock.Mock<IFileProvider>()
                    .Setup(b => b.FileOpenWrite("test"))
                    .Returns(openWriteFileStream);

                var cipherHelperMock = mock.Mock<ICipherHelper>();
                cipherHelperMock.Setup(c => c.ResolveDecryption(fIn, fOut, sma, callback))
                    .Returns(string.Empty);

                var cipher = mock.Create<Cipher>();
                var inFile = "test";
                var expected = "test";

                var actual = cipher.DecryptFile(inFile, "test_Decrypt", "abc", callback);

                Assert.Equal(expected, actual);

            }
        }


    }
}
