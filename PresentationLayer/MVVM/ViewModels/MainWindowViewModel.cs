using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using System.Windows.Input;

namespace PresentationLayer.MVVM.ViewModels
{
    public class MainWindowViewModel : NotifyBaseViewModel , IMainWindowViewModel
    {
        private NotifyBaseViewModel _currentChildview;

        IEncryptFilesViewModel _encryptFilesViewModel;
        IDecryptFilesViewModel _decryptFilesViewModel;
    
        public NotifyBaseViewModel CurrentChildView
        {
            get{ return _currentChildview; }
            set { _currentChildview = value;
                OnPropertyChanged(nameof(CurrentChildView));} 
        }

        public ICommand OpenChildViewEncryptFilesCommand { get; }
        public ICommand OpenChildViewDecryptFilesCommand { get; }

        public MainWindowViewModel(IEncryptFilesViewModel encryptFilesViewModel , IDecryptFilesViewModel decryptFilesViewModel
            )
        {

            _encryptFilesViewModel = encryptFilesViewModel;
            _decryptFilesViewModel = decryptFilesViewModel;
            OpenChildViewEncryptFilesCommand = new CommandBaseViewModel(ExecuteOpenChildViewEncryptFilesCommand);
            OpenChildViewDecryptFilesCommand = new CommandBaseViewModel(ExecuteOpenChildViewDecryptFilesCommand);

            ExecuteOpenChildViewEncryptFilesCommand(null);
        }


        private void ExecuteOpenChildViewEncryptFilesCommand(object obj)
        {
            CurrentChildView = (NotifyBaseViewModel)_encryptFilesViewModel;
        }

        private void ExecuteOpenChildViewDecryptFilesCommand(object obj)
        {
            CurrentChildView = (DecryptFilesViewModel)_decryptFilesViewModel;
        }
    }
}
