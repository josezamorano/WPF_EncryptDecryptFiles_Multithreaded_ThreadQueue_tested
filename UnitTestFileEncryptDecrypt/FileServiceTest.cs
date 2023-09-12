using DataAccessLayer.IOFiles;
using DataAccessLayer.Utils.Interfaces;
using ServiceLayer.Enumerations;
using UnitTestFileEncryptDecrypt.MockingClasses;

namespace UnitTestFileEncryptDecrypt
{
    public class FileServiceTest
    {

        IFileService _fileService;
        IDirectoryProvider _directoryProvider;
        public FileServiceTest()
        {
            _directoryProvider = new Mock_DirectoryProvider();
            _fileService = new FileService(_directoryProvider);
        }


        [Theory]
        //Arrange
        [InlineData("/test", 2)]
        [InlineData("", 1)]
        [InlineData(null, 1)]
        public void GetAllFilesInDirectory_InsertValidPath_ReturnAllFiles(string targetDirectory, int expectedResult)
        {
            //Act
            var actualResult = _fileService.GetAllFilesInDirectory(targetDirectory);
            //Assert
            Assert.Equal(expectedResult, actualResult.Count);

        }


        [Theory]
        //Arrange
        [InlineData("/test", "/test/files/videos/nye.txt", CipherState.Encrypted, "/test_Encrypted/files/videos/nye.txt_Encrypted")]
        public void CreateCipherFileName_InsertValidInputs_ReturnValidString(string originalDirectory, string fullPathOriginalFile, CipherState cipherState, string expectedResult)
        {
            //Act
            var actualResult = _fileService.CreateCipherFileName(originalDirectory, fullPathOriginalFile, CipherState.Encrypted);

            //Assert
            Assert.Contains(actualResult, expectedResult);
        }

        [Theory]
        //Arrange
        [InlineData("/test/", "/test/")]
        [InlineData("/test_Encrypted/", "/test_Encrypted/")]
        [InlineData("/test_Decrypted/", "/test_Decrypted/")]
        [InlineData("/test_Encrypted", "/test")]
        [InlineData("/test_Decrypted", "/test")]
        public void RemoveCipherSuffix_InsertValidInput_ReturnsValidString(string originalPath, string expectedResult)
        {
            //Act
            var result = _fileService.RemoveCipherSuffix(originalPath);

            //Assert
            Assert.Contains(expectedResult, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void RemoveCipherSuffix_InsertInvalidInputs_ThrowsException(string originalPath)
        {
            //Assert
            Assert.Throws<ArgumentException>("fullPathFileName", () => {
                //Act
                var result = _fileService.RemoveCipherSuffix(originalPath);
            });
        }



        [Theory]
        //Arrange
        [InlineData("/test/directory/files", CipherState.Encrypted, "Encrypted")]
        public void CreateCipherFolderName_InsertValidFolderName_ReturnsValidOutputDirectory(string originalDirectory, CipherState cipherState, string expectedSubstring)
        {
            //Act
            var actualString = _fileService.CreateCipherFolderName(originalDirectory, cipherState);

            //Assert
            Assert.Contains(expectedSubstring, actualString);

        }

        [Theory]
        //Arrange
        [InlineData("", CipherState.Encrypted, "Encrypted")]
        [InlineData(null, CipherState.Encrypted, "Encrypted")]
        public void CreateCipherFolderName_FolderNameInvalid_ReturnsException(string originalDirectory, CipherState cipherState, string expectedResult)
        {
            //Assert
            Assert.Throws<ArgumentException>("originalDirectory", () => {
                //Act
                var actualResult = _fileService.CreateCipherFolderName(originalDirectory, cipherState);
            });

        }
    }
}