using ServiceLayer.Utils.Interfaces;

namespace UnitTestFileEncryptDecrypt.MockingClasses
{
    public class Mock_ThreadQueue : IThreadQueue
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void EnqueueTask(Action task)
        {
            throw new NotImplementedException();
        }
    }
}
