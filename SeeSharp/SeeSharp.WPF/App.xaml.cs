using SeeSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            InitializeComponent();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Grid grid = new Grid();
            MainPage mainPage = new MainPage();
            ViewFactory.MainPageInstance = mainPage;

            this.MainWindow = mainPage;
        }
    }
}
