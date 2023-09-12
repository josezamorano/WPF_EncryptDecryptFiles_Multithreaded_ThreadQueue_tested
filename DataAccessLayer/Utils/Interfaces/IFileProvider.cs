using System.IO;

namespace DataAccessLayer.Utils.Interfaces
{
    public interface IFileProvider
    {
        FileStream FileOpenRead(string fullFilePath);

        FileStream FileOpenWrite(string fullFilePath);
    }
}
