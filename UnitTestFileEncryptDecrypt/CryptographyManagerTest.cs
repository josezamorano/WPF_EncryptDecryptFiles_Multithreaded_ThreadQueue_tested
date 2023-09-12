using Autofac.Extras.Moq;
using DataAccessLayer.Utils.Interfaces;
using DomainLayer;
using DomainLayer.Models;
using DomainLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Models;
using ServiceLayer.Utils.Interfaces;
using System.Globalization;
using UnitTestFileEncryptDecrypt.MockingClasses;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace UnitTestFileEncryptDecrypt
{
    public class CryptographyManagerTest
    {
        IFileService _fileServiceMock;
        ICipher _cipherMock;
        IThreadQueue _threadQueueMock;
        ICryptographyHelper _cryptographyHelperMock;
        ICryptographyManager _cryptographyManager;
        
        public CryptographyManagerTest()
        {
            _fileServiceMock = new Mock_FileService();
            _cipherMock = new Mock_Cipher();
            _threadQueueMock = new Mock_ThreadQueue();
            _cryptographyHelperMock = new Mock_CryptographyHelper();
            _cryptographyManager = new CryptographyManager(_fileServiceMock, _cipherMock, _threadQueueMock , _cryptographyHelperMock);
        }


        [Fact]
        public void GetAllFiles_InputValidFolder_ReturnsAllFilesList()
        {
            //Arrange
            var folder = "test";
            var expectedCount = 2;
            var totalFileSize = 1200;
            FolderContentInfo folderContentInfo = new FolderContentInfo()
            {
                TotalFiles = 10,
                TotalFilesSize = 1200
            };
            List<string> allFiles = new List<string>() { "file1.txt", "file2.txt" };


            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICryptographyHelper>()
                    .Setup(a => a.GetAllSelectedFilesTotalSizeInKb(allFiles))
                    .Returns(totalFileSize);

                void GetFolderContentInfoReport(FolderContentInfo folderContentInfo)
                {
                    string totalFiles = string.Format(CultureInfo.InvariantCulture, "{0:0,0} ", folderContentInfo.TotalFiles) + CustomConstants.FILES_TO_ENCRYPT;
                    string totalSizeKb = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00} kb", folderContentInfo.TotalFilesSize);


                    //Assert
                    Assert.Equal(expectedCount, folderContentInfo.TotalFiles);
                }
                              
                FolderInfoCallback folderInfoCallback = new FolderInfoCallback(GetFolderContentInfoReport);

                //Act
                _cryptographyManager.GetAllFilesThread(folder, folderInfoCallback);

            }
            
        }

        [Fact]
        public void CipherProcessAllFilesThread_InputValidFolder_ReturnOk()
        {
            //Arrange
            CipherInvocationInfo cipherInvocationInfo = new CipherInvocationInfo();
            cipherInvocationInfo.CipherState = CipherState.Encrypted;
            cipherInvocationInfo.Password = "abc";

            void ReportCallback(string report)
            {
                //Assert
                Assert.NotNull(report);
            }

            void ProgressCallback(int count)
            {
                int total = count;
            }
            cipherInvocationInfo.ReportCallBack = new ReportCallback(ReportCallback);
            cipherInvocationInfo.ProgressCallback = new ProgressCallback(ProgressCallback);

            //Act
            _cryptographyManager.CipherProcessAllFilesThread(cipherInvocationInfo);
        }


        #region Private Methods 


        #endregion Private Methods

    }
}
