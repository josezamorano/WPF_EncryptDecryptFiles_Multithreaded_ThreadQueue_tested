using DataAccessLayer.Utils.Interfaces;
using System.IO;

namespace DataAccessLayer.IOFiles
{
    public class DirectoryProvider : IDirectoryProvider
    {

        public DirectoryProvider()
        {            
        }
        public void ResolveDirectoryIfNoExists(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
        }

        public string[] GetFilesFromDirectory(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public string[] GetAllDirectoriesFromDirectory(string directory)
        {
            return Directory.GetDirectories(directory);
        }

        public bool DirectoryExists(string directory)
        {
            bool exists = System.IO.Directory.Exists(directory);
            return exists;
        }

        public DirectoryInfo CreateDirectory(string fullPath)
        {
            return System.IO.Directory.CreateDirectory(fullPath);
        }
    }
}