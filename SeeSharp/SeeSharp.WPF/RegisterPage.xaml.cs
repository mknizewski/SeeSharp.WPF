﻿using SeeSharp.BO.Dictionaries;
using SeeSharp.BO.Managers;
using SeeSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

                //ServerServiceClient serverService = ServerServiceClient.GetInstance();
                //serverService.CreateDirectoryForUserAsync(loginName, generatedNumber);
                //serverService.CreateDirectoryForUserCompleted += (send, recv) =>
                //{
                //    try
                //    {
                //        bool isRegistered = recv.Result;

                //        if (isRegistered)
                //        {
                //            this.RegisterAlert.Visibility = Visibility.Visible;
                //            this.RegisterAlert.Text = string.Format(
                //                PageDictionary.SuccesfulRegisterMessagePattern,
                //                loginName,
                //                Environment.NewLine,
                //                generatedNumber);

                //            this.LoginButton.IsEnabled = true;
                //        }
                //        else
                //        {
                //            string exceptionMessage = string.Format(ExceptionDictionary.LoginIsUsed, loginName);

                //            throw new Exception(exceptionMessage);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        ViewFactory.MainPageInstance.SetAlert(ex.Message);
                //        this.RegisterButton.IsEnabled = true;
                //    }
                //};
            }
            catch (Exception ex)
            {
                ViewFactory.MainPageInstance.SetAlert(ex.Message);
            }
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            ViewFactory.MainPageInstance.SetView(ViewType.Login, NavigationDictionary.LoginPageView);
        }
    }
}
