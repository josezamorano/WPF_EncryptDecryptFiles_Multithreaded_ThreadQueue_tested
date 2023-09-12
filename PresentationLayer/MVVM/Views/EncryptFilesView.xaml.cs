using PresentationLayer.MVVM.ViewModels;
using System.Windows.Controls;

namespace PresentationLayer.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EncryptFilesView.xaml
    /// </summary>
    public partial class EncryptFilesView : UserControl
    {
        public EncryptFilesView()
        {
            InitializeComponent();
        }

        //NOTE: ButtonClick="Modal_ButtonClick" Implemented for Demonstration purpuses only-->
        //We use Commands instead for the pattern MVVM
        private void Modal_ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var info1 = e;
        }

    }
}
