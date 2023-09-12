using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IEncryptFilesViewModel
    {
        public bool ButtonFolderBrowserEncryptIsEnabled { get; set; }

        public bool ButtonEncryptFilesIsEnabled { get; set; }

        public string TotalFilesToEncrypt { get; set; }
               
        public string EncryptionFolderName { get; set; }

        public string EncryptionFolderNameWarning { get; set; }

        public string TotalEncryptionFilesSizeInKbs { get; set; }

        public string EncryptionKeyword { get; set; }

        public string EncryptionKeywordConfirmation { get; set; }

        public string EncryptionKeywordWarning { get; set; }

        public int ProgressBarValue { get; set; }

        public string ProgressBarValueString { get; set; }

        public ICommand OpenFolderBrowserEncryptCommand { get; }
        public ICommand CompareKeywordsCommand { get; }
        public ICommand EncryptFilesCommand { get; }

    }
}
