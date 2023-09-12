using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer.Utils.Interfaces
{
    public interface ICipher
    {
        string EncryptFile(string inFile, string outFile, string password, ProgressCallback callback);

        string DecryptFile(string inFile, string outFile, string password, ProgressCallback decryptionProgressCallback);

    }
}
