using DomainLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using System.Collections.Generic;
using System.IO;

namespace DomainLayer
{
    public class CryptographyHelper : ICryptographyHelper
    {
        public double GetAllSelectedFilesTotalSizeInKb(List<string> allFiles)
        {
            //1000 bytes = 1     kiloByte
            //1    byte  = 0.001 KiloByte           
            double totalSizeKb = 0.0;
            foreach (string file in allFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Exists)
                {
                    var infoLengthBytes = fileInfo.Length;
                    totalSizeKb += (infoLengthBytes * CustomConstants.ONE_BYTE_IN_KILOBYTE);
                }
            }

            return totalSizeKb;
        }

    }
}
