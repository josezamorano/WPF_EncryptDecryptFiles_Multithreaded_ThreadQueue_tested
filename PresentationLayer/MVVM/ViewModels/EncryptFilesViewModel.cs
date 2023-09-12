using DomainLayer.Models;
using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Messages;
using ServiceLayer.Models;
using ServiceLayer.Utils.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace PresentationLayer.MVVM.ViewModels
{
    public class EncryptFilesViewModel : NotifyBaseViewModel, IEncryptFilesViewModel
    {



        private bool _buttonFolderBrowserEncryptIsEnabled;
        private bool _buttonEncryptFilesIsEnabled;
        private FolderContentInfo _folderContentInfo;
        private FolderBrowserDialog _folderBrowserDialog;
        private string _encryptionFolderName;
        private string _encryptionFolderNameWarning;
        private string _totalFilesToEncrypt;
        private string _totalEncryptionFilesSizeInKbs;
        private double _totalEncryptionFilesSizeBytes;

        private string _encryptionKeyword;
        private string _encryptionKeywordConfirmation;
        private string _encryptiondKeywordWarning;

        private double _fileEncryptionReadSize;

        private int _progressBarValue;
        private string _progressBarValueString;

        private string _modalControlVisibility;
        private string _modalControlTitle;
        private string _modalControlText;

        private string _modalControlBtnCancelVisibility;
        private string _modalControlBtnOKVisibility;
        private string _modalControlBtnYesVisibility;
        private string _modalControlBtnNoVisibility;

        private ICryptographyManager _cryptographyManager;
        private IStringTextValidator _stringTextValidator;
        private IPresentationLayerHelper _presentationLayerHelper;

        public bool ButtonFolderBrowserEncryptIsEnabled
        {
            get { return _buttonFolderBrowserEncryptIsEnabled; }
            set { _buttonFolderBrowserEncryptIsEnabled = value;
                OnPropertyChanged(nameof(ButtonFolderBrowserEncryptIsEnabled)); }
        }

        public bool ButtonEncryptFilesIsEnabled
        {
            get { return _buttonEncryptFilesIsEnabled; }
            set { _buttonEncryptFilesIsEnabled = value;
                OnPropertyChanged(nameof(ButtonEncryptFilesIsEnabled)); }
        }

        public string TotalFilesToEncrypt
        {
            get { return _totalFilesToEncrypt ?? string.Empty; }
            set { _totalFilesToEncrypt = value;
                OnPropertyChanged(nameof(TotalFilesToEncrypt)); }
        }

        public string EncryptionFolderName
        {
            get { return _encryptionFolderName ?? string.Empty; }
            set { _encryptionFolderName = value;
                OnPropertyChanged(nameof(EncryptionFolderName)); }
        }

        public string EncryptionFolderNameWarning
        {
            get { return _encryptionFolderNameWarning ?? string.Empty; }
            set { _encryptionFolderNameWarning = value;
                OnPropertyChanged(nameof(EncryptionFolderNameWarning)); }
        }

        public string TotalEncryptionFilesSizeInKbs
        {
            get { return _totalEncryptionFilesSizeInKbs ?? string.Empty; }
            set { _totalEncryptionFilesSizeInKbs = value;
                OnPropertyChanged(nameof(TotalEncryptionFilesSizeInKbs)); }
        }

        public string EncryptionKeyword
        {
            get { return _encryptionKeyword ?? string.Empty; }
            set { _encryptionKeyword = value;
                OnPropertyChanged(nameof(EncryptionKeyword));
                ClearEncryptionKeywordWarning(); }
        }

        public string EncryptionKeywordConfirmation
        {
            get { return _encryptionKeywordConfirmation ?? string.Empty; }
            set { _encryptionKeywordConfirmation = value;
                OnPropertyChanged(nameof(EncryptionKeywordConfirmation));
                ClearEncryptionKeywordWarning(); }
        }

        public string EncryptionKeywordWarning
        {
            get { return _encryptiondKeywordWarning ?? string.Empty; }
            set { _encryptiondKeywordWarning = value;
                OnPropertyChanged(nameof(EncryptionKeywordWarning)); }
        }

        public int ProgressBarValue
        {
            get { return _progressBarValue; }
            set { _progressBarValue = value;
                OnPropertyChanged(nameof(ProgressBarValue)); }
        }

        public string ProgressBarValueString
        {
            get { return _progressBarValueString; }
            set { _progressBarValueString = value;
                OnPropertyChanged(nameof(ProgressBarValueString)); }
        }

        public string ModalControlVisibility
        {
            get { return _modalControlVisibility ?? string.Empty; }
            set { _modalControlVisibility = value;
                OnPropertyChanged(nameof(ModalControlVisibility)); }
        }

        public string ModalControlTitle
        {
            get { return _modalControlTitle ?? string.Empty; }
            set { _modalControlTitle = value;
                OnPropertyChanged(nameof(ModalControlTitle)); }
        }

        public string ModalControlText
        {
            get { return _modalControlText ?? string.Empty; }
            set { _modalControlText = value;
                OnPropertyChanged(nameof(ModalControlText)); }
        }

        public string ModalControlBtnCancelVisibility
        {
            get { return _modalControlBtnCancelVisibility; }
            set { _modalControlBtnCancelVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnCancelVisibility)); }
        }

        public string ModalControlBtnOkVisibility
        {
            get { return _modalControlBtnOKVisibility; }
            set { _modalControlBtnOKVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnOkVisibility));
            }
        }

        public string ModalControlBtnYesVisibility
        {
            get { return _modalControlBtnYesVisibility; }
            set { _modalControlBtnYesVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnYesVisibility));
            }
        }

        public string ModalControlBtnNoVisibility
        {
            get { return _modalControlBtnNoVisibility; }
            set { _modalControlBtnNoVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnNoVisibility)); }
        }

        public ICommand OpenFolderBrowserEncryptCommand { get; }
        public ICommand CompareKeywordsCommand { get; }
        public ICommand EncryptFilesCommand { get; }
        public ICommand ButtonYesClickedCommand { get; }
        public ICommand ButtonNoClickedCommand { get; }
        public ICommand ButtonOkClickedCommand { get; }

        public EncryptFilesViewModel(
            ICryptographyManager cryptographyManager,
            IStringTextValidator stringTextValidator,
            IPresentationLayerHelper presentationLayerHelper

            )
        {

            _folderContentInfo = null;
            _totalEncryptionFilesSizeBytes = 0;

            ButtonFolderBrowserEncryptIsEnabled = true;
            ButtonEncryptFilesIsEnabled = true;
            ModalControlVisibility = CustomConstants.COLLAPSED;

            _cryptographyManager = cryptographyManager;
            _stringTextValidator = stringTextValidator;
            _presentationLayerHelper = presentationLayerHelper;

            SetAllModalButtonsInvisible();

            _folderBrowserDialog = new FolderBrowserDialog();
            OpenFolderBrowserEncryptCommand = new CommandBaseViewModel(ExecuteOpenFolderBrowserEncryptCommand);
            CompareKeywordsCommand = new CommandBaseViewModel(ExecuteCompareKeywordsCommand);
            EncryptFilesCommand = new CommandBaseViewModel(ExecuteEncryptFilesCommand);
            ButtonYesClickedCommand = new CommandBaseViewModel(ExecuteButtonYesClickedCommand);
            ButtonNoClickedCommand = new CommandBaseViewModel(ExecuteButtonNoClickedCommand);
            ButtonOkClickedCommand = new CommandBaseViewModel(ExecuteButtonOkClickedCommand);
        }

        private void SetAllModalButtonsInvisible()
        {
            ModalControlBtnCancelVisibility = CustomConstants.COLLAPSED;
            ModalControlBtnOkVisibility = CustomConstants.COLLAPSED;
            ModalControlBtnYesVisibility = CustomConstants.COLLAPSED;
            ModalControlBtnNoVisibility = CustomConstants.COLLAPSED;
        }

        private void ExecuteOpenFolderBrowserEncryptCommand(object obj)
        {
            ButtonFolderBrowserEncryptIsEnabled = false;
            ClearEncryptionFolderWarningInputs();
            _presentationLayerHelper.SetFolderBrowserDialog(_folderBrowserDialog);
            SetInputsToEmpty();
            System.Windows.Forms.DialogResult result = _folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                EncryptionFolderName = _folderBrowserDialog.SelectedPath;

                string message = CustomConstants.DO_YOU_WANT_TO_ENCRYPT_THE_FOLDER + " " + EncryptionFolderName;
                SetDisplayModalInfo(CustomConstants.FOLDER_FOR_ENCRYPTION, message);
                SetDisplayModalButtons(CustomConstants.COLLAPSED, CustomConstants.COLLAPSED, CustomConstants.VISIBLE, CustomConstants.VISIBLE);
            }
            else if (result == DialogResult.Cancel)
            {
                ButtonFolderBrowserEncryptIsEnabled = true;
            }
            else
            {
                ButtonFolderBrowserEncryptIsEnabled = true;
            }
            _folderBrowserDialog.Dispose();
        }

        private void SetDisplayModalInfo(string modalTitle, string modalMessage)
        {
            ModalControlVisibility = CustomConstants.VISIBLE;
            ModalControlTitle = modalTitle;
            ModalControlText = modalMessage;
        }

        private void SetDisplayModalButtons(string btnOkVisibility, string btnCancelVisibility, string btnYesVisibility, string btnNoVisibility)
        {
            ModalControlVisibility = CustomConstants.VISIBLE;
            ModalControlBtnOkVisibility = btnOkVisibility;
            ModalControlBtnCancelVisibility = btnCancelVisibility;
            ModalControlBtnYesVisibility = btnYesVisibility;
            ModalControlBtnNoVisibility = btnNoVisibility;
        }

        private void ExecuteButtonYesClickedCommand(object obj)
        {
            FolderInfoCallback folderInfoCallback = new FolderInfoCallback(GetFolderContentInfoReport);
            _cryptographyManager.GetAllFilesThread(EncryptionFolderName, folderInfoCallback);

            SetAllModalButtonsInvisible();
            ModalControlVisibility = CustomConstants.COLLAPSED;
        }

        private void ExecuteButtonNoClickedCommand(object obj)
        {
            EncryptionFolderName = string.Empty;
            ButtonFolderBrowserEncryptIsEnabled = true;

            SetAllModalButtonsInvisible();
            ModalControlVisibility = CustomConstants.COLLAPSED;
        }

        private void ExecuteButtonOkClickedCommand(object obj)
        {
            SetInputsToEmpty();
            SetAllModalButtonsInvisible();
            ModalControlVisibility = CustomConstants.COLLAPSED;            
        }

        private void ExecuteCompareKeywordsCommand(object obj)
        {
            AreEncryptionKeywordsValid();
        }

        private void ClearEncryptionFolderWarningInputs()
        {
            EncryptionFolderNameWarning = string.Empty;
            _totalEncryptionFilesSizeBytes = 0;
        }
              
        private void SetInputsToEmpty()
        {
            _folderContentInfo = null;
            _fileEncryptionReadSize = 0;

            TotalFilesToEncrypt = $"0 {CustomConstants.FILES_TO_ENCRYPT}";
            TotalEncryptionFilesSizeInKbs = "0 kb";
            EncryptionFolderName = string.Empty;
            EncryptionFolderNameWarning = string.Empty;
            EncryptionKeyword = string.Empty;
            EncryptionKeywordConfirmation = string.Empty;
            EncryptionKeywordWarning = string.Empty;

            ProgressBarValue = 0;
            ProgressBarValueString = CustomConstants.ENCRYPTION_PROGRESS + 0 + CustomConstants.PERCENT;
        }


        private bool IsValidEncryptionFolder(FolderContentInfo folderContentInfo)
        {
            _folderContentInfo = folderContentInfo;
            EncryptionFolderNameWarning = (_folderContentInfo == null || _folderContentInfo.TotalFiles == 0) ? Notification.WARNING_MESSAGE : string.Empty;
            _totalEncryptionFilesSizeBytes = (_folderContentInfo == null || _folderContentInfo.TotalFilesSize == 0) ? 0 : _folderContentInfo.TotalFilesSize / CustomConstants.ONE_BYTE_AS_ONE_KILOBYTE;

            return (_totalEncryptionFilesSizeBytes == 0) ? false : true;
        }

        private bool AreEncryptionKeywordsValid()
        {
            var notification = _stringTextValidator.ValidateStrings(EncryptionKeyword, EncryptionKeywordConfirmation);
            if (notification != string.Empty)
            {
                EncryptionKeywordWarning = notification;
                
                return false;
            }
            return true;
        }

        private void ClearEncryptionKeywordWarning()
        {
            if (!string.IsNullOrEmpty(EncryptionKeywordWarning))
            {
                EncryptionKeywordWarning = string.Empty;
            }
        }

        private void ExecuteEncryptFilesCommand(object obj)
        {
            if (!InputAreValid())
            {
                return;
            }

            ButtonFolderBrowserEncryptIsEnabled = false;
            ButtonEncryptFilesIsEnabled = false;

            ProgressCallback progressCallback = new ProgressCallback(ProgressBarEncryptionCallback);
            ReportCallback reportCallback = new ReportCallback(EncryptionReport);
            CipherInvocationInfo cipherInvocationInfo = new CipherInvocationInfo();
            cipherInvocationInfo.CipherState = CipherState.Encrypted;
            cipherInvocationInfo.Password = EncryptionKeyword;
            cipherInvocationInfo.ReportCallBack = reportCallback;
            cipherInvocationInfo.ProgressCallback = progressCallback;
            _cryptographyManager.CipherProcessAllFilesThread(cipherInvocationInfo);
        }



        private bool InputAreValid()
        {
            bool folderIsValid = true;
            bool keywordsAreValid = true;
            if (_totalEncryptionFilesSizeBytes == 0 ||
                string.IsNullOrEmpty(EncryptionFolderNameWarning))
            {
                folderIsValid = IsValidEncryptionFolder(_folderContentInfo);
            }

            bool keywordInputsAreEmpty = (string.IsNullOrEmpty(EncryptionKeyword) ||
                string.IsNullOrEmpty(EncryptionKeywordConfirmation)) ? true : false;
                
            if(keywordInputsAreEmpty || !string.IsNullOrEmpty(EncryptionKeywordWarning))
            {
                keywordsAreValid = AreEncryptionKeywordsValid();
            }

            if ( folderIsValid && keywordsAreValid)
            {
                return true;
            }
            return false;
        }

        private void GetFolderContentInfoReport(FolderContentInfo folderContentInfo)
        {
            TotalFilesToEncrypt = string.Format(CultureInfo.InvariantCulture, "{0:0,0} ", folderContentInfo.TotalFiles) + CustomConstants.FILES_TO_ENCRYPT;
            TotalEncryptionFilesSizeInKbs = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00} kb", folderContentInfo.TotalFilesSize);

            ButtonFolderBrowserEncryptIsEnabled = true;
            IsValidEncryptionFolder(folderContentInfo);
        }

        private void EncryptionReport(string report)
        {
            ButtonFolderBrowserEncryptIsEnabled = true;
            ButtonEncryptFilesIsEnabled = true;
       
            SetDisplayModalInfo(CustomConstants.ENCRYPTION_REPORT, report);
            SetDisplayModalButtons(CustomConstants.VISIBLE, CustomConstants.COLLAPSED, CustomConstants.COLLAPSED, CustomConstants.COLLAPSED);
        }

        private void ProgressBarEncryptionCallback(int value)
        {
            Action action = () => {
                _fileEncryptionReadSize = _fileEncryptionReadSize + value;
                double currentRead = ((_fileEncryptionReadSize / _totalEncryptionFilesSizeBytes) * 100);
                double safeCurrentRead = (currentRead >= CustomConstants.NINETY_NINE_POINT_EIGHTY) ? CustomConstants.ONE_HUNDRED : currentRead;
                ProgressBarValueString = CustomConstants.ENCRYPTION_PROGRESS + safeCurrentRead + CustomConstants.PERCENT;
                ProgressBarValue = (int)safeCurrentRead;
            };

            Task.Factory.StartNew(() => { action();});            
        }

    }
}
