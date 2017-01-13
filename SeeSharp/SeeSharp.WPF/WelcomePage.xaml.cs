using SeeSharp.BO.Dictionaries;
using SeeSharp.BO.Managers;
using SeeSharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        private const int MaxModulesDiffrence = 1;

        public WelcomePage()
        {
            InitializeComponent();
            InitializeView();
        }

        private void InitializeAchivmentPanel()
        {
            UserManager userManager = ViewFactory.MainPageInstance.UserManager;

            ServerServiceClient serverSevice = ServerServiceClient.GetInstance();
            int[] achivList = serverSevice.GetAchivmentFile(userManager.UserInfo.Login);

            if (achivList != null)
            {
                achivList.ToList().ForEach(id =>
                {
                    Achivment achivment = AchivmentManager.GetAchivment((Achivments)id);
                    AchivmentItem item = new AchivmentItem(achivment);

                    this.AchivmentPanel.Children.Add(item);
                });
            }
            else
            {
                this.AchivmentPanel.Visibility = System.Windows.Visibility.Collapsed;
                this.AchivmentBorder.Visibility = System.Windows.Visibility.Collapsed;
            }

            this.UpdateLayout();
        }

        private void InitializeView()
        {
            MainPage mainView = ViewFactory.MainPageInstance;

            if (mainView != null)
            {
                if (mainView.UserManager != null)
                {
                    string lastModuleMessage = string.IsNullOrEmpty(mainView.UserManager.UserInfo.LastTutorial) ? PageDictionary.TutorialNotStarted : mainView.UserManager.UserInfo.LastTutorial;

                    this.LayoutRoot.Visibility = Visibility.Visible;
                    this.CodeTextBlock.Text = string.Format(CodeTextBlock.Text, mainView.UserManager.UserInfo.Code);
                    this.PercentageTextBlock.Text = string.Format(PercentageTextBlock.Text, mainView.UserManager.UserInfo.Percentage);
                    this.LastModuleTextBlock.Text = string.Format(LastModuleTextBlock.Text, lastModuleMessage);
                    this.CuriositiesTextBox.Text = CuriositiesManager.GetRandomCuriosities();
                    this.GreetingsTextBlock.Text = GreetingsManager.GetGreetingsByDayOfWeek(DateTime.Now.DayOfWeek, mainView.UserManager.UserInfo.Login);

                    InitializeAchivmentPanel();
                }
                else
                    this.LayoutRoot.Visibility = Visibility.Collapsed;
            }
            else
                this.LayoutRoot.Visibility = Visibility.Collapsed;
        }

        private void DeleteAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void NewModuleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void PervButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CuriositiesTextBox.Text = CuriositiesManager.GetPervCuriosities();
        }

        private void NextButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CuriositiesTextBox.Text = CuriositiesManager.GetNextCuriosities();
        }

        private void ReturnToModuleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                string lastModuleTag = ViewFactory.MainPageInstance.UserManager.UserInfo.LastTutorial;

                if (string.IsNullOrEmpty(lastModuleTag))
                    throw new Exception(ExceptionDictionary.TutorialNotStarted);

                ViewFactory.MainPageInstance.SetModule(lastModuleTag);
            }
            catch (Exception ex)
            {
                ViewFactory.MainPageInstance.SetAlert(ex.Message);
            }
        }

        #region Modules & ModuleEvents

        private void LoadModule(TreeViewItem selectedItem)
        {
            if (selectedItem != null)
            {
                try
                {
                    List<Module> modules = ModuleManager.ModuleList;
                    UserManager userManager = ViewFactory.MainPageInstance.UserManager;

                    string selectedTag = selectedItem.Tag.ToString();
                    string userTag = userManager.UserInfo.LastTutorial;

                    if (string.IsNullOrEmpty(userTag) && selectedTag != modules.First().ModuleTag)
                        throw new Exception(ExceptionDictionary.TutorialNotStarted);

                    int selectedTagIndex = modules.IndexOf(modules.Where(x => x.ModuleTag == selectedTag).First());
                    int userTagIndex = string.IsNullOrEmpty(userTag) ? (int)decimal.Zero : modules.IndexOf(modules.Where(x => x.ModuleTag == userTag).First());
                    int diffrence = selectedTagIndex - userTagIndex;

                    if (diffrence > MaxModulesDiffrence)
                        throw new Exception(ExceptionDictionary.ModuleNotAllowed);

                    ViewFactory.MainPageInstance.SetModule(selectedTag);
                }
                catch (Exception ex)
                {
                    ViewFactory.MainPageInstance.SetAlert(ex.Message);
                }
            }
        }

        private TreeViewItem GetSelectedItem(object sender)
        {
            TreeViewItem list = sender as TreeViewItem;
            TreeViewItem selectedModule = null;

            list.Items.Cast<TreeViewItem>().ToList().ForEach(module =>
            {
                if (module.IsSelected)
                    selectedModule = module;
            });

            return selectedModule;
        }

        private void LoadInnerModule_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadModule(GetSelectedItem(sender));
        }

        private void LoadTopModule_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LoadModule(sender as TreeViewItem);
        }

        #endregion Modules & ModuleEvents
    }
}
