using SeeSharp.Infrastructure;
using SeeSharp.Web;
using System.Windows;
using System.Windows.Controls;

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
            ViewFactory.MainPageInstance.Dispose();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Grid grid = new Grid();
            MainPage mainPage = new MainPage();
            ViewFactory.MainPageInstance = mainPage;

            this.MainWindow = mainPage;
        }

        private void ConfigureApp()
        {
            ServerServiceClient serviceManager = ServerServiceClient.GetInstance();
            serviceManager.CreateDirectoriesIfDosentExists();
        }
    }
}
