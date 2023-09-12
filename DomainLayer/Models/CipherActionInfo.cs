using ServiceLayer.Enumerations;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer.Models
{
    public class CipherActionInfo
    {
        public CipherState CipherState { get; set; }

        public string InFile { get; set; }

        public string OutFile { get; set; }

        public string Password { get; set; }

        public ProgressCallback ProgressCallback { get; set; }
    }
}
