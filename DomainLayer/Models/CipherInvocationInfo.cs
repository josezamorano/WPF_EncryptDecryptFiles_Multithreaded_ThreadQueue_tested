using ServiceLayer.Enumerations;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer.Models
{
    public class CipherInvocationInfo
    {
        public CipherState CipherState { get; set; }

        public string Password { get; set; }

        public ReportCallback ReportCallBack { get; set; }

        public ProgressCallback ProgressCallback { get; set; }
    }
}
