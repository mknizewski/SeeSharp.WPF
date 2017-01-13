using System;
using System.Windows;
using System.Windows.Media.Imaging;
using SeeSharp.BO.Managers;
using SeeSharp.BO.Dictionaries;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for AchivmentAlert.xaml
    /// </summary>
    public partial class AchivmentAlert : Window
    {
        private Achivment _achivment;

        public AchivmentAlert(Achivment achivment)
        {
            this._achivment = achivment;

            InitializeComponent();
            InitializeAlert();
        }

        private void InitializeAlert()
        {
            string achivImageFileName = string.Format(AppSettingsDictionary.AchivmentImageDirectory, _achivment.File);
            Uri uri = new Uri(achivImageFileName, UriKind.Relative);

            this.AchivImage.Source = new BitmapImage(uri);
            this.TitleText.Text = _achivment.Title;
            this.DetailsText.Text = _achivment.Details;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            WindowPage page = (WindowPage)App.Current.MainWindow;
            MainPage root = page.MainPage;

            SaveAchivmentToProfile(_achivment.Id, root.UserManager.UserInfo.Login);
            this.Close();
        }

        private static void SaveAchivmentToProfile(int achivId, string loginName)
        {
            ServerServiceClient serverService = ServerServiceClient.GetInstance();
            serverService.UpdateAchivmentFile(achivId, loginName);
        }
    }
}
