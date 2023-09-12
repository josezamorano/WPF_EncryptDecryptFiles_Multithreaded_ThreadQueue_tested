using System;

namespace ServiceLayer.Utils.Interfaces
{
    public interface IThreadQueue
    {
        void EnqueueTask(Action task);
        void Dispose();
    }
}
