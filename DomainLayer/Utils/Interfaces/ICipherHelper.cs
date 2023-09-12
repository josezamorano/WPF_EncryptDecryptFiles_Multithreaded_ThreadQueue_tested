using System.IO;
using System.Security.Cryptography;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer.Utils.Interfaces
{
    public interface ICipherHelper
    {
        string ResolveEncryption(FileStream fileStreamIn, FileStream fileStreamOut, SymmetricAlgorithm sma, ProgressCallback callback);

        string ResolveDecryption(FileStream fileStreamIn, FileStream fileStreamOut, SymmetricAlgorithm sma, ProgressCallback callback);
    }
}
