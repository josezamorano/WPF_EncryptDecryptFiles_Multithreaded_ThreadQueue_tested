namespace ServiceLayer.DelegateTypess
{
    public class CustomDelegates
    {

        public delegate void ReportCallback(string encryptionReport);
        public delegate void ProgressCallback(int progressValue);
        public delegate void FolderInfoCallback(ServiceLayer.Models.FolderContentInfo folderContentInfo);

    }
}
