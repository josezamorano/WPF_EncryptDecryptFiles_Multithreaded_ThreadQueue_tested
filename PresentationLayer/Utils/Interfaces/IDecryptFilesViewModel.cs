using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IDecryptFilesViewModel
    {
        public bool ButtonFolderBrowserDecryptIsEnabled { get; set; }

        public bool ButtonDecryptFilesIsEnabled { get; set; }

        public string TotalFilesToDecrypt { get; set; }

        public string DecryptionFolderName { get; set; }

        public string DecryptionFolderNameWarning { get; set; }

        public string TotalDecryptionFilesSizeInKbs {  get; set; }   

        public string DecryptionKeyword { get; set; }

        public string DecryptionKeywordConfirmation {  get; set; }

        public string DecryptionKeywordWarning { get; set; }

        public int ProgressBarValue {  get; set; }

        public string ProgressBarValueString { get; set; }

        public ICommand OpenFolderBrowserDecryptCommand { get; }

        public ICommand CompareKeywordsCommand { get; }
        
        public ICommand DecryptFilesCommand { get; }


    }
}
