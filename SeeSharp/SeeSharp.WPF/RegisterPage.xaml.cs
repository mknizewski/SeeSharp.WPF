using SeeSharp.BO.Dictionaries;
using SeeSharp.BO.Managers;
using SeeSharp.Infrastructure;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string loginName = this.LoginBox.Text;

                if (string.IsNullOrEmpty(loginName))
                    throw new Exception(ExceptionDictionary.LoginNotFoundMessage);

                int generatedNumber = UserManager.GenerateCodeForNewUser();
                this.RegisterButton.IsEnabled = false;

                ServerServiceClient serverService = ServerServiceClient.GetInstance();
                bool isRegistered = serverService.CreateDirectoryForUser(loginName, generatedNumber);

                if (isRegistered)
                {
                    this.RegisterAlert.Visibility = Visibility.Visible;
                    this.CodeTextBlock.Visibility = Visibility.Visible;
                    this.RemainerTextBlock.Visibility = Visibility.Visible;

                    this.RegisterAlert.Text = string.Format(this.RegisterAlert.Text, loginName);
                    this.CodeTextBlock.Text = generatedNumber.ToString();

                    this.LoginButton.IsEnabled = true;
                }
                else
                {
                    string exceptionMessage = string.Format(ExceptionDictionary.LoginIsUsed, loginName);
                    throw new Exception(exceptionMessage);
                }
            }
            catch (Exception ex)
            {
                WindowPage page = (WindowPage)App.Current.MainWindow;
                MainPage root = page.MainPage;

                root.SetAlert(ex.Message);
            }
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;

            root.SetView(ViewType.Login, NavigationDictionary.LoginPageView);
        }
    }
}