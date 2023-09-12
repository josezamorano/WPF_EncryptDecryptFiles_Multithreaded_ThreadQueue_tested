using System.IO;

namespace DataAccessLayer.Utils.Interfaces
{
    public interface IDirectoryProvider
    {
        void ResolveDirectoryIfNoExists(string filePath);

        string[] GetFilesFromDirectory(string directory);

        string[] GetAllDirectoriesFromDirectory(string directory);

        bool DirectoryExists(string directory);

        DirectoryInfo CreateDirectory(string directory);

    }
}
