using PresentationLayer.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IMainWindowViewModel
    {
        public NotifyBaseViewModel CurrentChildView { get; set; }
        public ICommand OpenChildViewEncryptFilesCommand { get; }
        public ICommand OpenChildViewDecryptFilesCommand { get; }

    }
}
