using SeeSharp.BO.Managers;
using System.Windows;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
            this.Exit += App_Exit;

            this.InitializeComponent();
            this.ConfigureApp();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
        }

        private void ConfigureApp()
        {
            ServerServiceClient serviceManager = ServerServiceClient.GetInstance();
            serviceManager.CreateDirectoriesIfDosentExists();
        }
    }
}