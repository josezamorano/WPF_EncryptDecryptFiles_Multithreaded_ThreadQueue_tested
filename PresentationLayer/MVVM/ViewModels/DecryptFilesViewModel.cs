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
    public class DecryptFilesViewModel : NotifyBaseViewModel, IDecryptFilesViewModel
    {
        private bool _buttonFolderBrowserDecryptIsEnabled;
        private bool _buttonDecryptFilesIsEnabled;

        private FolderContentInfo _folderContentInfo;
        private FolderBrowserDialog _folderBrowserDialog;

        private string _decryptionFolderName;
        private string _decryptionFolderNameWarning;

        private double _fileDecryptionReadSize;
        private string _totalFilesToDecrypt;
        private string _totalDecryptionFilesSizeInKbs;

        private string _decryptionKeyword;
        private string _decryptionKeywordConfirmation;
        private string _decryptionKeywordWarning;

        private int _progressBarValue;
        private string _progressBarValueString;

        //======
        private string _modalControlVisibility;
        private string _modalControlTitle;
        private string _modalControlText;

        private string _modalControlBtnCancelVisibility;
        private string _modalControlBtnOKVisibility;
        private string _modalControlBtnYesVisibility;
        private string _modalControlBtnNoVisibility;

        //======


        private ICryptographyManager _cryptographyManager;
        private IStringTextValidator _stringTextValidator;
        private IPresentationLayerHelper _presentationLayerHelper;

        private double _totalDecryptionFilesSizeBytes;
        public bool ButtonFolderBrowserDecryptIsEnabled
        {
            get{ return _buttonFolderBrowserDecryptIsEnabled; }
            set
            {
                _buttonFolderBrowserDecryptIsEnabled = value;
                OnPropertyChanged(nameof(ButtonFolderBrowserDecryptIsEnabled));
            }
        }

        public bool ButtonDecryptFilesIsEnabled
        {
            get { return _buttonDecryptFilesIsEnabled; }
            set
            {
                _buttonDecryptFilesIsEnabled = value;
                OnPropertyChanged(nameof(ButtonDecryptFilesIsEnabled));
            }
        }

        public string TotalFilesToDecrypt
        {
            get { return _totalFilesToDecrypt ?? string.Empty; }
            set
            {
                _totalFilesToDecrypt = value;
                OnPropertyChanged(nameof(TotalFilesToDecrypt));
            }
        }

        public string DecryptionFolderName
        {
            get { return _decryptionFolderName ?? string.Empty; }
            set
            {
                _decryptionFolderName = value;
                OnPropertyChanged(nameof(DecryptionFolderName));
            }
        }

        public string DecryptionFolderNameWarning
        {
            get { return _decryptionFolderNameWarning ?? string.Empty; }
            set
            {
                _decryptionFolderNameWarning = value;
                OnPropertyChanged(nameof(DecryptionFolderNameWarning));
            }
        }
              

        public string TotalDecryptionFilesSizeInKbs
        {
            get { return _totalDecryptionFilesSizeInKbs ?? string.Empty; }
            set { _totalDecryptionFilesSizeInKbs = value;
                OnPropertyChanged(nameof(TotalDecryptionFilesSizeInKbs));}
        }

        public string DecryptionKeyword
        {
            get { return _decryptionKeyword ?? string.Empty; }
            set { _decryptionKeyword = value;
                  OnPropertyChanged(nameof(DecryptionKeyword));
                ClearDecryptionKeywordWarning(); }
        }

        public string DecryptionKeywordConfirmation
        {
            get { return _decryptionKeywordConfirmation; }
            set { _decryptionKeywordConfirmation = value;
                  OnPropertyChanged(nameof(DecryptionKeywordConfirmation));
                ClearDecryptionKeywordWarning(); }
        }

        public string DecryptionKeywordWarning
        {
            get { return  (_decryptionKeywordWarning ?? string.Empty);}
            set { _decryptionKeywordWarning = value;
                  OnPropertyChanged(nameof(DecryptionKeywordWarning));}
        }

        public int ProgressBarValue
        {
            get { return _progressBarValue; }
            set
            {
                _progressBarValue = value;
                OnPropertyChanged(nameof(ProgressBarValue));
            }
        }

        public string ProgressBarValueString
        {
            get { return _progressBarValueString ?? string.Empty; }
            set { _progressBarValueString = value;
            OnPropertyChanged(nameof(ProgressBarValueString));}
        }

        //========

        public string ModalControlVisibility
        {
            get { return _modalControlVisibility ?? string.Empty; }
            set
            {
                _modalControlVisibility = value;
                OnPropertyChanged(nameof(ModalControlVisibility));
            }
        }

        public string ModalControlTitle
        {
            get { return _modalControlTitle ?? string.Empty; }
            set
            {
                _modalControlTitle = value;
                OnPropertyChanged(nameof(ModalControlTitle));
            }
        }

        public string ModalControlText
        {
            get { return _modalControlText ?? string.Empty; }
            set
            {
                _modalControlText = value;
                OnPropertyChanged(nameof(ModalControlText));
            }
        }

        public string ModalControlBtnCancelVisibility
        {
            get { return _modalControlBtnCancelVisibility; }
            set
            {
                _modalControlBtnCancelVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnCancelVisibility));
            }
        }

        public string ModalControlBtnOkVisibility
        {
            get { return _modalControlBtnOKVisibility; }
            set
            {
                _modalControlBtnOKVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnOkVisibility));
            }
        }

        public string ModalControlBtnYesVisibility
        {
            get { return _modalControlBtnYesVisibility; }
            set
            {
                _modalControlBtnYesVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnYesVisibility));
            }
        }

        public string ModalControlBtnNoVisibility
        {
            get { return _modalControlBtnNoVisibility; }
            set
            {
                _modalControlBtnNoVisibility = value;
                OnPropertyChanged(nameof(ModalControlBtnNoVisibility));
            }
        }

        //=========

        public ICommand OpenFolderBrowserDecryptCommand { get; }
        public ICommand CompareKeywordsCommand { get;  }
        public ICommand DecryptFilesCommand { get; }
        //==========
        public ICommand ButtonYesClickedCommand { get; }
        public ICommand ButtonNoClickedCommand { get; }
        public ICommand ButtonOkClickedCommand { get; }

        //==========


        public DecryptFilesViewModel(
            ICryptographyManager cryptographyManager, 
            IStringTextValidator stringTextValidator,
            IPresentationLayerHelper presentationLayerHelper
            )
        {
            _folderContentInfo = null;
            _totalDecryptionFilesSizeBytes = 0;

            ButtonFolderBrowserDecryptIsEnabled = true;
            ButtonDecryptFilesIsEnabled = true;
            ModalControlVisibility = CustomConstants.COLLAPSED;

            _cryptographyManager = cryptographyManager;
            _stringTextValidator = stringTextValidator;
            _presentationLayerHelper = presentationLayerHelper;

            SetAllModalButtonsInvisible();

            _folderBrowserDialog = new FolderBrowserDialog();
            OpenFolderBrowserDecryptCommand = new CommandBaseViewModel(ExecuteOpenFolderBrowserDecryptCommand);
            CompareKeywordsCommand = new CommandBaseViewModel(ExecuteCompareKeywordsCommand);
            DecryptFilesCommand = new CommandBaseViewModel(ExecuteDecryptFilesCommand);

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
        private void ExecuteOpenFolderBrowserDecryptCommand(object obj)
        {
            ButtonFolderBrowserDecryptIsEnabled = false;
            ClearDecryptionFolderWarningInputs();
            _presentationLayerHelper.SetFolderBrowserDialog(_folderBrowserDialog); 
            SetInputsToEmpty();
            System.Windows.Forms.DialogResult result = _folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                DecryptionFolderName = _folderBrowserDialog.SelectedPath;

                string message = CustomConstants.DO_YOU_WANT_TO_DECRYPT_THE_FOLDER + " " + DecryptionFolderName;
                SetDisplayModalInfo(CustomConstants.FOLDER_FOR_DECRYPTION, message);
                SetDisplayModalButtons(CustomConstants.COLLAPSED, CustomConstants.COLLAPSED, CustomConstants.VISIBLE, CustomConstants.VISIBLE);
            }
            else if (result == DialogResult.Cancel)
            {
                ButtonFolderBrowserDecryptIsEnabled = true;
            }
            else
            {
                ButtonFolderBrowserDecryptIsEnabled = true;
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
            _cryptographyManager.GetAllFilesThread(DecryptionFolderName, folderInfoCallback);

            SetAllModalButtonsInvisible();
            ModalControlVisibility = CustomConstants.COLLAPSED;
        }

        private void ExecuteButtonNoClickedCommand(object obj)
        {
            DecryptionFolderName = string.Empty;
            ButtonFolderBrowserDecryptIsEnabled = true;

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
            AreDecryptionKeywordsValid();
        }
        private void ClearDecryptionFolderWarningInputs()
        {
            DecryptionFolderNameWarning = string.Empty;
            _totalDecryptionFilesSizeBytes = 0;
        }

        private void SetInputsToEmpty()
        {
            _folderContentInfo = null;
            _fileDecryptionReadSize = 0;

            TotalFilesToDecrypt = $"0 {CustomConstants.FILES_TO_DECRYPT}";
            TotalDecryptionFilesSizeInKbs = "0 kb";
            DecryptionFolderName = string.Empty;
            DecryptionFolderNameWarning = string.Empty;
            DecryptionKeyword = string.Empty;
            DecryptionKeywordConfirmation = string.Empty;
            DecryptionKeywordWarning = string.Empty;

            ProgressBarValue = 0;
            ProgressBarValueString = CustomConstants.DECRYPTION_PROGRESS + 0 + CustomConstants.PERCENT;
        }

        //====
        private bool IsValidDecryptionFolder(FolderContentInfo folderContentInfo)
        {
            _folderContentInfo = folderContentInfo;
            DecryptionFolderNameWarning = (_folderContentInfo == null || _folderContentInfo.TotalFiles == 0) ? Notification.WARNING_MESSAGE : string.Empty;
            _totalDecryptionFilesSizeBytes = (_folderContentInfo == null || _folderContentInfo.TotalFilesSize == 0) ? 0 : _folderContentInfo.TotalFilesSize / CustomConstants.ONE_BYTE_AS_ONE_KILOBYTE;

            return (_totalDecryptionFilesSizeBytes == 0) ? false : true;
        }

        private bool AreDecryptionKeywordsValid()
        {
            var notification = _stringTextValidator.ValidateStrings(DecryptionKeyword, DecryptionKeywordConfirmation);
            if (notification != string.Empty)
            {
                DecryptionKeywordWarning = notification;
                return false;
            }
            return true;
        }

        private void ClearDecryptionKeywordWarning()
        {
            if (!string.IsNullOrEmpty(DecryptionKeywordWarning))
            {
                DecryptionKeywordWarning = string.Empty;
            }
        }
        //====
        private void ExecuteDecryptFilesCommand(object obj)
        {
            if (!InputAreValid())
            {
                return;
            }

            ButtonFolderBrowserDecryptIsEnabled = false;
            ButtonDecryptFilesIsEnabled = false;

            ProgressCallback progressCallback = new ProgressCallback(ProgressBarDecryptionCallback);
            ReportCallback reportCallback = new ReportCallback(DecryptionReport);
            CipherInvocationInfo cipherInvocationInfo = new CipherInvocationInfo();
            cipherInvocationInfo.CipherState = CipherState.Decrypted;
            cipherInvocationInfo.Password = DecryptionKeyword;
            cipherInvocationInfo.ReportCallBack = reportCallback;
            cipherInvocationInfo.ProgressCallback = progressCallback;
            _cryptographyManager.CipherProcessAllFilesThread(cipherInvocationInfo);
        }

        private bool InputAreValid()
        {
            bool folderIsValid = true;
            bool keywordsAreValid = true;
            if (_totalDecryptionFilesSizeBytes == 0 ||
                string.IsNullOrEmpty(DecryptionFolderNameWarning))
            {
                folderIsValid = IsValidDecryptionFolder(_folderContentInfo);
            }

            bool keywordInputsAreEmpty = (string.IsNullOrEmpty(DecryptionKeyword) ||
                string.IsNullOrEmpty(DecryptionKeywordConfirmation)) ? true : false;

            if (keywordInputsAreEmpty || !string.IsNullOrEmpty(DecryptionKeywordWarning))
            {
                keywordsAreValid = AreDecryptionKeywordsValid();
            }

            if (folderIsValid && keywordsAreValid)
            {
                return true;
            }
            return false;
        }

        private void GetFolderContentInfoReport(FolderContentInfo folderContentInfo)
        {
            TotalFilesToDecrypt = string.Format(CultureInfo.InvariantCulture, "{0:0,0} ", folderContentInfo.TotalFiles) + CustomConstants.FILES_TO_DECRYPT;
            TotalDecryptionFilesSizeInKbs = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00} kb", folderContentInfo.TotalFilesSize);

            ButtonFolderBrowserDecryptIsEnabled = true;
            IsValidDecryptionFolder(folderContentInfo);
        }

        private void DecryptionReport(string report)
        {
            ButtonFolderBrowserDecryptIsEnabled = true;
            ButtonDecryptFilesIsEnabled = true;

            SetDisplayModalInfo(CustomConstants.DECRYPTION_REPORT, report);
            SetDisplayModalButtons(CustomConstants.VISIBLE, CustomConstants.COLLAPSED, CustomConstants.COLLAPSED, CustomConstants.COLLAPSED);
        }

        private void ProgressBarDecryptionCallback(int value)
        {

            Action action = () => {
                _fileDecryptionReadSize = _fileDecryptionReadSize + value;
                double currentRead = ((_fileDecryptionReadSize / _totalDecryptionFilesSizeBytes) * 100);
                double safeCurrentRead = (currentRead >=CustomConstants.NINETY_NINE_POINT_EIGHTY) ? CustomConstants.ONE_HUNDRED : currentRead;
                ProgressBarValueString = CustomConstants.DECRYPTION_PROGRESS + safeCurrentRead + CustomConstants.PERCENT;
                ProgressBarValue = (int)safeCurrentRead;
            };

            Task.Factory.StartNew(() => { action(); });
        }


    }
}
