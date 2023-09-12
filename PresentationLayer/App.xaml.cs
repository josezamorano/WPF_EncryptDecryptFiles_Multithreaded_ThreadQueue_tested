using Autofac;

using ServiceLayer.Multithreading;
using ServiceLayer.Utils.Interfaces;
using DataAccessLayer.IOFiles;
using DataAccessLayer.Utils.Interfaces;
using DomainLayer;
using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.ViewModels;
using PresentationLayer.MVVM.Views;
using PresentationLayer.Utils.Interfaces;
using System.Windows;
using ServiceLayer.Helpers;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IMainWindowViewModel _mainWindowViewModel;



        public App()
        {
            ConfigureDependencyInjectionContainer();
        }

        private void ConfigureDependencyInjectionContainer()
        {

            ContainerBuilder builder = new ContainerBuilder();

            //Service Layer
            builder.RegisterType<ServiceLayer.Validator.StringTextValidator>().As<IStringTextValidator>();
            builder.RegisterType<ThreadQueue>().As<IThreadQueue>();
            builder.RegisterType<PresentationLayerHelper>().As<IPresentationLayerHelper>();

            //Data Access Layer
            builder.RegisterType<DirectoryProvider>().As<IDirectoryProvider>();
            builder.RegisterType<FileProvider>().As<IFileProvider>();
            builder.RegisterType<FileService>().As<IFileService>();

            //Domain Layer
            builder.RegisterType<CipherHelper>().As<ICipherHelper>();
            builder.RegisterType<Cipher>().As<ICipher>();
            builder.RegisterType<CryptographyHelper>().As<ICryptographyHelper>();
            builder.RegisterType<CryptographyManager>().As<ICryptographyManager>();

            //Presentation Layer
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<EncryptFilesViewModel>().As<IEncryptFilesViewModel>();
            builder.RegisterType<DecryptFilesViewModel>().As<IDecryptFilesViewModel>();


            var newContainer = builder.Build();
            ILifetimeScope newScope = newContainer.BeginLifetimeScope();
            _mainWindowViewModel = newScope.Resolve<IMainWindowViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // here you take control            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindowView mainView = new MainWindowView(_mainWindowViewModel);
            if (mainView != null)
            {
                mainView.Show();
            }
        }

    }
}
