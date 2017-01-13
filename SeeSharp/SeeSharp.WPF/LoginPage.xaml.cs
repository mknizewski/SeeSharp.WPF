using SeeSharp.BO.Dictionaries;
using SeeSharp.BO.Managers;
using SeeSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        private const string RegexNumberOnlyPattern = "[^0-9.-]+";

        public LoginPage()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            ;
        }

        private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                string loginName = this.LoginBox.Text;
                string loginCode = this.CodeBox.Text;
                MainPage root = ViewFactory.MainPageInstance;
                ServerServiceClient serverService = ServerServiceClient.GetInstance();

                if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(loginCode))
                    throw new Exception(ExceptionDictionary.IncorrectLoginCreditentials);

                if (IsNotNumber(loginCode))
                    throw new Exception(ExceptionDictionary.CodeIsNotNumber);

                Dictionary<string, string> userProfile = serverService.GetUserProfile(loginName);
                root.UserManager = ManagerFactory.GetManager<UserManager>();
                root.UserManager.SignIn(userProfile, loginCode);

                root.SetView(ViewType.WelcomePage, NavigationDictionary.WelcomePageView);
                root.SetUserMenuView(User.Logged);
                root.UpdateLayout();
            }
            catch (Exception ex)
            {
                ViewFactory.MainPageInstance.SetAlert(ex.Message);
            }
        }

        private static bool IsNotNumber(string text)
        {
            Regex regex = new Regex(RegexNumberOnlyPattern);

            return regex.IsMatch(text);
        }
    }
}
