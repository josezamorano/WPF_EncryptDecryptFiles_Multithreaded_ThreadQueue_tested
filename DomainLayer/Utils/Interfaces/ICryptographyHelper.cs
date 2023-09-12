using System.Collections.Generic;

namespace DomainLayer.Utils.Interfaces
{
    public interface ICryptographyHelper
    {
        double GetAllSelectedFilesTotalSizeInKb(List<string> allFiles);
    }
}
