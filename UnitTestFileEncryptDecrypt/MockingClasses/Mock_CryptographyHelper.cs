using DomainLayer.Utils.Interfaces;

namespace UnitTestFileEncryptDecrypt.MockingClasses
{
    public class Mock_CryptographyHelper : ICryptographyHelper
    {
        public double GetAllSelectedFilesTotalSizeInKb(List<string> allFiles)
        {
            return 1200;
        }
    }
}
