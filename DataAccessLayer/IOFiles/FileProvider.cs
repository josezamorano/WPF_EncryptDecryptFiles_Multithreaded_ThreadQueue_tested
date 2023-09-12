using DataAccessLayer.Utils.Interfaces;
using System.IO;

namespace DataAccessLayer.IOFiles
{
    public class FileProvider : IFileProvider
    {

        public FileProvider()
        {            
        }
        public FileStream FileOpenRead(string fullFilePath)
        {
            return File.OpenRead(fullFilePath);
        }

        public FileStream FileOpenWrite(string fullFilePath)
        {
            return File.OpenWrite(fullFilePath);
        }
    }
}
