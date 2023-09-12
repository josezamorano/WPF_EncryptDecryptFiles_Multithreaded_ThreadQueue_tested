using ServiceLayer.Enumerations;
using System.Collections.Generic;

namespace DataAccessLayer.Utils.Interfaces
{
    public interface IFileService
    {
        List<string> GetAllFilesInDirectory(string directory);

        string CreateCipherFileName(string originalDirectory, string fullPathOriginalFile, CipherState cipherState);

        string RemoveCipherSuffix(string fullPathFileName);

        string CreateCipherFolderName(string originalDirectory, CipherState cipherState);

    }
}
