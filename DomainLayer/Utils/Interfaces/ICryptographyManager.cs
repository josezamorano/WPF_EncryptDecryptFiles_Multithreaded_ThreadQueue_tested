using DomainLayer.Models;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer.Utils.Interfaces
{
    public interface ICryptographyManager
    {
        //FolderContentInfo GetAllFiles(string folder);
        void GetAllFilesThread(string folder, FolderInfoCallback folderInfoCallback);
        void CipherProcessAllFilesThread(CipherInvocationInfo cipherInvocationInfo);
    }
}
