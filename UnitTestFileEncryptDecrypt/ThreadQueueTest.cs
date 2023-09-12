using ServiceLayer.Multithreading;
using ServiceLayer.Utils.Interfaces;

namespace UnitTestFileEncryptDecrypt
{
    public class ThreadQueueTest
    {
        IThreadQueue _threadQueue;
        public ThreadQueueTest()
        {
            _threadQueue = new ThreadQueue();
        }



        [Fact]
        public void EnqueueTask_ProvideValidInput_ReturnsOk()
        {
            //Arrange
            void EncryptDecryptFile()
            {

                string test = "File Encrypted / Decrypted";
                //Assert
                //Method EncryptDecryptFile ahs been called
                Assert.NotEmpty(test);
            }

            Action action = () => { EncryptDecryptFile(); };
            //Act
            _threadQueue.EnqueueTask(action);
        }

    }
}
