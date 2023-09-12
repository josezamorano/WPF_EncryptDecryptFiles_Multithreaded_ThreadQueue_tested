using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace PresentationLayer.CustomUserControls
{
    /// <summary>
    /// Interaction logic for Modal.xaml
    /// </summary>
    public partial class Modal : UserControl
    {
        public Modal()
        {
            InitializeComponent();
        }

        //BEGIN Custom Properties
        #region BEGIN Custom Properties 
        private string _modalTitle;
        private string _modalText;
        private string _modalTitleColor;
        private string _modalTextColor;       
        
        private string _modalBtnOkColor;
        private string _modalBtnCancelColor;
        private string _modalBtnYesColor;
        private string _modalBtnNoColor;
       
        private string _modalBtnOkVisibility;
        private string _modalBtnCancelVisibility;
        private string _modalBtnYesVisibility;
        private string _modalBtnNoVisibility;



        public string ModalTitle
        {
            get { return _modalTitle ?? string.Empty; }
            set { _modalTitle = value; }
        }
        public static readonly DependencyProperty ModalTitleProperty = 
            DependencyProperty.Register(nameof(ModalTitle), typeof(string), typeof(Modal));

        public string ModalText
        {
            get { return _modalText ?? string.Empty; }
            set { _modalText = value; }
        }
        public static readonly DependencyProperty ModalTextProperty =
            DependencyProperty.Register(nameof(ModalText), typeof(string), typeof(Modal));

        public string ModalTitleColor
        {
            get { return _modalTitleColor; }
            set { _modalTitleColor = value; }
        }

        public static readonly DependencyProperty ModalTitleColorProperty =
            DependencyProperty.Register(nameof(ModalTitleColor), typeof(string), typeof(Modal));


        public string ModalTextColor
        {
            get { return _modalTextColor; }
            set { _modalTextColor = value; }
        }

        public static readonly DependencyProperty ModalTextColorProperty =
            DependencyProperty.Register(nameof(ModalTextColor), typeof(string), typeof(Modal));


        public string ModalBtnOkColor
        {
            get { return _modalBtnOkColor; }
            set { _modalBtnOkColor = value; }
        }

        public static readonly DependencyProperty ModalBtnOkColorProperty =
            DependencyProperty.Register(nameof(ModalBtnOkColor), typeof(string), typeof(Modal));


        public string ModalBtnCancelColor
        {
            get { return _modalBtnCancelColor; }
            set { _modalBtnCancelColor = value; }
        }

        public static readonly DependencyProperty ModalBtnCancelColorProperty =
            DependencyProperty.Register(nameof(ModalBtnCancelColor), typeof(string), typeof(Modal));


        public string ModalBtnYesColor
        {
            get { return _modalBtnYesColor; }
            set { _modalBtnYesColor = value; }
        }

        public static readonly DependencyProperty ModalBtnYesColorProperty =
            DependencyProperty.Register(nameof(ModalBtnYesColor), typeof(string), typeof(Modal));


        public string ModalBtnNoColor
        {
            get { return _modalBtnNoColor; }
            set { _modalBtnNoColor = value; }
        }

        public static readonly DependencyProperty ModalBtnNoColorProperty =
            DependencyProperty.Register(nameof(ModalBtnNoColor), typeof(string), typeof(Modal));


        public string ModalBtnOkVisibility
        {
            get { return _modalBtnOkVisibility ?? string.Empty; }
            set { _modalBtnOkVisibility =value; }
        }
        
        public static readonly DependencyProperty ModalBtnOkVisibilityProperty =
            DependencyProperty.Register(nameof(ModalBtnOkVisibility) , typeof(Visibility), typeof(Modal));


        public string ModalBtnCancelVisibility
        {
            get { return _modalBtnCancelVisibility ?? string.Empty; }
            set { _modalBtnCancelVisibility = value;}
        }
        public static readonly DependencyProperty ModalBtnCancelVisibilityProperty =
            DependencyProperty.Register(nameof(ModalBtnCancelVisibility), typeof(Visibility), typeof(Modal));

        public string ModalBtnYesVisibility
        {
            get { return _modalBtnYesVisibility ?? string.Empty; }
            set { _modalBtnYesVisibility = value; }
        }

        public static readonly DependencyProperty ModalBtnYesVisibilityProperty =
            DependencyProperty.Register(nameof(ModalBtnYesVisibility), typeof(Visibility), typeof(Modal));

        public string ModalBtnNoVisibility
        {
            get { return _modalBtnNoVisibility ?? string.Empty; }
            set { _modalBtnNoVisibility = value; }
        }

        public static readonly DependencyProperty ModalBtnNoVisibilityProperty =
            DependencyProperty.Register(nameof(ModalBtnNoVisibility), typeof(Visibility), typeof(Modal));

        #endregion END Custom Properties
        //End Custom Properties



        //BEGIN Click Events         
        #region BEGIN Click Events
        //NOTE: Click="ModalBtnYes_Click" Implemented for Demonstration purpuses only
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
         name: "ButtonClick",
         routingStrategy:RoutingStrategy.Bubble,
         handlerType: typeof(RoutedEventHandler),
         ownerType: typeof(Modal));         

        public event RoutedEventHandler ButtonClick
        {
            add { ModalBtnYes.AddHandler(ButtonClickEvent, value); }
            remove { ModalBtnYes.RemoveHandler(ButtonClickEvent, value); }
        }

        private void ModalBtnYes_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ButtonClickEvent));
        }

        #endregion Click Events
        //END Click Events




        //BEGIN Custom Commands
        #region BEGIN Custom Commands 

        private ICommand _modalBtnOkCommand;
        private ICommand _modalBtnCancelCommand; 
        private ICommand _modalBtnYesCommand;
        private ICommand _modalBtnNoCommand;

        public ICommand ModalBtnOkCommand
        {
            get { return _modalBtnOkCommand; }
            set { _modalBtnOkCommand = value; }
        }

        public static readonly DependencyProperty ButtonOkCommandProperty =
            DependencyProperty.Register(nameof(ModalBtnOkCommand), typeof(ICommand), typeof(Modal));



        public ICommand ModalBtnCancelCommand
        {
            get { return _modalBtnCancelCommand; }
            set { _modalBtnCancelCommand = value; }

        }
        public static readonly DependencyProperty ModalBtnCancelCommandProperty =
            DependencyProperty.Register(nameof(ModalBtnCancelCommand), typeof(ICommand), typeof(Modal));


        public ICommand ModalBtnYesCommand
        {
            get { return _modalBtnYesCommand;}
            set { _modalBtnYesCommand = value;}
        }

        public static readonly DependencyProperty ModalBtnYesCommandProperty =
            DependencyProperty.Register(nameof(ModalBtnYesCommand) , typeof(ICommand), typeof(Modal));


        public ICommand ModalBtnNoCommand
        {
            get { return _modalBtnNoCommand; }
            set { _modalBtnNoCommand = value;}
        }

        public static readonly DependencyProperty ModalBtnNoCommandProperty =
            DependencyProperty.Register(nameof(ModalBtnNoCommand), typeof(ICommand), typeof(Modal));

        #endregion END Custom Commands
        //END Custom Commands 



    }
}
